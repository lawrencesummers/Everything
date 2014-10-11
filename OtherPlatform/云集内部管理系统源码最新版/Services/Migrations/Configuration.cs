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
                    AreaDisplayName = "�û���¼",
                    SystemId = "000"
                },
                new SysArea
                {
                    AreaName = "Platform",
                    AreaDisplayName = "����ƽ̨",
                    SystemId = "001"
                },
                new SysArea
                {
                    AreaName = "Admin",
                    AreaDisplayName = "����ƽ̨",
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
                    ActionDisplayName = "�б�",
                    ActionName = "Index",
                    SystemId = "001"
                },
                new SysAction
                {
                    ActionDisplayName = "��ϸ",
                    ActionName = "Details",
                    SystemId = "003"
                },
                new SysAction
                {
                    ActionDisplayName = "�½�",
                    ActionName = "Create",
                    SystemId = "004"
                },
                new SysAction
                {
                    ActionDisplayName = "�༭",
                    ActionName = "Edit",
                    SystemId = "005"
                },
                new SysAction
                {
                    ActionDisplayName = "ɾ��",
                    ActionName = "Delete",
                    SystemId = "006"
                },
                new SysAction
                {
                    ActionDisplayName = "����",
                    ActionName = "Import",
                    SystemId = "007"
                },
                new SysAction
                {
                    ActionDisplayName = "����",
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
                    ControllerDisplayName = "����ƽ̨",
                    ControllerName = "Index",
                    SystemId = "100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "֪ͨ����",
                    ControllerName = "Message",
                    SystemId = "101",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "���߹�ͨ",
                    ControllerName = "Chat",
                    SystemId = "102",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "����ƽ̨",
                    ControllerName = "Index",
                    SystemId = "100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "����",
                    ControllerName = "Desktop",
                    SystemId = "100100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "����",
                    ControllerName = "Desktop",
                    SystemId = "100100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "ʹ�ð���",
                    ControllerName = "Help",
                    SystemId = "100200",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "ʹ�ð���",
                    ControllerName = "Help",
                    SystemId = "100200",
                    Display = false,
                },


                //ǰ̨�µĹ���

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ƻ�",
                    ControllerName = "MyPlan",
                    SystemId = "200",
                    Ico = "fa-clock-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ҵļƻ�",
                    ControllerName = "MyPlan",
                    SystemId = "200100",
                    Ico = "fa-clock-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ҵ�����",
                    ControllerName = "MyProjectTask",
                    SystemId = "200200",
                    Ico = "fa-tasks"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ҵ�����",
                    ControllerName = "MyCalendar",
                    SystemId = "200300",
                    Ico = "fa-calendar"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "����ظ�",
                    ControllerName = "ProjectTaskReply",
                    SystemId = "200400",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��Ŀ",
                    ControllerName = "MyParticipateWork",
                    SystemId = "300",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�Ҹ����",
                    ControllerName = "MyCreateWork",
                    SystemId = "300100",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�Ҳ����",
                    ControllerName = "MyParticipateWork",
                    SystemId = "300200",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��Ŀ�ظ�	",
                    ControllerName = "ProjectInfoReply",
                    SystemId = "300250",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ҹ�ע��",
                    ControllerName = "MyFollowWork",
                    SystemId = "300300",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "������Ŀ",
                    ControllerName = "AllWork",
                    SystemId = "300400",
                    Ico = "fa-list-ul"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��Ŀͳ��",
                    ControllerName = "ProjectInfoCount",
                    SystemId = "300500",
                    Ico = "fa-list-ul"
                },

                    new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��Ŀ�ļ�",
                    ControllerName = "ProjectFile",
                    SystemId = "300600",
                    Display = false,
                    Ico = "fa-file-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�г�",
                    ControllerName = "Customer",
                    SystemId = "350",
                    Ico = "fa-shopping-cart"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ͻ���Ϣ",
                    ControllerName = "Customer",
                    SystemId = "350050",
                    Ico = "fa-user"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��ϵ��",
                    ControllerName = "Contact",
                    SystemId = "350100",
                    Ico = "fa-user"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��ͨ��¼",
                    ControllerName = "CustomerCommunication",
                    SystemId = "350150",
                    Ico = "fa-comments-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "ҵ��ͳ��",
                    ControllerName = "CustomerCount",
                    SystemId = "350200",
                    Ico = "fa-cogs"
                }, 
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�����",
                    ControllerName = "Activity",
                    SystemId = "350300",
                    Ico = "fa-car"
                },  new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�������",
                    ControllerName = "ActivityUser",
                    Display = false,
                    SystemId = "350300100",
                    Ico = "fa-car"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "����",
                    ControllerName = "ProjectFinancial",
                    SystemId = "360",
                    Ico = "fa-cny"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "������Ϣ",
                    ControllerName = "ProjectFinancial",
                    SystemId = "360300",
                    Ico = "fa-cny"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�������",
                    ControllerName = "ProjectFinancialReport",
                    SystemId = "360400",
                    Ico = "fa-list-alt"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ĵ�",
                    ControllerName = "Knowledge",
                    SystemId = "400",
                    Ico = "fa-file-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "֪ʶ��",
                    ControllerName = "Knowledge",
                    SystemId = "400200",
                    Ico = "fa-file-text"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "֪ʶ��ظ�",
                    ControllerName = "KnowledgeReply",
                    SystemId = "400200100",
                    Display = false,
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "ͬ��",
                    ControllerName = "Colleague",
                    SystemId = "520",
                    Ico = "fa-users"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "����ͬ��",
                    ControllerName = "Colleague",
                    SystemId = "520100",
                    Ico = "fa-users"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "����",
                    ControllerName = "Flow",
                    SystemId = "550",
                    Ico = "fa-align-left"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "����",
                    ControllerName = "Flow",
                    SystemId = "550100",
                    Ico = "fa-align-left"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "ͳ��",
                    ControllerName = "ProjectInfoReport",
                    SystemId = "600",
                    Ico = "fa-bar-chart-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��Ŀ����",
                    ControllerName = "ProjectInfoReport",
                    SystemId = "600100",
                    Ico = "fa-list"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�����ܽ�",
                    ControllerName = "WorkReport",
                    SystemId = "600200",
                    Ico = "fa-list-ol"
                },
              
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ֵ�",
                    ControllerName = null,
                    SystemId = "650",
                    Ico = "fa-book"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��Ŀ״̬",
                    ControllerName = "ProjectInfoState",
                    SystemId = "650100",
                    Ico = "fa-info-circle"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "ҵ��״̬",
                    ControllerName = "BusinessState",
                    SystemId = "650200",
                    Ico = "fa-circle"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "ҵ�����",
                    ControllerName = "BusinessChance",
                    SystemId = "650150",
                    Ico = "fa-dot-circle-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ͻ�����",
                    ControllerName = "CustomerType",
                    SystemId = "650300",
                    Ico = "fa-group"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�ͻ��ȼ�",
                    ControllerName = "CustomerLevel",
                    SystemId = "650400",
                    Ico = "fa-thumbs-up"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "����",
                    ControllerName = "SysUser",
                    SystemId = "700",
                    Ico = "fa-cog"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��֯�ܹ�",
                    ControllerName = "SysDepartment",
                    SystemId = "700200",
                    Ico = "fa-sitemap"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�û���־",
                    ControllerName = "SysUserLog",
                    SystemId = "700900",
                    Ico = "fa-calendar-o"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "��ɫ����",
                    ControllerName = "SysRole",
                    SystemId = "700300",
                    Ico = "fa-users"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    ControllerDisplayName = "�û�����",
                    ControllerName = "SysUser",
                    SystemId = "700400",
                    Ico = "fa-user"
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "��ҵ����",
                    ControllerName = "Index",
                    SystemId = "700",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "��ҵ����",
                    ControllerName = "SysEnterprise",
                    SystemId = "700200",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "��ɫ����",
                    ControllerName = "SysRole",
                    SystemId = "700300",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "�û�����",
                    ControllerName = "SysUser",
                    SystemId = "700400",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "ϵͳ����",
                    ControllerName = "Index",
                    SystemId = "800",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "ϵͳ����",
                    ControllerName = "WebConfigAppSetting",
                    SystemId = "800100",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "ϵͳ����",
                    ControllerName = "SysArea",
                    SystemId = "800200",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "��������",
                    ControllerName = "SysAction",
                    SystemId = "800300",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "ϵͳģ��",
                    ControllerName = "SysController",
                    SystemId = "800400",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "������Ϣ",
                    ControllerName = "SysHelp",
                    SystemId = "800900",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "ϵͳ��Ϣ",
                    ControllerName = "Index",
                    SystemId = "900",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "ͳ����Ϣ",
                    ControllerName = "SysStatistic",
                    SystemId = "900100",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "�û���־",
                    ControllerName = "SysUserLog",
                    SystemId = "900300",
                },
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Admin").Id,
                    ControllerDisplayName = "ϵͳ��־",
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