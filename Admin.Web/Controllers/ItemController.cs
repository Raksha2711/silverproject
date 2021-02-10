﻿using Command.Entity1;
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
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
//using Dapper;

namespace Admin.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        public CommandDbContext _dbContext;

        public ItemController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var list = _dbContext.Item.Where(w => w.Status.Equals("1")).ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Item/Update.cshtml");
        }
        public IActionResult Edit(int id)
        {
            var result = _dbContext.Item.Where(w => w.Id.Equals(id)).FirstOrDefault();
            return View("~/Views/Item/Update.cshtml", result);
        }
        [HttpPost]
        public IActionResult Update(Item model)
        {
            if (model != null)
            {
                model.Status = "1";
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    _dbContext.Item.Add(model);
                }
                else
                {
                    _dbContext.Item.Update(model);
                }
                _dbContext.SaveChanges();
            }
            //redirect to edit
              return RedirectToAction("Index");
            // var list = _dbContext.Item.Where(w => w.Status.Equals("1")).ToList();
            //Index();
           // return View(list);
        }

        public IActionResult Delete(int id)
        {
            var result = _dbContext.Item.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.Status = "0";
            _dbContext.Item.Update(result);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<List<Item>> Import()
        {
            IFormFile formFile = Request.Form.Files[0];
            var list = new List<Item>();
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
                            list.Add(new Item
                            {
                                Name = (worksheet.Cells[row, 1].Value).ToString(),
                                CreatedDate = DateTime.Now,
                                Status = "1"
                            });

                        }
                        if (list.Count > 0)
                        {
                            var newUserIDs = list.Select(u => u.Name).Distinct().ToArray();
                            var usersInDb = _dbContext.Item.Where(u => newUserIDs.Contains(u.Name))
                                                           .Select(u => u.Name).ToArray();
                            var usersNotInDb = list.Where(u => !usersInDb.Contains(u.Name));
                            foreach (Item user in usersNotInDb)
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
            var item =  _dbContext.Item.Where(w => w.Status.Equals("1")).ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("test");
                workSheet.Cells.LoadFromCollection(item, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"ItemData-{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}
