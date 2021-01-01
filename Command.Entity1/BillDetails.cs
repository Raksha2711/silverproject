using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Command.Entity1
{
   
    [Table("BillDetails", Schema = "po")]
    public partial class BillDetails
    {
        [Key]
        public int Id { get; set; }
        //[Column(TypeName = "datetime")]
        public int BillId { get; set; }
        // [StringLength(64)]
        public int ItemId { get; set; }
        //[StringLength(20)]
        public int Qty { get; set; }
        //[Column(TypeName = "datetime")]
        [StringLength(3)]
        public int Unit { get; set; }
        public double BasicRate { get; set; }
        public double AddCost { get; set; }
        public double CDC { get; set; }
        public double Discount1 { get; set; }
        public double Scheme1 { get; set; }
        public double Scheme2 { get; set; }
        public int GSTRate { get; set; }
        public double NLC { get; set; }
        [StringLength(150)]
        public int Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }

}
