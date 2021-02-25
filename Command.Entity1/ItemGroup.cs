using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Command.Entity1
{
    [Table("ItemGroup", Schema = "po")]
    public partial class ItemGroup
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string ItemGroupName { get; set; }
        public string ParentItemGroupId { get; set; }
        [StringLength(500)]
        public string ItemGroupNLevelString { get; set; }
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
        public ICollection<Item> Item { get; set; }
    }
}
