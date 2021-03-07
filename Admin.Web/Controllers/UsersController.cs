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
            var users = (from u in _dbContext.Users
                         join rj in _dbContext.UserRoles on u.Id equals rj.UserId
                         join r in _dbContext.Roles on rj.RoleId equals r.Id
                         select new RegisterViewModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             Name = u.Name,
                             Role = r.Name,
                             RecStatus=u.RecStatus
                         }).ToList();
            return View(users);
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.rolelist = GetRoles();
            return View();
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(RegisterViewModel Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            //if (ModelState.IsValid)
            //{
            var user = new SilverlineUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                Name = Input.Name,
                PhoneNumber = Input.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                var usr = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(usr, Input.Role);
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet("edit/{id:int}")]
        public IActionResult Update(int id)
        {
            ViewBag.rolelist = GetRoles();

            var user = (from u in _dbContext.Users
                        join urole in _dbContext.UserRoles on u.Id equals urole.UserId
                        join role in _dbContext.Roles on urole.RoleId equals role.Id
                        where u.Id.Equals(id)
                        select new UpdateUserViewModel
                        {
                            Id = u.Id,
                            Email = u.Email,
                            Name = u.Name,
                            PhoneNumber = u.PhoneNumber,
                            Role = role.Name,
                            RecStatus=u.RecStatus
                        }).FirstOrDefault();
            return View("~/Views/Users/Update.cshtml", user);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Update(UpdateUserViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;

            await _userManager.UpdateAsync(user);
            var role = await _userManager.GetRolesAsync(user);
            if (!role.Contains(model.Role))
            {
                await _userManager.RemoveFromRoleAsync(user, role.FirstOrDefault());
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, model.Password);
            }
            return RedirectToAction("index");
        }

        

        [Route("deleterow/{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _dbContext.Users.Where(w => w.Id.Equals(id)).FirstOrDefault();
            result.RecStatus = (result.RecStatus == 'A' || result.RecStatus==null) ? 'D' : 'A';
            
            _dbContext.Users.Update(result);
            _dbContext.SaveChanges();

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
