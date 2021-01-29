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
                    _dbContext.Warehouse.Update(model);
                }
                _dbContext.SaveChanges();
            }
            //redirect to edit
            return RedirectToAction("Edit", new { id = model.Id });
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
                                CreatedDate = DateTime.Now,
                                Status = "1"
                            });

                        }
                        if (list.Count > 0)
                        {
                            var newUserIDs = list.Select(u => u.Name).Distinct().ToArray();
                            var usersInDb = _dbContext.Warehouse.Where(u => newUserIDs.Contains(u.Name)).Select(u => u.Name).ToArray();
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
            //Let use below test data for writing it to excel
            var item = _dbContext.SalesPerson.Where(w => w.Status.Equals("1")).ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // let's convert our object data to Datatable for a simplified logic.
            // Datatable is the easiest way to deal with complex datatypes for easy reading and formatting. 
            //DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(item), (typeof(DataTable)));
            //string path = HttpContext.Current.Server.MapPath(HttpRequest.ApplicationPath);
            //string path = "F:/";// System.Environment.GetFolderPath(Environment.SpecialFolder);
            //FileInfo filePath = new FileInfo(path);
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("test");
                workSheet.Cells.LoadFromCollection(item, true);

            }
            //using (var excelPack = new ExcelPackage(filePath))
            //{
            //    var ws = excelPack.Workbook.Worksheets.Add("WriteTest");
            //    ws.Cells.LoadFromDataTable(table, true, OfficeOpenXml.Table.TableStyles.Light8);
            //    excelPack.Save();
            //}
            stream.Position = 0;
            string excelName = $"FlashSale-{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            // return ;
        }
    }
}
