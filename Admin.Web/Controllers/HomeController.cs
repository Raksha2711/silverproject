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
            var result = _dbContext.Bills.Select(s => new TaskItemViewModel
            {
                No = s.No,
                Id = s.Id,
                Date = s.Date,
                SalesPerson = s.SalesPerson,
                Vendor = s.Vendor,
                DeliveryType = s.DeliveryType,
                DelieveryPlaceId = s.DelieveryPlaceId,
                PaymentTerm = s.PaymentTerm,
                PaymentValue = s.PaymentValue

            }).ToList();
            return View(result);
        }
    }
}
