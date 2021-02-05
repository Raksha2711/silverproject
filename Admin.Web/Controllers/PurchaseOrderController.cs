using Admin.Web.Models;
using Command.Entity1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Web.Controllers
{
    [Route("purchaseorder")]
    public class PurchaseOrderController : Controller
    {
        public CommandDbContext _dbContext;
        public PurchaseOrderController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        internal void FillDropDown()
        {
            List<SalesPerson> SalesPersonList = _dbContext.SalesPerson.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.SalesPerson = new SelectList(SalesPersonList, "Id", "Name");
            List<Warehouse> WarehouseList = _dbContext.Warehouse.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.WareHouse = new SelectList(WarehouseList, "Id", "Name");
            List<Vendor> VendorList = _dbContext.Vendor.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.Vendor = new SelectList(VendorList, "Id", "Name");
            List<Item> ItemList = _dbContext.Item.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.Item = new SelectList(ItemList, "Id", "Name");
            var LatestId = _dbContext.BillMaster.OrderByDescending(n => n.Id).Take(1).Select(s => s.POId).FirstOrDefault();
            ViewBag.LatestId = LatestId;
            //var PaymentTermlist = new List<SelectListItem>();
            //for (var i = 1; i < 50; i++)
            //    PaymentTermlist.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            //ViewBag.PaymentTerm = PaymentTermlist;
        }
        [Route("", Name = "purchaseorder")]
        public IActionResult Index()
        {
            //var result = (from s in _dbContext.Bills
            //              join v in _dbContext.Vendor on s.Vendor equals v.Id
            //              join sp in _dbContext.SalesPerson on s.SalesPerson equals sp.Id

            //              select new TaskItemViewModel
            //              {
            //                  No = s.No,
            //                  Id = s.Id,
            //                  Date = s.Date,
            //                  SalesPersoName = sp.Name,
            //                  VendorName = v.Name,
            //                  DeliveryType = s.DeliveryType,
            //                  DelieveryPlaceId = s.DelieveryPlaceId,
            //                  PaymentTerm = s.PaymentTerm,
            //                  PaymentValue = s.PaymentValue,
            //                  Purchase = s.Purchase,
            //                  Accounts = s.Accounts,
            //                  Approver = s.Approver


            //              }).ToList();
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var m = new Bill();
            int.TryParse(GetPoNo()?.Replace("SIH", ""), out int poNo);
            m.No = string.Format("SIH{0}", poNo + 1);
            m.BillItems = new List<BillItem>();
            m.BillItems.Add(new BillItem());
            FillDropDown();
            return View("~/Views/PurchaseOrder/Create.cshtml", m);
        }
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var result = _dbContext.Bills.Include(i => i.BillItems).Where(w => w.Id.Equals(id)).FirstOrDefault();
            FillDropDown();
            return View("~/Views/PurchaseOrder/Create.cshtml", result);
        }
        #region Api
        [HttpPost("create.json")]
        [AllowAnonymous]
        public IActionResult add([FromBody] Bill model)
        {

            if (ModelState.IsValid && model != null)
            {
                model.CreatedDate = DateTime.Now;
                model.Recstatus = 'A';
                _dbContext.Bills.Add(model);
                _dbContext.SaveChanges();
                return Json(model.Id);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update/{id:int}.json")]
        public IActionResult Update(int id, [FromBody] Bill model)
        {
            if (id > 0 && model != null)
            {
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                return Json(model.Id);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("detail/{id:int}")]
        public IActionResult Detail([FromRoute] int id)
        {
            try
            {
                var result = (from s in _dbContext.Bills.Include(e => e.BillItems)
                              join v in _dbContext.Vendor on s.Vendor equals v.Id
                              join sp in _dbContext.SalesPerson on s.SalesPerson equals sp.Id
                              join wh in _dbContext.Warehouse on s.DelieveryPlaceId equals wh.Id into whl
                              from wh in whl.DefaultIfEmpty()
                              select new BillViewModel
                              {
                                  No = s.No,
                                  BillItemsView =
                                      (from a in s.BillItems
                                       join bi in _dbContext.Item on a.ItemId equals bi.Id
                                       select new BillItemViewModel
                                       {
                                           Id = a.Id,
                                           ItemName = bi.Name,
                                           Qty = a.Qty,
                                           Unit = a.Unit,
                                           BasicRate = a.BasicRate,
                                           AddCost = a.AddCost,
                                           CDC = a.CDC,
                                           Discount1 = a.Discount1,
                                           Scheme1 = a.Scheme1,
                                           Scheme2 = a.Scheme2,
                                           SchemeAmt = a.SchemeAmt,
                                           GSTRate = a.GSTRate,
                                           NLC = a.NLC,
                                           Remarks = a.Remarks,

                                       }).ToList(),
                                  Id = s.Id,
                                  Date = s.Date,
                                  SalesPersoName = sp.Name,
                                  VendorName = v.Name,
                                  DeliveryType = s.DeliveryType,
                                  DelieveryPlaceId = s.DelieveryPlaceId,
                                  PaymentTerm = s.PaymentTerm,
                                  PaymentValue = s.PaymentValue,
                                  WarehouseDetail = wh,
                                  Approver = s.Approver,
                                  Purchase = s.Purchase,
                                  Accounts = s.Accounts

                              }).Where(w => w.Id.Equals(id)).FirstOrDefault();


                return View(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet("additem")]
        public IActionResult AddItem()
        {
            try
            {
                List<Item> ItemList = _dbContext.Item.Where(w => w.Status.Equals("1")).ToList();
                ViewBag.Item = new SelectList(ItemList, "Id", "Name");
                return PartialView("~/Views/PurchaseOrder/_billitem.cshtml", new BillItem());
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        internal string GetPoNo()
        {
            var a = _dbContext.Bills.OrderByDescending(o => o.Id).Select(s => s.No).FirstOrDefault();
            return a;
        }
        [HttpPost]
        [Route("deleterow/{id:int}.json")]
        public IActionResult DeleteRow(int id)
        {
            if (id > 0)
            {
                var result = _dbContext.BillItems.SingleOrDefault(b => b.Id == id);
                if (result != null)
                {
                    result.Recstatus = 'D';
                    _dbContext.SaveChanges();
                }
                //BillItem model = new BillItem();
                //model.Recstatus = 'D';
                //model.Id = id;
                //_dbContext.BillItems.Add(model);

                return Json(id);
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        [Route("forwardpo")]
        public IActionResult ForwardPo(int poid, string status)
        {
            var po = _dbContext.Bills.Where(w => w.Id == poid).FirstOrDefault();
            if (status == "accept")
            {
                if (po.Approver == 0) po.Approver = 1;
                else if (po.Purchase == 0) po.Purchase = 1;
                else if (po.Accounts == 0) po.Accounts = 1;
            }
            else if (status == "cancel")
            {
                po.Recstatus = 'D';
            }
            _dbContext.SaveChanges();
            return RedirectToAction("index", "home");
        }
        [HttpPost]
        [Route("purchase")]
        public IActionResult Purchase(PurchaseRequestModel model)
        {
            var po = _dbContext.Bills.Where(w => w.Id == model.PoId).FirstOrDefault();
            po.PurchaseInvoiceNo = model.InvoceNo;
            po.PurchaseDate = model.InvoceDate ?? DateTime.Now;
            po.GoodReceiveDate = model.GoodRecordDate ?? DateTime.Now;
            po.Purchase = 1;
            _dbContext.SaveChanges();
            return RedirectToAction("index", "home");
        }
        [HttpPost]
        [Route("cancel")]
        public IActionResult Cancel(int poid, string rejectreason)
        {
            var po = _dbContext.Bills.Where(w => w.Id == poid).FirstOrDefault();
            po.Recstatus = 'D';
            po.Rejectreason = rejectreason;
            _dbContext.SaveChanges();
            return RedirectToAction("index", "home");
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
            var result = new DtResult<TaskItemViewModel>();
            //var isSuperAdmin = User.IsSuperAdmin();
            var query = (from s in _dbContext.Bills
                          join v in _dbContext.Vendor on s.Vendor equals v.Id
                          join sp in _dbContext.SalesPerson on s.SalesPerson equals sp.Id

                          select new TaskItemViewModel
                          {
                              No = s.No,
                              Id = s.Id,
                              Date = s.Date,
                              SalesPersoName = sp.Name,
                              VendorName = v.Name,
                              DeliveryType = s.DeliveryType,
                              DelieveryPlaceId = s.DelieveryPlaceId,
                              PaymentTerm = s.PaymentTerm,
                              PaymentValue = s.PaymentValue,
                              Purchase = s.Purchase,
                              Accounts = s.Accounts,
                              Approver = s.Approver


                          });

            result.data = new List<TaskItemViewModel>();
            result.recordsTotal = query.Count();

            if (param.Search != null && !string.IsNullOrEmpty(param.Search.Value))
            {
                var keyword = param.Search.Value;
                query = query.Where(w => w.SalesPersoName.Contains(keyword));// ||
                //w.Entity.Value.Contains(keyword) || w.PromoterName.Contains(keyword));
            }

            //if (param.Columns.Any(w => !string.IsNullOrEmpty(w.Search.Value)))
            //{
            //    foreach (var item in param.Columns.Where(w => !string.IsNullOrEmpty(w.Search.Value)).ToList())
            //    {
            //        if (item.Data != null && !string.IsNullOrEmpty(item.Search.Value) && item.Name.Equals(nameof(TaskItemViewModel.VendorName)))
            //        {
            //            var promoterId = Convert.ToInt32(item.Search.Value.ToLower().Trim());
            //            query = query.Where(w => w.Entity.PromoterId == promoterId);
            //        }
            //    }
            //}

            //if (param.Order != null && param.Order.Length > 0)
            //{
            //    foreach (var item in param.Order)
            //    {
            //        if (param.Columns[item.Column].Data.Equals(nameof(PromoterConfig.Key), StringComparison.OrdinalIgnoreCase))
            //            query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Entity.Key) : query.OrderBy(o => o.Entity.Key);
            //        else if (param.Columns[item.Column].Data.Equals(nameof(PromoterConfig.Value), StringComparison.OrdinalIgnoreCase))
            //            query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.Entity.Value) : query.OrderBy(o => o.Entity.Value);
            //        else if (param.Columns[item.Column].Data.Equals("PromoterName", StringComparison.OrdinalIgnoreCase))
            //            query = item.Dir == DTOrderDir.DESC ? query.OrderByDescending(o => o.PromoterName) : query.OrderBy(o => o.PromoterName);
            //    }
            //}

            result.recordsFiltered = query.Count();
            if (param.Length > 0)
            {
                query = query.Skip(param.Start).Take(param.Length);
            }

            var entries = query.ToList();

            foreach (var e in entries)
            {
                result.data.Add(e);
            }
            return result;
        }
    }
}
