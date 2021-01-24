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
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
       // [StringLength(64)]
        public int SalesPersonName { get; set; }
        //[StringLength(20)]
        public int VendorName { get; set; }
        //[Column(TypeName = "datetime")]
        public int PickUpDel { get; set; }
        [StringLength(100)]
        public string DelieveryPlace { get; set; }
        public int PaymentTerm { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }

        private DateTime? dateCreated = null;
        public int ModifiedBy { get; set; }
    }
    
}
