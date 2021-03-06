using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Command.Entity1
{
    public partial class CommandDbContext : IdentityDbContext<SilverlineUser,SilverlineRole,int>
    {
        public CommandDbContext(DbContextOptions<CommandDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<BillMaster> BillMaster { get; set; }
        public virtual DbSet<SalesPerson> SalesPerson { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillItem> BillItems { get; set; }
        public virtual DbSet<ItemGroup> ItemGroup { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Reason> Reason { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SilverlineUser>(i => { i.ToTable("Users", "auth"); });
            modelBuilder.Entity<SilverlineRole>(i => { i.ToTable("Roles", "auth"); });
            modelBuilder.Entity<IdentityUserRole<int>>(i => { i.ToTable("UserRoles", "auth"); });
            modelBuilder.Entity<IdentityUserLogin<int>>(i => { i.ToTable("UserLogins", "auth"); });
            modelBuilder.Entity<IdentityRoleClaim<int>>(i => { i.ToTable("RoleClaims", "auth"); });
            modelBuilder.Entity<IdentityUserClaim<int>>(i => { i.ToTable("UserClaims", "auth"); });
            modelBuilder.Entity<IdentityUserToken<int>>(i => { i.ToTable("UserTokens", "auth"); });
            base.OnModelCreating(modelBuilder);
        }
    }
}
