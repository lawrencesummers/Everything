using System.Data.Entity;
using System.Threading.Tasks;
using Models.SysModels;

namespace Services.SysServices
{
    public abstract class SysApplicationDb : DbContext
    {
        protected SysApplicationDb(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<User>()
        //    //    .Property(u => u.DisplayName)
        //    //    .HasColumnName("display_name");
        //}

        public DbSet<SysEnterprise> SysEnterprises { get; set; }
        public DbSet<SysDepartment> SysDepartments { get; set; }
        public DbSet<SysDepartmentSysUser> SysDepartmentSysUsers { get; set; }
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysRoleSysUser> SysRoleSysUsers { get; set; }
        public DbSet<SysUserLog> SysUserLogs { get; set; }
       
        public DbSet<SysLog> SysLogs { get; set; }
        public DbSet<SysArea> SysAreas { get; set; }
        public DbSet<SysController> SysControllers { get; set; }
        public DbSet<SysControllerSysAction> SysControllerSysActions { get; set; }
        public DbSet<SysAction> SysActions { get; set; }
        public DbSet<SysRoleSysControllerSysAction> SysRoleSysControllerSysActions { get; set; }
        public DbSet<SysUserResetPassword> SysUserResetPasswords { get; set; }
        public DbSet<SysHelp> SysHelps { get; set; }
        public DbSet<SysUploadFile> SysUploadFiles { get; set; }
        public DbSet<SysMail> SysMails { get; set; }
        public DbSet<SysSignalR> SysSignalRs { get; set; }
        public DbSet<SysSignalROnline> SysSignalROnlines { get; set; }

        public virtual int Commit()
        {
            return base.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}