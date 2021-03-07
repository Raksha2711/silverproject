using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Web.Customization
{
    public class CustomerContextModel
    {
        public CustomerContextModel(IHttpContextAccessor contextAccessor)
        {
            ClaimsPrincipal user = contextAccessor.HttpContext.User;
            
            var userid = user.Claims.Where(w => w.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            UserId = userid != null ? Convert.ToInt32(userid.Value) : 0;

            var email = user.Claims.Where(w => w.Type == ClaimTypes.Name).FirstOrDefault();
            Email = email != null ? email.Value : string.Empty;

            var mobile = user.Claims.Where(w => w.Type == ClaimTypes.MobilePhone).FirstOrDefault();
            Mobile = mobile != null ? mobile.Value : string.Empty;

            var role = user.Claims.Where(w => w.Type == ClaimTypes.Role).FirstOrDefault();
            Role = role != null ? role.Value : string.Empty;


        }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
    }
}
