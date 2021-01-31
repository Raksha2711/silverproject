using System;
using System.Collections.Generic;
using System.Text;

namespace Command.Entity1
{
    public interface ICreateAuditable
    {
        string CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
    }
    public interface IUpdateAuditable
    {
        string ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
    
}
