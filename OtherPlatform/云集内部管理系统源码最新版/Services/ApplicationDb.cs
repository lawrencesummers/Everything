using System.Data.Entity;
using Models.SysModels;
using Models.UserModels;
using Services.SysServices;

namespace Services
{
    public class ApplicationDb : SysApplicationDb
    {
        //指定连接字符串
        public ApplicationDb()
            : base("DefaultConnection")
        {
        }


        //用户实体添加到此文件中

        public DbSet<ProjectInfo> ProjectInfos { get; set; }
        public DbSet<ProjectInfoState> ProjectInfoStates { get; set; } //项目状态
        public DbSet<ProjectInfoReply> ProjectInfoReplys { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectTaskReply> ProjectTaskReplys { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<ProjectFinancial> ProjectFinancials { get; set; }
        public DbSet<Knowledge> Knowledges { get; set; }
        public DbSet<KnowledgeReply> KnowledgeReplys { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Customer> Customer { get; set; } //客户
        public DbSet<CustomerType> CustomerTypes { get; set; } //客户类型
        public DbSet<CustomerLevel> CustomerLevels { get; set; } //客户等级

        public DbSet<BusinessState> BusinessStates { get; set; } //业务状态 
        public DbSet<CustomerCommunication> CustomerCommunications { get; set; } //业务沟通记录

        public DbSet<Flow> Flows { get; set; } //流程

        public DbSet<Activity> Activitys { get; set; } //活动组织报名 
        public DbSet<ActivityUser> ActivityUsers { get; set; } //活动组织报名用户


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //设置字段映射
            modelBuilder.Entity<Plan>()
                .Property(u => u.StartDate)
                .HasColumnName("StarDate");
            //生成存储过程   var destinations = context.Database.SqlQuery<DestinationSummary>("dbo.GetDestinationSummary @p0, @p1", country, keyWords);
            modelBuilder.Entity<SysLog>().MapToStoredProcedures();

        }
    }
}