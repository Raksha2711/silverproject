using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.Entity1;

namespace Admin.Web.Models
{
    public class TaskItemViewModel : Bill
    {
        public bool CanEdit { get; set; }
        public bool CanReSendEmail { get; set; }
        public string VendorName { get; set; }
        public string SalesPersoName { get; set; }
        
    }
}
