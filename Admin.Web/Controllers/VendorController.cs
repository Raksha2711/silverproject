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

namespace Admin.Web.Controllers
{
    public class VendorController : Controller
    {
        public CommandDbContext _dbContext;
        public VendorController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var list = _dbContext.Vendor.Where(w => w.Status.Equals("1")).ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Vendor/Update.cshtml");
        }
        public IActionResult Edit(int id)
        {
            var result = _dbContext.Vendor.Where(w => w.Id.Equals(id)).FirstOrDefault();
            return View("~/Views/Vendor/Update.cshtml", result);
        }
        [HttpPost]
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
                    _dbContext.Vendor.Update(model);
                }
                _dbContext.SaveChanges();
            }
            // return RedirectToAction("Edit", new { id = model.Id });
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var result = _dbContext.Vendor.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.Status = "0";
            _dbContext.Vendor.Update(result);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
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
                                CreatedDate = DateTime.Now,
                                Status = "1"
                            });

                        }
                        if (list.Count > 0)
                        {
                            var newUserIDs = list.Select(u => u.Name).Distinct().ToArray();
                            var usersInDb = _dbContext.Vendor.Where(u => newUserIDs.Contains(u.Name))
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
    }
}
