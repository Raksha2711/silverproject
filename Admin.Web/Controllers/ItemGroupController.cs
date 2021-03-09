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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Admin.Web.Controllers
{
    [Authorize]
    [Route("itemgroup")]
    public class ItemGroupController : Controller
    {
        public CommandDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public static string DbConnection { get; set; }
        //static PDFControllerService()
        //{
        //    DbConnection = "Server=todtrips.database.windows.net;Database=TodTrip;persist security info=True;user id=todtrips;password=clouddb@7519;MultipleActiveResultSets=True";
        //}
        public ItemGroupController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
            DbConnection = "Data Source=sql5050.site4now.net;User ID=DB_A6EA60_Raksha2710_admin;Password=Mazda@123;";
        }

        [Route("", Name = "itemgroup")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            //FillDropDown();
            ViewBag.ItemGroupNLevelString = FillDropDown().Result;
            return View("~/Views/ItemGroup/Update.cshtml");
        }

        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var result = _dbContext.ItemGroup.Where(w => w.Id.Equals(id)).FirstOrDefault();

            //FillDropDown();
            ViewBag.ItemGroupNLevelString = FillDropDown().Result;
            return View("~/Views/ItemGroup/Update.cshtml", result);
        }
        [HttpPost]
        [Route("update")]
        public IActionResult Update(ItemGroup model)
        {
            if (model != null)
            {
                model.Status = "1";
                if (model.ParentItemGroupId == "0")
                {
                    model.ItemGroupNLevelString = model.ItemGroupName;
                }
                else
                {
                    var ItemNLevelName = _dbContext.ItemGroup.Where(x => x.Id.ToString() == model.ParentItemGroupId).Select(s => s.ItemGroupNLevelString).FirstOrDefault();
                    model.ItemGroupNLevelString = ItemNLevelName + ">>" + model.ItemGroupName;
                }
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    _dbContext.ItemGroup.Add(model);
                }
                else
                {
                    var res = _dbContext.ItemGroup.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (res != null)
                    {
                        res.ModifiedDate = model.ModifiedDate;
                        res.ItemGroupName = model.ItemGroupName;
                        res.ItemGroupNLevelString = model.ItemGroupNLevelString;
                        res.CreatedDate = DateTime.Now;
                        res.Status = model.Status;
                        res.ModifiedBy = model.ModifiedBy;
                        _dbContext.ItemGroup.Update(res);
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
            var result = _dbContext.ItemGroup.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.Status = "0";
            _dbContext.ItemGroup.Update(result);
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
        internal DtResult<ItemGroup> GetList(DTParameters param)
        {
            var result = new DtResult<ItemGroup>();
            var query = (from s in _dbContext.ItemGroup
                         //join t in _dbContext.ItemGroup on s.Id.ToString() equals t.ParentItemGroupId
                         where s.Status.Equals("1")
                         select new ItemGroup
                         {
                             Id = s.Id,
                             ItemGroupName = s.ItemGroupName,
                             ItemGroupNLevelString = s.ItemGroupNLevelString
                         });
           
            result.data = new List<ItemGroup>();
            result.recordsTotal = query.Count();

            var searchColumn = (from sr in param.Columns where !string.IsNullOrWhiteSpace(sr.Search.Value) select sr).ToList();
            if (searchColumn?.Count() > 0)
            {
                foreach (var item in searchColumn)
                {
                    if (item.Name.ToLower().Equals("name"))
                    {
                        if (!string.IsNullOrWhiteSpace(item.Search.Value))
                            query = query.Where(w => w.ItemGroupName.Contains(item.Search.Value));

                    }
                }
            }

            if (param.Search != null && !string.IsNullOrEmpty(param.Search.Value))
            {
                var keyword = param.Search.Value;
                query = query.Where(w => w.ItemGroupName.Contains(keyword) || w.ItemGroupNLevelString.Contains(keyword));// ||
                                                                            //w.Entity.Value.Contains(keyword) || w.PromoterName.Contains(keyword));
            }
            if (param.Order != null && param.Order.Length > 0)
            {
                foreach (var item in param.Order)
                {
                    if (param.Columns[item.Column].Data.Equals("itemgroupname"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.ItemGroupName) : query.OrderBy(o => o.ItemGroupName);
                    if (param.Columns[item.Column].Data.Equals("itemgroupnlevelstring"))
                        query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.ItemGroupNLevelString) : query.OrderBy(o => o.ItemGroupNLevelString);
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
        internal async Task<List<SelectListItem>> FillDropDown()

        {
            var result = new List<SelectListItem>();
            try
            {
                using (var db = new SqlConnection(DbConnection))
                {
                    var query = $@"with  nleveltbl(Id,ItemGroupName,ItemGroupNLevelString,ParentItemGroupId) as
                    (
                        select 
                        id as Id,
                        ItemGroupName  ,
                        CAST( ItemGroupName as varchar(max)) as ItemGroupNLevelString ,
                        ParentItemGroupId
                        from po.ItemGroup 
                        where ParentItemGroupId =0
                        UNION ALL
                        select 
                        i.id as Id,
                        i.ItemGroupName ,
                        CAST( CONCAT(ig.ItemGroupName,' >> ', i.ItemGroupName)as varchar(max)) as ItemGroupNLevelString,
                        i.ParentItemGroupId
                        from po.ItemGroup i
                        join nleveltbl ig on i.ParentItemGroupId=ig.Id
                        )
                        select * from nleveltbl";
                    var queryResult = await db.QueryAsync<ItemGroup>(query);
                    db.Close();
                    var qresult = queryResult.ToList();
                    result = qresult.Select(s => new SelectListItem { Text = s.ItemGroupNLevelString, Value = s.Id.ToString() }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("Import")]
        public IActionResult Import()
        {
            IFormFile formFile = Request.Form.Files[0];
            var list = new List<ItemGroup>();
            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    try
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value != null)
                            {
                                list.Add(new ItemGroup
                                {
                                    ItemGroupName = (worksheet.Cells[row, 1].Value).ToString(),
                                    ParentItemGroupId = GetId((worksheet.Cells[row, 2].Value).ToString()).ToString(),
                                    CreatedDate = DateTime.Now,
                                    Status = "1"
                                });
                            }

                        }
                        if (list.Count > 0)
                        {
                            var newUserIDs = list.Select(u => u.ItemGroupName).Distinct().ToArray();
                            var usersInDb = _dbContext.ItemGroup.Where(u => newUserIDs.Contains(u.ItemGroupName) && u.Status.Equals("1"))
                                                           .Select(u => u.ItemGroupName).ToArray();
                            var usersNotInDb = list.Where(u => !usersInDb.Contains(u.ItemGroupName));
                            foreach (ItemGroup user in usersNotInDb)
                            {
                                //user.ParentItemGroupId = "0";
                                user.ItemGroupNLevelString = _dbContext.ItemGroup.Where(u => u.Id.ToString().Equals(user.ParentItemGroupId)).Select(s=>s.ItemGroupName).FirstOrDefault();
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
            return RedirectToAction("Index");
        }
        internal int GetId(string name)
        {
            var id = 0;
            try
            {
                 id = _dbContext.ItemGroup.Where(w => w.ItemGroupName.Equals(name)).Select(s => s.Id).First();
            }
            catch (Exception e)
            {
                if (id == null || id == 0)
                {
                    ItemGroup model = new ItemGroup();
                    model.Status = "1";
                    model.ItemGroupName = name;
                    model.ParentItemGroupId = "0";
                    model.ItemGroupNLevelString = model.ItemGroupName;
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedBy = "1";
                    model.CreatedBy = "1";
                    model.ModifiedDate = DateTime.Now;
                    _dbContext.ItemGroup.Add(model);
                    _dbContext.SaveChanges();
                    var idval = _dbContext.ItemGroup.Where(w => w.ItemGroupName.Equals(name)).Select(s => s.Id).First();
                    return idval;
                }
            }
            return id;
        }
        [Route("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var item = _dbContext.ItemGroup.Where(w => w.Status.Equals("1")).Select(s => s.ItemGroupName).ToList();
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
