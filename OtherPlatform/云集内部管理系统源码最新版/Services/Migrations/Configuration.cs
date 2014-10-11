using System.Data.Entity.Migrations;
using System.Linq;
using Models.SysModels;

namespace Services.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //ContextKey = "Services.ApplicationDb";
        }

        protected override void Seed(ApplicationDb context)
        {
            //  This method will be called after migrating to the latest version.

            #region SysArea

            var sysAreas = new[]
            {
                new SysArea
                {
                    AreaName = "Login",
                    AreaDisplayName = "用户登录",
                    SystemId = "000"
                },
                new SysArea
                {
                    AreaName = "Platform",
                    AreaDisplayName = "操作平台",
                    SystemId = "001"
                },
                new SysArea
                {
                    AreaName = "Admin",
                    AreaDisplayName = "管理平台",
                    SystemId = "002"
                }
            };
            context.SysAreas.AddOrUpdate(a => new {a.AreaName, a.AreaDisplayName, a.SystemId}, sysAreas);

            #endregion

            #region SysAction

            var sysActions = new[]
            {
                new SysAction
                {
                    ActionDisplayName = "列表",
                    ActionName = "Index",
                    SystemId = "001"
                },
                new SysAction
                {
                    ActionDisplayName = "详细",
                    ActionName = "Details",
                    SystemId = "003"
                },
                new SysAction
                {
                    ActionDisplayName = "新建",
                    ActionName = "Create",
                    SystemId = "004"
                },
                new SysAction
                {
                    ActionDisplayName = "编辑",
                    ActionName = "Edit",
                    SystemId = "005"
                },
                new SysAction
                {
                    ActionDisplayName = "删除",
                    ActionName = "Delete",
                    SystemId = "006"
                },
                new SysAction
                {
                    ActionDisplayName = "导入",
                    ActionName = "Import",
                    SystemId = "007"
                },
                new SysAction
                {
                    ActionDisplayName = "导出",
                    ActionName = "Export",
                    SystemId = "008"
                }
            };
            context.SysActions.AddOrUpdate(a => new {a.ActionName, a.SystemId, a.ActionDisplayName}, sysActions);

            #endregion

            #region SysController

            var sysControllers = new[]
            {
                // Platform
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "操作平台",
                    ControllerName = "Index",
                    SystemId = "100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "通知公告",
                    ControllerName = "Message",
                    SystemId = "101",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "在线沟通",
                    ControllerName = "Chat",
                    SystemId = "102",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "管理平台",
                    ControllerName = "Index",
                    SystemId = "100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "桌面",
                    ControllerName = "Desktop",
                    SystemId = "100100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "桌面",
                    ControllerName = "Desktop",
                    SystemId = "100100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "使用帮助",
                    ControllerName = "Help",
                    SystemId = "100200",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "使用帮助",
                    ControllerName = "Help",
                    SystemId = "100200",
                    Display = false,
                },


                //前台新的功能

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "计划",
                    ControllerName = "MyPlan",
                    SystemId = "200",
                    Ico = "fa-clock-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "我的计划",
                    ControllerName = "MyPlan",
                    SystemId = "200100",
                    Ico = "fa-clock-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "我的任务",
                    ControllerName = "MyProjectTask",
                    SystemId = "200200",
                    Ico = "fa-tasks"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "我的日历",
                    ControllerName = "MyCalendar",
                    SystemId = "200300",
                    Ico = "fa-calendar"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "任务回复",
                    ControllerName = "ProjectTaskReply",
                    SystemId = "200400",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "项目",
                    ControllerName = "MyParticipateWork",
                    SystemId = "300",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "我负责的",
                    ControllerName = "MyCreateWork",
                    SystemId = "300100",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "我参与的",
                    ControllerName = "MyParticipateWork",
                    SystemId = "300200",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "项目回复	",
                    ControllerName = "ProjectInfoReply",
                    SystemId = "300250",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "我关注的",
                    ControllerName = "MyFollowWork",
                    SystemId = "300300",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "公开项目",
                    ControllerName = "AllWork",
                    SystemId = "300400",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "项目统计",
                    ControllerName = "ProjectInfoCount",
                    SystemId = "300500",
                    Ico = "fa-list-ul"
                },

                    new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "项目文件",
                    ControllerName = "ProjectFile",
                    SystemId = "300600",
                    Display = false,
                    Ico = "fa-file-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "市场",
                    ControllerName = "Customer",
                    SystemId = "350",
                    Ico = "fa-shopping-cart"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "客户信息",
                    ControllerName = "Customer",
                    SystemId = "350050",
                    Ico = "fa-user"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "联系人",
                    ControllerName = "Contact",
                    SystemId = "350100",
                    Ico = "fa-user"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "沟通记录",
                    ControllerName = "CustomerCommunication",
                    SystemId = "350150",
                    Ico = "fa-comments-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "业务统计",
                    ControllerName = "CustomerCount",
                    SystemId = "350200",
                    Ico = "fa-cogs"
                }, 
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "活动管理",
                    ControllerName = "Activity",
                    SystemId = "350300",
                    Ico = "fa-car"
                },  new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "活动管理报名",
                    ControllerName = "ActivityUser",
                    Display = false,
                    SystemId = "350300100",
                    Ico = "fa-car"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "财务",
                    ControllerName = "ProjectFinancial",
                    SystemId = "360",
                    Ico = "fa-cny"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "财务信息",
                    ControllerName = "ProjectFinancial",
                    SystemId = "360300",
                    Ico = "fa-cny"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "财务汇总",
                    ControllerName = "ProjectFinancialReport",
                    SystemId = "360400",
                    Ico = "fa-list-alt"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "文档",
                    ControllerName = "Knowledge",
                    SystemId = "400",
                    Ico = "fa-file-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "知识库",
                    ControllerName = "Knowledge",
                    SystemId = "400200",
                    Ico = "fa-file-text"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "知识库回复",
                    ControllerName = "KnowledgeReply",
                    SystemId = "400200100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "同事",
                    ControllerName = "Colleague",
                    SystemId = "520",
                    Ico = "fa-users"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "所有同事",
                    ControllerName = "Colleague",
                    SystemId = "520100",
                    Ico = "fa-users"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "流程",
                    ControllerName = "Flow",
                    SystemId = "550",
                    Ico = "fa-align-left"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "流程",
                    ControllerName = "Flow",
                    SystemId = "550100",
                    Ico = "fa-align-left"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "统计",
                    ControllerName = "ProjectInfoReport",
                    SystemId = "600",
                    Ico = "fa-bar-chart-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "项目报表",
                    ControllerName = "ProjectInfoReport",
                    SystemId = "600100",
                    Ico = "fa-list"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "工作总结",
                    ControllerName = "WorkReport",
                    SystemId = "600200",
                    Ico = "fa-list-ol"
                },
              
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "字典",
                    ControllerName = null,
                    SystemId = "650",
                    Ico = "fa-book"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "项目状态",
                    ControllerName = "ProjectInfoState",
                    SystemId = "650100",
                    Ico = "fa-info-circle"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "业务状态",
                    ControllerName = "BusinessState",
                    SystemId = "650200",
                    Ico = "fa-circle"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "业务机会",
                    ControllerName = "BusinessChance",
                    SystemId = "650150",
                    Ico = "fa-dot-circle-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "客户类型",
                    ControllerName = "CustomerType",
                    SystemId = "650300",
                    Ico = "fa-group"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "客户等级",
                    ControllerName = "CustomerLevel",
                    SystemId = "650400",
                    Ico = "fa-thumbs-up"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "管理",
                    ControllerName = "SysUser",
                    SystemId = "700",
                    Ico = "fa-cog"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "组织架构",
                    ControllerName = "SysDepartment",
                    SystemId = "700200",
                    Ico = "fa-sitemap"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "用户日志",
                    ControllerName = "SysUserLog",
                    SystemId = "700900",
                    Ico = "fa-calendar-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "角色管理",
                    ControllerName = "SysRole",
                    SystemId = "700300",
                    Ico = "fa-users"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "用户管理",
                    ControllerName = "SysUser",
                    SystemId = "700400",
                    Ico = "fa-user"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "企业管理",
                    ControllerName = "Index",
                    SystemId = "700",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "企业管理",
                    ControllerName = "SysEnterprise",
                    SystemId = "700200",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "角色管理",
                    ControllerName = "SysRole",
                    SystemId = "700300",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "用户管理",
                    ControllerName = "SysUser",
                    SystemId = "700400",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "系统设置",
                    ControllerName = "Index",
                    SystemId = "800",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "系统参数",
                    ControllerName = "WebConfigAppSetting",
                    SystemId = "800100",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "系统区域",
                    ControllerName = "SysArea",
                    SystemId = "800200",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "操作类型",
                    ControllerName = "SysAction",
                    SystemId = "800300",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "系统模块",
                    ControllerName = "SysController",
                    SystemId = "800400",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "帮助信息",
                    ControllerName = "SysHelp",
                    SystemId = "800900",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "系统信息",
                    ControllerName = "Index",
                    SystemId = "900",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "统计信息",
                    ControllerName = "SysStatistic",
                    SystemId = "900100",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "用户日志",
                    ControllerName = "SysUserLog",
                    SystemId = "900300",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "系统日志",
                    ControllerName = "SysLog",
                    SystemId = "900400",
                }
            };
            context.SysControllers.AddOrUpdate(
                a => new {a.SysAreaId, a.SystemId}, sysControllers);

            #endregion

            #region SysControllerSysAction

            SysControllerSysAction[] sysControllerSysActions = (from sysAction in sysActions
                from sysController in sysControllers
                select
                    new SysControllerSysAction
                    {
                        SysActionId = sysAction.Id,
                        SysControllerId = sysController.Id
                    }).ToArray();

            context.SysControllerSysActions.AddOrUpdate(a => new {a.SysActionId, a.SysControllerId},
                sysControllerSysActions);

            #endregion
        }
    }
}