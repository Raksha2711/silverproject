using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Command.Entity1
{
    public partial class CommandDbContext :DbContext
    {
        public CommandDbContext(DbContextOptions<CommandDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<BillMaster> BillMaster { get; set; }
        //public virtual DbSet<BillMaster> BillMaster { get; set; }
        //public virtual DbSet<Item> Item { get; set; }

    }
}
