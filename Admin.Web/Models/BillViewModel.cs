using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.Entity1;

namespace Admin.Web.Models
{
    public class BillViewModel : Bill
    {
        public string VendorName { get; set; }
        public string SalesPersoName { get; set; }
    }
}
