using Command.Entity1;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Admin.Web.Models;

namespace Admin.Web.Controllers
{
    [Authorize]
    [Route("salesperson")]
    public class SalesPersonController : Controller
    {
        public CommandDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public SalesPersonController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
            //  _configuration = configuration;
        }
        [Route("", Name = "salesperson")]
        public IActionResult Index()
        {
            var list = _dbContext.SalesPerson.Where(w => w.Status.Equals("1")).ToList();
            return View(list);
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/SalesPerson/Update.cshtml");
        }

        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var result = _dbContext.SalesPerson.Where(w => w.Id.Equals(id)).FirstOrDefault();
            return View("~/Views/SalesPerson/Update.cshtml", result);
        }
        [HttpPost]
        [Route("update")]
        public IActionResult Update(SalesPerson model)
        {
            if (model != null)
            {
                model.Status = "1";
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    _dbContext.SalesPerson.Add(model);
                }
                else
                {
                    var res = _dbContext.SalesPerson.Where(x => x.Id == model.Id).FirstOrDefault();
                    if(res != null)
                    {
                        res.ModifiedDate = model.ModifiedDate;
                        res.Name = model.Name;
                        res.CreatedDate = DateTime.Now;
                        res.Status = model.Status;
                        res.ModifiedBy = model.ModifiedBy;
                        _dbContext.SalesPerson.Update(res);
                    }

                }
                _dbContext.SaveChanges();
            }
            //redirect to edit
            //return RedirectToAction("Edit", new { id = model.Id });
            return RedirectToAction("Index");
        }
        [Route("deleterow/{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _dbContext.SalesPerson.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.Status = "0";
            _dbContext.SalesPerson.Update(result);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("Import")]
        public async Task<List<SalesPerson>> Import()
        {
            IFormFile formFile = Request.Form.Files[0];
            var list = new List<SalesPerson>();
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    try
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new SalesPerson
                            {
                                Name = (worksheet.Cells[row, 1].Value).ToString(),
                                CreatedDate = DateTime.Now,
                                Status = "1"
                            });

                        }
                        if (list.Count > 0)
                        {
                            var newUserIDs = list.Select(u => u.Name).Distinct().ToArray();
                            var usersInDb = _dbContext.SalesPerson.Where(u => newUserIDs.Contains(u.Name) && u.Status.Equals("1"))
                                                           .Select(u => u.Name).ToArray();
                            var usersNotInDb = list.Where(u => !usersInDb.Contains(u.Name));
                            foreach (SalesPerson user in usersNotInDb)
                            {
                                _dbContext.Add(user);
                                _dbContext.SaveChanges();
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return list;
        }
        [Route("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var item = _dbContext.SalesPerson.Where(w => w.Status.Equals("1")).Select(s => s.Name).ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("test");
                workSheet.Cells.LoadFromCollection(item, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"SalesPersonData-{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpPost]
        [Route("list.json")]
        public IActionResult List(DTParameters param)
        {
            var result = GetList(param);
            result.draw = param.Draw;
            return Json(result);
        }
        internal DtResult<SalesPerson> GetList(DTParameters param)
        {
            var result = new DtResult<SalesPerson>();
            var query = (from s in _dbContext.SalesPerson
                         where s.Status.Equals("1")
                         select new SalesPerson
                         {
                             Id = s.Id,
                             Name = s.Name
                         });

            result.data = new List<SalesPerson>();
            result.recordsTotal = query.Count();

            var searchColumn = (from sr in param.Columns where !string.IsNullOrWhiteSpace(sr.Search.Value) select sr).ToList();
            if (searchColumn?.Count() > 0)
            {
                foreach (var item in searchColumn)
                {
                    if (item.Name.ToLower().Equals("name"))
                    {
                        if (!string.IsNullOrWhiteSpace(item.Search.Value))
                            query = query.Where(w => w.Name.Contains(item.Search.Value));

                    }
                }
            }

            if (param.Search != null && !string.IsNullOrEmpty(param.Search.Value))
            {
                var keyword = param.Search.Value;
                query = query.Where(w => w.Name.Contains(keyword));// ||
                                                                   //w.Entity.Value.Contains(keyword) || w.PromoterName.Contains(keyword));
            }
            if (param.Order != null && param.Order.Length > 0)
            {
                foreach (var item in param.Order)
                {
                    if (param.Columns[item.Column].Data.Equals("name"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Name) : query.OrderBy(o => o.Name);
                }
            }
            result.recordsFiltered = query.Count();
            if (param.Length > 0)
            {
                query = query.Skip(param.Start).Take(param.Length);
            }
            var entries = query.ToList();

            foreach (var e in entries) { result.data.Add(e); }
            return result;
        }
    }
}
