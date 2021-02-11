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

namespace Admin.Web.Controllers
{
    [Authorize]
    public class WareHouseController : Controller
    {
        public CommandDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public WareHouseController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
            //  _configuration = configuration;
        }
        public IActionResult Index()
        {

            var list = _dbContext.Warehouse.Where(w => w.Status.Equals("1")).ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Warehouse/Update.cshtml");
        }
        public IActionResult Edit(int id)
        {
            var result = _dbContext.Warehouse.Where(w => w.Id.Equals(id)).FirstOrDefault();
            return View("~/Views/Warehouse/Update.cshtml", result);
        }
        [HttpPost]
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
                    model.CreatedDate = DateTime.Now;

                    _dbContext.Warehouse.Update(model);
                }
                _dbContext.SaveChanges();
            }
            //redirect to edit
            //return RedirectToAction("Edit", new { id = model.Id });
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = _dbContext.Warehouse.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.Status = "0";
            _dbContext.Warehouse.Update(result);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
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
