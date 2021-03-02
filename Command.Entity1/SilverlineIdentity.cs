using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Command.Entity1
{
    public class SilverlineUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
    }
    public class SilverlineRole : IdentityRole<int>
    {
        //public const string Admin = "Admin";
        //public const string Sales = "Sales";
        //public const string Account = "Account";
        //public const string Purchase = "Purchase";
        //public const string Approver = "Approver";
    }
}
