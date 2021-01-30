using Command.Entity1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Web.Controllers
{
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
        public IActionResult Index()
        {
            FillDropDown();
            return View();
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/PurchaseOrder/Create.cshtml");
        }
        [HttpPost]
        public ActionResult Create(BillMaster model)
        {
            if (model != null)
            {
                //model.Status = 1;
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    model.Status = 'A';
                    _dbContext.BillMaster.Add(model);
                }
                else
                {
                    _dbContext.BillMaster.Update(model);
                }
                _dbContext.SaveChanges();
            }
            
            FillDropDown();
            return View("~/Views/PurchaseOrder/Index.cshtml");
        }
        
        public IActionResult Detail()
        {
            return View();
        }
    }
}
