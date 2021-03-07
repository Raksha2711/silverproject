using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Web.Customization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace Admin.Web.Controllers
{
    public class BaseController : Controller
    {
        IServiceProvider ServiceProvider { get; set; }
        public BaseController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        CustomerContextModel _customerContext;
        
        public CustomerContextModel CustomerContext
        {
            get
            {
                if (_customerContext == null)
                    _customerContext = ServiceProvider.GetService<CustomerContextModel>();
                return _customerContext;
            }
        }
    }
}
