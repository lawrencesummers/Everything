using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.WebApi.Controllers
{
    public class CustomerCountController : Controller
    {
        private readonly IBusinessChanceService _iBusinessChanceService;
        private readonly IBusinessStateService _iBusinessStateService;
        private readonly ICustomerLevelService _iCustomerLevelService;
        private readonly ICustomerTypeService _iCustomerTypeService;

        public CustomerCountController(IBusinessStateService iBusinessStateService,
            IBusinessChanceService iBusinessChanceService, ICustomerTypeService iCustomerTypeService,
            ICustomerLevelService iCustomerLevelService)
        {
            _iBusinessStateService = iBusinessStateService;
            _iBusinessChanceService = iBusinessChanceService;
            _iCustomerTypeService = iCustomerTypeService;
            _iCustomerLevelService = iCustomerLevelService;
        }

        //
        // GET: /Platform/BusinessReport/

        public ActionResult Index()
        {
            IQueryable<BusinessState> model = _iBusinessStateService.GetAll();
            return View(model);
        }

        public ActionResult Details(string reportType, int width = 550, int height = 350)
        {
            var chart = new Chart {Height = height, Width = width};
            chart.Legends.Add("图例");
            var chartArea = new ChartArea("Area1") {AxisX = {Interval = 1}, Area3DStyle = {Enable3D = true}};

            chart.ChartAreas.Add(chartArea);

            var seriescountAll = new Series();

            switch (reportType)
            {
                case "BusinessState":
                    chart.Titles.Add(new Title("业务状态", Docking.Top, new Font("微软雅黑", 20), Color.Black));

                    seriescountAll.ChartArea = "Area1";

                    seriescountAll.IsVisibleInLegend = true;
                    seriescountAll.IsValueShownAsLabel = true;
                    seriescountAll.Label = "#VALX  #VALY";
                    seriescountAll.Points.DataBind(
                        _iBusinessStateService.GetAll(a => a.Statistics && a.Customers.Any(b => !b.Deleted))
                            .Select(a => new {Key = a.BusinessStateName, Count = a.Customers.Count()}), "Key", "Count",
                        "");
                    seriescountAll.ChartType = SeriesChartType.Funnel;

                    chart.Series.Add(seriescountAll);

                    break;

                case "BusinessChance":

                    chart.Titles.Add(new Title("业务机会", Docking.Top, new Font("微软雅黑", 20), Color.Black));

                    seriescountAll.ChartArea = "Area1";

                    seriescountAll.IsVisibleInLegend = true;
                    seriescountAll.IsValueShownAsLabel = true;
                    seriescountAll.Label = "#VALX  #VALY";
                    seriescountAll.Points.DataBind(
                        _iBusinessChanceService.GetAll(a => a.CustomerBusinessChances.Any(b => !b.Deleted))
                            .Select(a => new {Key = a.BusinessChanceName, Count = a.CustomerBusinessChances.Count()}),
                        "Key", "Count", "");

                    seriescountAll.ChartType = SeriesChartType.Pie;

                    chart.Series.Add(seriescountAll);

                    break;

                case "CustomerType":

                    chart.Titles.Add(new Title("客户类型", Docking.Top, new Font("微软雅黑", 20), Color.Black));

                    seriescountAll.ChartArea = "Area1";
                    seriescountAll.Color = Color.CornflowerBlue;
                    seriescountAll.IsVisibleInLegend = true;
                    seriescountAll.IsValueShownAsLabel = true;
                    seriescountAll.Label = "#VALX  #VALY";
                    seriescountAll.Points.DataBind(_iCustomerTypeService.GetAll(a => a.Customers.Any(b => !b.Deleted))
                        .Select(a => new {Key = a.CustomerTypeName, Count = a.Customers.Count()}), "Key", "Count", "");

                    seriescountAll.ChartType = SeriesChartType.Doughnut;

                    chart.Series.Add(seriescountAll);

                    break;

                case "CustomerLevel":

                    chart.Titles.Add(new Title("客户等级", Docking.Top, new Font("微软雅黑", 20), Color.Black));

                    seriescountAll.ChartArea = "Area1";

                    seriescountAll.IsVisibleInLegend = true;
                    seriescountAll.IsValueShownAsLabel = true;
                    seriescountAll.Label = "#VALX  #VALY";

                    seriescountAll.Points.DataBind(_iCustomerLevelService.GetAll(a => a.Customers.Any(b => !b.Deleted))
                        .Select(a => new {Key = a.CustomerLevelName, Count = a.Customers.Count()}), "Key", "Count", "");

                    seriescountAll.ChartType = SeriesChartType.Doughnut;

                    chart.Series.Add(seriescountAll);

                    break;
            }

            var imageStream = new MemoryStream();
            chart.SaveImage(imageStream, ChartImageFormat.Png);
            imageStream.Position = 0;
            return new FileStreamResult(imageStream, "image/png");
        }
    }
}