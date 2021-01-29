using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Command.Entity1
{
    [Table("Warehouse", Schema = "po")]
    public partial class Warehouse
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Address { get; set; }

        
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
        [StringLength(1)]
        public string Status { get; set; }
    }
}
