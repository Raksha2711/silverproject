using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Command.Entity1;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Admin.Web.Controllers
{
    //[Authorize(Roles = Role)]
    [Authorize(Roles = "Admin")]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly UserManager<SilverlineUser> _userManager;
        private readonly SignInManager<SilverlineUser> _signInManager;
        public CommandDbContext _dbContext;
        //private readonly RoleManager<SilverlineRole> _roleManager;
        public UsersController(UserManager<SilverlineUser> userManager,
            SignInManager<SilverlineUser> signInManager,
            CommandDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }
        [Route("", Name = "users")]
        public IActionResult Index()
        {
            var user = _userManager.Users.Select(s => new SilverlineUser
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.UserName
            }).ToList();
            //var resu = await _roleManager.CreateAsync(new SilverlineRole { Name = "Sales" });
            return View(user);
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.rolelist = GetRoles();
            return View();
        }
        [HttpGet("edit/{id:int}")]
        public IActionResult Update(int id)
        {
            ViewBag.rolelist = GetRoles();

            var user = (from u in _dbContext.Users
                        join urole in _dbContext.UserRoles on u.Id equals urole.UserId
                        join role in _dbContext.Roles on urole.RoleId equals role.Id
                        where u.Id.Equals(id)
                        select new RegisterViewModel
                        {
                            Id = u.Id,
                            Email = u.Email,
                            Name = u.Name,
                            Role=role.Name
                        }).FirstOrDefault();
            return View("~/Views/Users/Update.cshtml", user);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Update(RegisterViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            user.Name = model.Name;
            var role = await _userManager.GetRolesAsync(user);
            if (!role.Contains(model.Role))
            {
                await _userManager.RemoveFromRoleAsync(user, role.FirstOrDefault());
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(RegisterViewModel Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            //if (ModelState.IsValid)
            //{
                var user = new SilverlineUser { UserName = Input.Email, Email = Input.Email, Name = Input.Name };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var usr = await _userManager.FindByNameAsync(user.UserName);

                    await _userManager.AddToRoleAsync(usr, Input.Role);

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            //}
            return RedirectToAction("Index");
        }
        internal List<SelectListItem> GetRoles()
        {
            var result = _dbContext.Roles.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Name
            }).ToList();
            return result;
        }

    }
}
