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
    [Route("warehouse")]
    public class WareHouseController : Controller
    {
        public CommandDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public WareHouseController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
            //  _configuration = configuration;
        }
        [Route("", Name = "warehouse")]
        public IActionResult Index()
        {

            var list = _dbContext.Warehouse.Where(w => w.Status.Equals("1")).ToList();
            return View(list);
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/Warehouse/Update.cshtml");
        }
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var result = _dbContext.Warehouse.Where(w => w.Id.Equals(id)).FirstOrDefault();
            return View("~/Views/Warehouse/Update.cshtml", result);
        }
        [HttpPost]
        [Route("update")]
        public IActionResult Update(Warehouse model)
        {
            if (model != null)
            {
                model.Status = "1";
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    _dbContext.Warehouse.Add(model);
                }
                else
                {
                    var res = _dbContext.Warehouse.Where(x => x.Id == model.Id).FirstOrDefault();
                    if(res != null)
                    {
                        res.CreatedDate = DateTime.Now;
                        res.Name = model.Name;
                        res.Address = model.Address;
                        res.ModifiedBy = model.ModifiedBy;
                        res.ModifiedDate = model.ModifiedDate;
                        _dbContext.Warehouse.Update(res);
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
            var result = _dbContext.Warehouse.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.Status = "0";
            _dbContext.Warehouse.Update(result);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("list.json")]
        public IActionResult List(DTParameters param)
        {
            var result = GetList(param);
            result.draw = param.Draw;
            return Json(result);
        }
        internal DtResult<Warehouse> GetList(DTParameters param)
        {
            var result = new DtResult<Warehouse>();
            var query = (from v in _dbContext.Warehouse
                         where v.Status.Equals("1")
                         select new Warehouse
                         {
                             Id = v.Id,
                             Name = v.Name,
                             Address = v.Address,
                         });

            result.data = new List<Warehouse>();
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
                query = query.Where(w => w.Name.Contains(keyword) || w.Address.Contains(keyword));
                                                                   //w.Entity.Value.Contains(keyword) || w.PromoterName.Contains(keyword));
            }
            if (param.Order != null && param.Order.Length > 0)
            {
                foreach (var item in param.Order)
                {
                    if (param.Columns[item.Column].Data.Equals("name"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Name) : query.OrderBy(o => o.Name);
                    else if (param.Columns[item.Column].Data.Equals("address"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Address) : query.OrderBy(o => o.Address);
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
        [Route("Import")]
        public async Task<List<Warehouse>> Import()
        {
            IFormFile formFile = Request.Form.Files[0];
            var list = new List<Warehouse>();
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
                            list.Add(new Warehouse
                            {
                                Name = (worksheet.Cells[row, 1].Value).ToString(),
                                Address = (worksheet.Cells[row, 2].Value).ToString(),
                                CreatedDate = DateTime.Now,
                                Status = "1"
                            });

                        }
                        if (list.Count > 0)
                        {
                            var newUserIDs = list.Select(u => u.Name).Distinct().ToArray();
                            var usersInDb = _dbContext.Warehouse.Where(u => newUserIDs.Contains(u.Name) && u.Status.Equals("1")).Select(u => u.Name).ToArray();
                            var usersNotInDb = list.Where(u => !usersInDb.Contains(u.Name));
                            foreach (Warehouse user in usersNotInDb)
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
            var item = _dbContext.Warehouse.Where(w => w.Status.Equals("1")).Select(s =>new { s.Name,s.Address} ).ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("test");
                workSheet.Cells.LoadFromCollection(item, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"WareHouse-{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
