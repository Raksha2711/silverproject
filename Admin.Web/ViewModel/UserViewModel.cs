using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Sales = "Sales";
        public const string Account = "Account";
        public const string Purchase = "Purchase";
        public const string Approver = "Approver";
    }
    
}
