using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Command.Entity1
{
    [Table("Vendor", Schema = "po")]
    public partial class Vendor
    {
        [Key]
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(100)]
        public string EmailId { get; set; }
        [StringLength(15)]
        public string ContactNo { get; set; }
        [StringLength(20)]
        public string CreatedBy { get; set; }
        [StringLength(50)]
        public string ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [StringLength(1)]
        public string Status { get; set; }
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
        public string ContactPerson { get; set; }
    }
}
