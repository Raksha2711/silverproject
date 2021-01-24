using Command.Entity1;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Web.Controllers
{
    public class LoginController : Controller
    {
        public CommandDbContext _dbContext;
        public LoginController(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
