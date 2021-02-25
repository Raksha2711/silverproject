using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Command.Entity1
{
    [Table("Item", Schema = "po")]
    public partial class Item
    {
        [Key]
        public int Id { get; set; }
        public int ItemGroupId { set; get; }
        //public virtual ItemGroup ItemGroup { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(20)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
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
        [StringLength(1)]
        public string Status { get; set; }
    }
}
