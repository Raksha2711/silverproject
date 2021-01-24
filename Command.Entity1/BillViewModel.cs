using System;
using System.Collections.Generic;
using System.Text;

namespace Command.Entity1
{
    public class BillViewModel : BillMaster
    {
       List<BillDetails> billdetails { get; set; }
    }
}
