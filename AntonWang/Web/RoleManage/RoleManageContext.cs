using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RoleManage.Migrations;
using RoleManage.Models;

namespace RoleManage
{
    public class RoleManageContext : DbContext
    {
        public RoleManageContext()
            : base("RoleManageContext")
        {
        }
        static RoleManageContext()
        {
            //初始化时自动更新迁移到最新版本
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RoleManageContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            builder.Entity<User>().HasMany(s => s.Roles).WithMany(s => s.Users).Map(s=>s.ToTable("UserRole"));
            builder.Entity<Role>().HasMany(s => s.Menus).WithMany(s => s.Roles);
            builder.Entity<Role>().HasMany(s => s.Functions).WithMany(s => s.Roles);
          //  builder.Entity<Function>().HasOptional(s=>s.Menu).

            builder.Entity<Function>()
                .HasRequired(s => s.Menu)
                .WithMany(s => s.Functions)
                .HasForeignKey(s => s.MenuId);
        }

        public DbSet<Function> Function { get; set; }
        // New code:
        public DbSet<Menu> Menu { get; set; }
        // New code:
        public DbSet<Role> Role { get; set; }

        public DbSet<User> User { get; set; }

    }
}