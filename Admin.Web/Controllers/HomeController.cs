using Admin.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Command.Entity1;

namespace Admin.Web.Controllers
{
    //[Authorize(Roles = "PowerUser")]
    public class HomeController : Controller
    {
        public CommandDbContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CommandDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var result = (from s in _dbContext.Bills
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
                              PaymentValue = s.PaymentValue

                          }).ToList();
            return View(result);
        }
    }
}
