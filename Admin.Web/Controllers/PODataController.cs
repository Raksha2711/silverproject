using Admin.Web.Models;
using Command.Entity1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Admin.Web.Helper;
using System.Globalization;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Dapper;

namespace Admin.Web.Controllers
{
    [Route("podata")]
    public class PODataController : Controller
    {
        public CommandDbContext _dbContext;
        public static string DbConnection { get; set; }
        public PODataController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
            DbConnection = "Data Source=sql5050.site4now.net;User ID=DB_A6EA60_Raksha2710_admin;Password=Mazda@123;";
        }
        [Route("", Name = "podata")]
        public IActionResult Index()
        
        {
            return View();
        }
        [HttpPost]
        [Route("list.json")]
        public IActionResult List(DTParameters param)
        {
            var result = GetList(param);
            result.draw = param.Draw;
            return Json(result);
        }
        internal DtResult<TaskItemViewModel> GetList(DTParameters param)
        {
//            var result1 = new DtResult<TaskItemViewModel>();
//            try
//            {
//                using (var db = new SqlConnection(DbConnection))
//                {
//                    var query1 = $@"select convert(Date,CreatedDate,103),count(case when Approver=1 then (approver) else 0 end),count(case when purchase=1 then (purchase) else 0 end) TotalPurchasePO,count(case when Accounts=1 then (Accounts) else 0 end) as TotalAccountPO from po.bill where convert(date,createddate,103) between convert(date,'03/03/2021',103) and convert(date,'04/03/2021',103)
//group by convert(Date,CreatedDate,103)";
//                    var queryResult =  db.QueryAsync<TaskItemViewModel>(query1);
//                    db.Close();
//                    result1.data = new List<TaskItemViewModel>();
//                    result1.recordsTotal = query1.Count();
//                   // result = queryResult.ToList();
//                    //result = qresult.Select(s => new SelectListItem { Text = s.ItemGroupNLevelString, Value = s.Id.ToString() }).ToList();
//                }
//                //return result1;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

           
                var result = new DtResult<TaskItemViewModel>();
            var a = _dbContext.Bills.Where(w => w.CreatedDate >= DateTime.Now).GroupBy(g => g.CreatedDate).Select(s => s.Key);
            
            var query = (from s in _dbContext.Bills
                        // where DateTime.Pa(s.CreatedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                         group s by s.CreatedDate into g
                         select new TaskItemViewModel
                         {
                             Date =  g.Key,
                             Purchase = g.Sum(x=>x.Purchase),
                             Accounts = g.Sum(x => x.Accounts),
                             Approver = g.Sum(x => x.Approver),
                         });
            result.data = new List<TaskItemViewModel>();
            result.recordsTotal = query.Count();

            var searchColumn = (from sr in param.Columns where !string.IsNullOrWhiteSpace(sr.Search.Value) select sr).ToList();
            if (searchColumn?.Count() > 0)
            {
                foreach (var item in searchColumn)
                {
                    if (item.Name.ToLower().Equals("start"))
                    {
                        if (!string.IsNullOrWhiteSpace(item.Search.Value))
                            query = query.Where(w => w.Date >= DateTime.ParseExact(item.Search.Value.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).OrderByDescending(o => o.Id);

                    }
                    if (item.Name.ToLower().Equals("end"))
                    {
                        if (!string.IsNullOrWhiteSpace(item.Search.Value))
                            query = query.Where(w => w.Date <= DateTime.ParseExact(item.Search.Value.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture).AddHours(23)).OrderByDescending(o => o.Id);
                    }

                }
            }

            if (param.Search != null && !string.IsNullOrEmpty(param.Search.Value))
            {
                var keyword = param.Search.Value;
                query = query.Where(w => w.SalesPersoName.Contains(keyword));// ||
                                                                             //w.Entity.Value.Contains(keyword) || w.PromoterName.Contains(keyword));
            }
            if (param.Order != null && param.Order.Length > 0)
            {
                foreach (var item in param.Order)
                {
                    //if (param.Columns[item.Column].Data.Equals("no"))
                    //    query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Id) : query.OrderBy(o => o.Id);
                    //else if (param.Columns[item.Column].Data.Equals("vendorName"))
                    //    query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.VendorName) : query.OrderBy(o => o.VendorName);
                    //else if (param.Columns[item.Column].Data.Equals("deliveryType"))
                    //    query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.DeliveryType) : query.OrderBy(o => o.DeliveryType);
                    //else if (param.Columns[item.Column].Data.Equals("paymentTerm"))
                    //    query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.PaymentTerm) : query.OrderBy(o => o.PaymentTerm);
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
