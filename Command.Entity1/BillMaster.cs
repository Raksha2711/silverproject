﻿using System;
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
        public string POId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public int SalesPersonName { get; set; }
        public int VendorName { get; set; }
        public string PickUpDel { get; set; }
        [StringLength(100)]
        public string DelieveryPlace { get; set; }
        public int DelieveryPlaceId { get; set; }
        public string PaymentTerm { get; set; }
        public int PaymentValue { get; set; }

        public int ItemId { get; set; }
        public int Qty { get; set; }
        [StringLength(3)]
        public string Unit { get; set; }
        public double BasicRate { get; set; }
        public double AddCost { get; set; }
        public double CDC { get; set; }
        public double Discount1 { get; set; }
        public double Scheme1 { get; set; }
        public double Scheme2 { get; set; }
        public double SchemeAmt { get; set; }
        public int GSTRate { get; set; }
        public double NLC { get; set; }
        [StringLength(150)]
        public string Remarks { get; set; }
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
        public char  Status {get;set;}
    }
}
