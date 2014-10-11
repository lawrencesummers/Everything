using Autofac;
using Autofac.Integration.Mvc;
using IServices.Infrastructure;
using IServices.ISysServices;
using Services;
using Services.Infrastructure;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using System.Timers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Helper;
using Web.SignalR;


namespace Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private Timer _objTimer;

        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDb, Services.Migrations.Configuration>());
            // 下载于www.51aspx.com
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.Load("Services"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

            builder.RegisterType<UserInfo>().As<IUserInfo>();
            
            builder.RegisterType<UserRole>().As<IUserRole>();

            builder.RegisterType<Messenger>().As<IMessenger>();//客户端消息推送

            builder.RegisterType<OnTimedEvent>().As<IOnTimedEvent>();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterHubs(Assembly.GetExecutingAssembly());

            //builder.RegisterType<ChatHub>().ExternallyOwned();

            var container = builder.Build();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            //GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapSupport.BootstrapBundleConfig.RegisterBundles(BundleTable.Bundles);




            //计划任务 按照间隔时间执行
            var onTimedEvent = DependencyResolver.Current.GetService<IOnTimedEvent>();
            _objTimer = new Timer(Convert.ToDouble(ConfigurationManager.AppSettings["Timer"]) * 1000 * 60);
            _objTimer.Elapsed += onTimedEvent.Run;
            _objTimer.Start();


            // 下载于www.51aspx.com
            //SqlDependency.Start(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        protected void Application_End()
        {
            //SqlDependency.Stop(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}