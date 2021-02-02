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

            return View(_dbContext.Bills.ToList());
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
            return View();
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

        internal string GetPoNo() { return _dbContext.Bills.OrderBy(o => o.Id).Select(s => s.No).FirstOrDefault(); }
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
    }
}
