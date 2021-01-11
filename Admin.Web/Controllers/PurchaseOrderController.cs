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
        public IActionResult Index()
        {
            List<SalesPerson> SalesPersonList = _dbContext.SalesPerson.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.SalesPerson = new SelectList(SalesPersonList, "Id", "Name");
            List<Vendor> VendorList = _dbContext.Vendor.Where(w => w.Status.Equals("1")).ToList();
            ViewBag.Vendor = new SelectList(VendorList, "Id", "Name");
            var PaymentTermlist = new List<SelectListItem>();
            for (var i = 1; i < 50; i++)
            PaymentTermlist.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            ViewBag.PaymentTerm = PaymentTermlist;
            return View();
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/PurchaseOrder/Create.cshtml");
        }
        [HttpPost]
        public IActionResult Create(BillMaster model)
        {
            if (model != null)
            {
                //model.Status = 1;
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    _dbContext.BillMaster.Add(model);
                }
                else
                {
                    _dbContext.BillMaster.Update(model);
                }
                _dbContext.SaveChanges();
            }
            return View("~/Views/PurchaseOrder/Index.cshtml");
        }
    }
}
