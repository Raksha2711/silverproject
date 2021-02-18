using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Web.Controllers
{
    public class ItemGroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
