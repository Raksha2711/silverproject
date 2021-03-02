using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Command.Entity1;

namespace Admin.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<SilverlineUser> _userManager;
        private readonly SignInManager<SilverlineUser> _signInManager;
        public UsersController(UserManager<SilverlineUser> userManager,
            SignInManager<SilverlineUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.Users.Select(s => new RegisterViewModel
            {
                Id=s.Id,
                Email = s.Email,
                Name = s.UserName
            }).ToList();
            return View(user);
        }
        public IActionResult Create()
        {
            return View("~/Views/Users/Create.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            var user = new SilverlineUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return View();
            }
            return View();
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(RegisterViewModel Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new SilverlineUser { UserName = Input.Email, Email = Input.Email, Name = Input.Name };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("~/Views/Account/SignUp.cshtml");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = _userManager.Users.Where(w=>w.Id==id).FirstOrDefault();
            return View("~/Views/Users/Create.cshtml",user);
        }
    }
}
