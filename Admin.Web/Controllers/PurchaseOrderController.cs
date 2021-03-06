﻿using Admin.Web.Models;
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
using Microsoft.AspNetCore.Http;

namespace Admin.Web.Controllers
{
    //[Authorize]
    [Route("purchaseorder")]
    public class PurchaseOrderController : BaseController
    {
        public CommandDbContext _dbContext;

        public PurchaseOrderController(IServiceProvider serviceProvider, CommandDbContext dbContext) : base(serviceProvider)
        {
            _dbContext = dbContext;
        }
        internal void FillDropDown()
        {
            //List<SalesPerson> SalesPersonList = _dbContext.SalesPerson.Where(w => w.Status.Equals("1")).ToList();
            //ViewBag.SalesPerson = new SelectList(SalesPersonList, "Id", "Name");
            List<Warehouse> WarehouseList = _dbContext.Warehouse.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.WareHouse = new SelectList(WarehouseList, "Id", "Name");
            List<Vendor> VendorList = _dbContext.Vendor.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.Vendor = new SelectList(VendorList, "Id", "Name");
            List<ItemGroup> ItemList = _dbContext.ItemGroup.Where(w => w.Status.Equals("1") && !w.ParentItemGroupId.Equals("0")).ToList();
            ViewBag.Item = new SelectList(ItemList, "Id", "ItemGroupName");
            var LatestId = _dbContext.BillMaster.OrderByDescending(n => n.Id).Take(1).Select(s => s.POId).FirstOrDefault();
            ViewBag.LatestId = LatestId;

        }
        [Authorize]
        [Route("", Name = "purchaseorder")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Sales")]
        [HttpGet("create")]
        public IActionResult Create()
        {
            var m = new Bill();
            int.TryParse(GetPoNo()?.Replace("SIH", ""), out int poNo);
            m.No = string.Format("SIH{0}", poNo + 1);
            m.BillItems = new List<BillItem>();
            m.BillItems.Add(new BillItem());
            FillDropDown();
            ViewBag.Personname = GetName(CustomerContext.UserId);
            return View("~/Views/PurchaseOrder/Create.cshtml", m);
        }
        [Authorize(Roles = "Admin,Sales")]
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var result = _dbContext.Bills.Include(i => i.BillItems).Where(w => w.Id.Equals(id) && w.Recstatus.Equals('A')).FirstOrDefault();
            FillDropDown();
            return View("~/Views/PurchaseOrder/Create.cshtml", result);
        }
        #region Api
        [Authorize(Roles = "Admin,Sales")]
        [HttpPost("create.json")]
        public IActionResult add([FromBody] Bill model)
        {
            var No = _dbContext.Bills.Where(w => w.No.Equals(model.No)).FirstOrDefault();
            if (ModelState.IsValid && model != null && No == null)
            {
                model.Date = DateTime.Now;
                model.CreatedDate = DateTime.Now;
                model.Recstatus = 'A';
                model.CreatedBy = CustomerContext.UserId.ToString();
                model.SalesPerson = CustomerContext.UserId;
                _dbContext.Bills.Add(model);
                _dbContext.SaveChanges();
                TempData["success"] = $"Purchase Invoice No:{model.No} created successfully";
                return Json(model.Id);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("detail/{id:int}")]
        public IActionResult Detail([FromRoute] int id)
        {
            var result = GetPoDetail(id);
            ViewBag.detail = true;
            List<Reason> ReasonList = _dbContext.Reason.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.Reason = new SelectList(ReasonList, "Id", "Name");
            return View(result);

        }
        [HttpGet("view/{id:int}")]
        public IActionResult View([FromRoute] int id)
        {
            ViewBag.detail = false;
            var result = GetPoDetail(id);
            return View("~/Views/PurchaseOrder/Detail.cshtml", result);

        }
        [Authorize(Roles = "Admin,Sales")]
        [HttpPost]
        [Route("update/{id:int}.json")]
        public IActionResult Update(int id, [FromBody] Bill model)
        {
            if (id > 0 && model != null)
            {
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                TempData["success"] = $"Purchase Invoice No:{model.No} updated successfully";
                return Json(model.Id);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("additem")]
        public IActionResult AddItem()
        {
            try
            {
                List<ItemGroup> ItemList = _dbContext.ItemGroup.Where(w => w.Status.Equals("1")).ToList();
                ViewBag.Item = new SelectList(ItemList, "Id", "ItemGroupName");
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
                if (po.Approver == 0) { po.Approver = 1; SendMail(poid); }
                else if (po.Purchase == 0) po.Purchase = 1;
                else if (po.Accounts == 0) po.Accounts = 1;
                TempData["success"] = $"purchase Invoice No:{po.No} successfully confirm.";
            }
            else if (status == "cancel")
            {
                po.Recstatus = 'D';
            }
            _dbContext.SaveChanges();
            return RedirectToAction("index", "home");
        }
        [Authorize(Roles = "Admin,Purchase")]
        [HttpPost]
        [Route("purchase")]
        public IActionResult Purchase(PurchaseRequestModel model)
        {
            //SendMail();
            var po = _dbContext.Bills.Where(w => w.Id == model.PoId).FirstOrDefault();
            po.PurchaseInvoiceNo = model.InvoceNo;
            po.PurchaseDate = model.InvoceDate ?? DateTime.Now;
            po.GoodReceiveDate = model.GoodRecordDate ?? DateTime.Now;
            po.Purchase = 1;
            _dbContext.SaveChanges();
            TempData["success"] = $"Purchase Invoice No:{model.InvoceNo} successfully confirm.";
            return RedirectToAction("index", "home");
        }
        internal void SendMail(int poid)
        {
            try
            {
                var poDetail = GetPoDetail(poid);
                var html = this.RenderViewAsync("_popdf", poDetail).Result;
                if (!string.IsNullOrWhiteSpace(poDetail.Email))
                {
                    Email.Send(poDetail.Email,poDetail.Email2,
                        $"Silverline IT HUB - Purchase Order (PO NO : {poDetail.No})",
                        $"Hello Team,{System.Environment.NewLine}",
                        false, $"PurchaseOrder-{poDetail.No}.pdf",
                        PdfHelper.ConvertToPdf(html)).Wait();
                }
            }
            catch (Exception ex)
            {
            }
        }
        [Route("resensemail/{poid}")]
        public IActionResult ReSendEmail(int poid)
        {
            SendMail(poid);
            return RedirectToAction("Index", "Home");
        }
        internal BillViewModel GetPoDetail(int id)
        {
            var result = (from s in _dbContext.Bills.Include(e => e.BillItems)
                          join v in _dbContext.Vendor on s.Vendor equals v.Id
                          join sp in _dbContext.Users on s.SalesPerson equals sp.Id
                          join wh in _dbContext.Warehouse on s.DelieveryPlaceId equals wh.Id into whl
                          from wh in whl.DefaultIfEmpty()
                          select new BillViewModel
                          {
                              No = s.No,
                              BillItemsView =
                                  (from a in s.BillItems
                                   join bi in _dbContext.ItemGroup on a.ItemId equals bi.Id
                                   select new BillItemViewModel
                                   {
                                       Id = a.Id,
                                       ItemName = bi.ItemGroupName,
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
                              VendorAddress = v.Address,
                              DeliveryAddress = wh.Address,
                              Email = v.EmailId,
                              Email2 = v.EmailId2,
                              DeliveryType = s.DeliveryType,
                              DelieveryPlaceId = s.DelieveryPlaceId,
                              PaymentTerm = s.PaymentTerm,
                              PaymentValue = s.PaymentValue,
                              WarehouseDetail = wh,
                              Approver = s.Approver,
                              Purchase = s.Purchase,
                              Accounts = s.Accounts

                          }).Where(w => w.Id.Equals(id)).FirstOrDefault();
            return result;
        }
        [HttpPost]
        [Route("cancel")]
        public IActionResult Cancel(int poid, string rejectreason)
        {
            var po = _dbContext.Bills.Where(w => w.Id == poid).FirstOrDefault();
            po.Recstatus = 'D';
            po.Rejectreason = rejectreason;
            _dbContext.SaveChanges();
            TempData["warring"] = $"Purchase Invoice No:{po.No} was canceled.";
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
            

            var query = (from s in _dbContext.Bills
                         join v in _dbContext.Vendor on s.Vendor equals v.Id
                         join sp in _dbContext.Users on s.SalesPerson equals sp.Id 
                         join r in _dbContext.Reason on s.Rejectreason equals r.Id.ToString() into abc
                         from r in abc.DefaultIfEmpty()
                         orderby s.Id descending
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
                             Approver = s.Approver,
                             Recstatus = s.Recstatus,
                             Rejectreason = r.Name,

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

        [HttpPost]
        [Route("GetDataById")]
        public JsonResult GetDataById(int vendorId)
        {
            var res = _dbContext.Vendor.Where(x => x.Id == vendorId).FirstOrDefault();
            return Json(res);
        }
        //[HttpPost]
        //[Route("fillreason")]
        //public IActionResult fillreason()
        //{
        //    List<Reason> ReasonList = _dbContext.Reason.Where(w => w.Status.Equals("1")).ToList();
        //    ViewBag.Reason = new SelectList(ReasonList, "Id", "Name");
        //    return View();//BadRequest(ModelState);
        //}
        internal string GetName(int id)
        {
            return _dbContext.Users.Where(w => w.Id.Equals(id)).Select(s => s.Name).FirstOrDefault();
        }
    }
}
