using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Command.Entity1
{
    public partial class CommandDbContext : IdentityDbContext<IdentityUser>
    {
        public CommandDbContext(DbContextOptions<CommandDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<BillMaster> BillMaster { get; set; }
        public virtual DbSet<SalesPerson> SalesPerson { get; set; }
        //public virtual DbSet<BillDetails> BillDetails { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        //public virtual DbSet<UserData> UserData { get; set; }
        // public virtual DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>(i => { i.ToTable("Users", "auth"); });
            modelBuilder.Entity<IdentityRole>(i => { i.ToTable("Roles", "auth"); });
            modelBuilder.Entity<IdentityUserRole<string>>(i => { i.ToTable("UserRoles", "auth"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(i => { i.ToTable("UserLogins", "auth"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(i => { i.ToTable("RoleClaims", "auth"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(i => { i.ToTable("UserClaims", "auth"); });
            modelBuilder.Entity<IdentityUserToken<string>>(i => { i.ToTable("UserTokens", "auth"); });
            base.OnModelCreating(modelBuilder);
        }
    }
}
