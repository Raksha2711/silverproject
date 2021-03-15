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
using Microsoft.AspNetCore.Authorization;
using Admin.Web.Models;
using System.Globalization;

namespace Admin.Web.Controllers
{
    [Authorize]
    [Route("vendor")]
    public class VendorController : Controller
    {
        public CommandDbContext _dbContext;
        public VendorController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Route("", Name = "vendor")]
        public IActionResult Index()
        {
            var list = _dbContext.Vendor.Where(w => w.Status.Equals("1")).ToList();
            return View(list);
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/Vendor/Update.cshtml");
        }
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var result = _dbContext.Vendor.Where(w => w.Id.Equals(id)).FirstOrDefault();
            return View("~/Views/Vendor/Update.cshtml", result);
        }
        [HttpPost]
        [Route("update")]
        public IActionResult Update(Vendor model)
        {
            if (model != null)
            { 
                model.Status = "1";
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    _dbContext.Vendor.Add(model);
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    var res = _dbContext.Vendor.Where(x => x.Id == model.Id).FirstOrDefault();
                    if(res != null)
                    {
                        res.CreatedDate = DateTime.Now;
                        res.Name = model.Name;
                        res.Address = model.Address;
                        res.EmailId = model.EmailId;
                        res.EmailId2 = model.EmailId2;
                        res.ContactNo = model.ContactNo;
                        res.ContactPerson = model.ContactPerson;
                        res.ModifiedDate = model.ModifiedDate;
                        res.ModifiedBy = model.ModifiedBy;
                        _dbContext.Vendor.Update(res);
                    }
                }
                _dbContext.SaveChanges();
            }
            // return RedirectToAction("Edit", new { id = model.Id });
            return RedirectToAction("Index");
        }
        [Route("deleterow/{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _dbContext.Vendor.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.Status = "0";
            _dbContext.Vendor.Update(result);
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
        internal DtResult<Vendor> GetList(DTParameters param)
        {
            var result = new DtResult<Vendor>();
            var query = (from v in _dbContext.Vendor
                         where v.Status.Equals("1")
                         select new Vendor
                         {
                             Id = v.Id,
                             CreatedDate = v.CreatedDate,
                             Name = v.Name,
                             Address = v.Address,
                             EmailId = v.EmailId,
                             EmailId2 = v.EmailId2,
                             ContactNo = v.ContactNo,
                             ContactPerson = v.ContactPerson
                         });
            result.data = new List<Vendor>();
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
                query = query.Where(w => w.Name.Contains(keyword) || w.Address.Contains(keyword) || w.EmailId.Contains(keyword) || w.ContactNo.Contains(keyword) || w.ContactPerson.Contains(keyword));
                                                                             //w.Entity.Value.Contains(keyword) || w.PromoterName.Contains(keyword));
            }
            if (param.Order != null && param.Order.Length > 0)
            {
                foreach (var item in param.Order)
                {
                    //if (param.Columns[item.Column].Data.Equals("id"))
                    //    query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Id) : query.OrderBy(o => o.Id);
                     if (param.Columns[item.Column].Data.Equals("name"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Name) : query.OrderBy(o => o.Name);
                    else if (param.Columns[item.Column].Data.Equals("address"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Address) : query.OrderBy(o => o.Address);
                    else if (param.Columns[item.Column].Data.Equals("emailId"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.EmailId) : query.OrderBy(o => o.EmailId);
                    else if (param.Columns[item.Column].Data.Equals("emailId2"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.EmailId2) : query.OrderBy(o => o.EmailId2);
                    else if (param.Columns[item.Column].Data.Equals("contactNo"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.ContactNo) : query.OrderBy(o => o.ContactNo);
                    else if (param.Columns[item.Column].Data.Equals("contactPerson"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.ContactPerson) : query.OrderBy(o => o.ContactPerson);
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
        public async Task<List<Vendor>> Import()
        {
            IFormFile formFile = Request.Form.Files[0];
            var list = new List<Vendor>();
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
                            list.Add(new Vendor
                            {
                                Name = (worksheet.Cells[row, 1].Value).ToString(),
                                Address = (worksheet.Cells[row, 2].Value).ToString(),
                                EmailId= (worksheet.Cells[row, 3].Value).ToString(),
                                EmailId2 = (worksheet.Cells[row, 4].Value).ToString(),
                                ContactNo = (worksheet.Cells[row, 5].Value).ToString(),
                                ContactPerson = (worksheet.Cells[row, 6].Value).ToString(),
                                CreatedDate = DateTime.Now,
                                Status = "1"
                            });

                        }
                        if (list.Count > 0)
                        {
                            var newUserIDs = list.Select(u => u.Name).Distinct().ToArray();
                            var usersInDb = _dbContext.Vendor.Where(u => newUserIDs.Contains(u.Name) && u.Status.Equals("1"))
                                                           .Select(u => u.Name).ToArray();
                            var usersNotInDb = list.Where(u => !usersInDb.Contains(u.Name));
                            foreach (Vendor user in usersNotInDb)
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
            var item = _dbContext.Vendor.Where(w => w.Status.Equals("1")).Select(s => new { s.Name, s.Address,s.EmailId,s.EmailId2,s.ContactNo,s.ContactPerson }).ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("test");
                workSheet.Cells.LoadFromCollection(item, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"VendorData-{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
