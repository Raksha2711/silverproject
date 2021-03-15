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
using Microsoft.Data.SqlClient;
using Dapper;
using Admin.Web.ViewModel;

namespace Admin.Web.Controllers
{
    [Authorize]
    [Route("item")]
    public class ItemController : Controller
    {
        public CommandDbContext _dbContext;
        public static string DbConnection { get; set; }
        public ItemController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
            //DbConnection = "Data Source=sql5050.site4now.net;User ID=DB_A6EA60_Raksha2710_admin;Password=Mazda@123;";
            DbConnection = "Data Source=b2bpotential.cnb1fgovpd8k.ap-south-1.rds.amazonaws.com; Database=SLPurchaseOrderDB; User ID=admin; Password=STELLANS$sd*1;";

        }
        [Route("", Name = "item")]
        public IActionResult Index()
        {
            //var list = _dbContext.Item.Where(w => w.Status.Equals("1")).ToList();
            return View();
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            FillDropDown();
            return View("~/Views/Item/Update.cshtml");
        }
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var result = _dbContext.Item.Where(w => w.Id.Equals(id)).FirstOrDefault();
            return View("~/Views/Item/Update.cshtml", result);
        }
        [HttpPost]
        [Route("update")]
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
                    var res = _dbContext.Item.Where(x => x.Id == model.Id).FirstOrDefault();
                    if(res!= null)
                    {
                        res.Name = model.Name;
                        res.ModifiedBy = model.ModifiedBy;
                        res.ModifiedDate = model.ModifiedDate;
                        res.CreatedDate = DateTime.Now;
                        _dbContext.Item.Update(res);
                    }

                }
                _dbContext.SaveChanges();
            }
              return RedirectToAction("Index");
        }
        [Route("deleterow/{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _dbContext.Item.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.Status = "0";
            _dbContext.Item.Update(result);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("Import")]
        public  IActionResult Import()
        {
            IFormFile formFile = Request.Form.Files[0];
            var list = new List<Item>();
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
                            var usersInDb = _dbContext.Item.Where(u => newUserIDs.Contains(u.Name) && u.Status.Equals("1"))
                                                           .Select(u => u.Name).ToArray();
                            var usersNotInDb = list.Where(u => !usersInDb.Contains(u.Name));
                            foreach (Item user in usersNotInDb)
                            {
                                user.ItemGroupId = 20;
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
        [Route("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            //var item =  _dbContext.Item.Where(w => w.Status.Equals("1")).Select(s => new { s.Name}).ToList();
            var item = _dbContext.Item.Where(w => w.Status.Equals("1")).Select(s =>  s.Name ).ToList();
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

        [HttpPost]
        [Route("list.json")]
        public IActionResult List(DTParameters param)
        {
            var result = GetList(param);
            result.draw = param.Draw;
            return Json(result);
        }
        internal DtResult<Item> GetList(DTParameters param)
        {
            var result = new DtResult<Item>();
            var query = (from s in _dbContext.Item
                         where s.Status.Equals("1")
                         select new Item
                         {
                             Id = s.Id,
                             Name = s.Name
                         });
            //var query = (from s in _dbContext.Item
            //             join st in _dbContext.ItemGroup on s.ItemGroupId equals st.Id
            //             where (s.Status.Equals("1"))
            //             select new ItemViewModel
            //             {
            //                 Id = s.Id,
            //                 Name = s.Name,
            //                 ItemGroupName = st.ItemGroupName
            //             });
            result.data = new List<Item>();
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
        //internal void FillDropDown()
        //{
        //    List<ItemGroup> ItemGroupList = _dbContext.ItemGroup.Where(w => w.Status.Equals("1")).Select(x => new ItemGroup
        //    {
        //        Id = x.Id,
        //        ItemGroupName = x.ItemGroupName
        //    }).ToList();
        //    ViewBag.ItemGroupNLevelString = new SelectList(ItemGroupList, "Id", "ItemGroupName");
        //}
        internal async void FillDropDown()

        {
            var result = new List<ItemViewModel>();
            try
            {
                using (var db = new SqlConnection(DbConnection))
                {
                    var query = $@"with  nleveltbl(Id,Name,ItemGroupName,ItemGroupId) as
                    (
                        select 
                        id as Id,
                        Name  ,
                        CAST( Name as varchar(max)) as ItemGroupName ,
                        ItemGroupId
                        from po.Item 
                        where ItemGroupId =0
                        UNION ALL
                        select 
                        i.id as Id,
                        i.Name ,
                        CAST( CONCAT(ig.Name,' >> ', i.Name)as varchar(max)) as ItemGroupName,
                        i.ItemGroupId
                        from po.Item i
                        join nleveltbl ig on i.ItemGroupId=ig.Id
                        )
                        select * from nleveltbl";
                    var queryResult = await db.QueryAsync<ItemViewModel>(query);
                    db.Close();
                    var result1 = queryResult.ToList();
                    ViewBag.ItemGroupNLevelString = result1.Select(s => new SelectListItem { Text = s.ItemGroupName, Value = s.Id.ToString() }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
        
        [HttpPost]
        [Route("additemgroup")]
        public IActionResult AddItemGroup(ItemGroup model)
        {
            if (model != null)
            {
                model.Status = "1";
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
                        if(model.ItemGroupNLevelString == "0")
                        {
                            res.ParentItemGroupId = "0";
                            res.ItemGroupNLevelString = model.ItemGroupName;
                            
                        }
                        else
                        {

                           //produces flat sequence

                            res.ParentItemGroupId = model.ItemGroupNLevelString;
                            var a = from category in _dbContext.ItemGroup
                            join prod in _dbContext.ItemGroup on category.Id.ToString() equals prod.ParentItemGroupId
                            select new { c =  category.ItemGroupName + ">>" + prod.ItemGroupName };
                            res.ParentItemGroupId = model.ItemGroupNLevelString;
                            res.ItemGroupNLevelString = a.ToString();//_dbContext.ItemGroup.Where(x => x.Id == model.Id).Select(s => s.ItemGroupName).FirstOrDefault();
                        }
                        
                        res.ItemGroupNLevelString = _dbContext.ItemGroup.Where(x => x.Id == model.Id).Select(s => s.ItemGroupName).FirstOrDefault();
                        res.CreatedDate = DateTime.Now;
                        res.Status = model.Status;
                        res.ModifiedBy = model.ModifiedBy;
                        _dbContext.ItemGroup.Update(res);
                    }

                }
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Create", "item");
        }




        //[HttpGet]
        //[Route("additemingroup")]
        //public async Task<List<ItemGroup>> FillDD()
        //{
        //    var result = new List<ItemGroup>();
        //    try
        //    {
        //        using (var db = new SqlConnection(DbConnection))
        //        {
        //            var query = $@"with  nleveltbl(Id,Name,ItemGroupName,ItemGroupId) as
        //            (
        //                select 
        //                id as Id,
        //                Name  ,
        //                CAST( Name as varchar(max)) as ItemGroupName ,
        //                ItemGroupId
        //                from po.Item 
        //                where ItemGroupId =0
        //                UNION ALL
        //                select 
        //                i.id as Id,
        //                i.Name ,
        //                CAST( CONCAT(ig.Name,' >> ', i.Name)as varchar(max)) as ItemGroupName,
        //                i.ItemGroupId
        //                from po.Item i
        //                join nleveltbl ig on i.ItemGroupId=ig.Id
        //                )
        //                select * from nleveltbl";
        //            var queryResult = await db.QueryAsync<ItemGroup>(query);
        //            db.Close();
        //            result = queryResult.ToList();
        //            ViewBag.ItemGroupList = new SelectList(result.ToList(), "Id", "ItemGroupName");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}
    }
}
