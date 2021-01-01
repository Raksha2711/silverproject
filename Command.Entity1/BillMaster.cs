using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Command.Entity1
{
   
        [Table("BillMaster", Schema = "po")]
        public partial class BillMaster
        {
        [Key]
        public int Id { get; set; }
        //[StringLength(64)]
        //public string PONo { get; set; }

        //[Column(TypeName = "datetime")]
        //public DateTime CreatedDate { get; set; }
        //[StringLength(64)]
        //    public string PONo { get; set; }
        //    [StringLength(20)]
        //    public string CreatedBy { get; set; }

        //    [Column(TypeName = "datetime")]
        //    public DateTime CreatedDate { get; set; }

        //    [StringLength(50)]
        //    public string ModifiedBy { get; set; }
        //    [Column(TypeName = "datetime")]
        //    public DateTime ModifiedDate { get; set; }
        //    [StringLength(1)]
        //    public string Status { get; set; }
    }
    
}
