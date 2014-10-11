using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class ProjectInfoCountController : Controller
    {
        private readonly IProjectInfoService _IProjectInfoService;
        private readonly IProjectInfoStateService _iProjectInfoStateService;

        public ProjectInfoCountController(IProjectInfoStateService iProjectInfoStateService,
            IProjectInfoService iProjectInfoService)
        {
            _iProjectInfoStateService = iProjectInfoStateService;
            _IProjectInfoService = iProjectInfoService;
        }

        //
        // GET: /Platform/ProjectInfoCount/

        public ActionResult Index()
        {
            IQueryable<ProjectInfo> model = _IProjectInfoService.GetAll(a => a.LastProjectInfoId == null);
            return View(model);
        }

        public ActionResult Details(int width = 500, int height = 500)
        {
            var chart = new Chart { Height = height, Width = width };
            var chartArea = new ChartArea("Area1")
            {
                AxisX = { Interval = 1 },
                Area3DStyle = { Enable3D = true },
                BackColor = Color.Transparent
            };
            chart.ChartAreas.Add(chartArea);

            chart.BackColor = Color.Transparent;


            var seriescountAll = new Series("项目统计");
            var countAll =
                _iProjectInfoStateService.GetAll()
                    .Select(a => new { Key = a.ProjectInfoStateName, Count = a.ProjectInfos.Count(b => !b.Deleted) });
            seriescountAll.ChartArea = "Area1";
            seriescountAll.IsVisibleInLegend = true;
            seriescountAll.IsValueShownAsLabel = true;
            seriescountAll.Label = "#VALX  #VALY";
            seriescountAll.Points.DataBind(countAll, "Key", "Count", "");
            seriescountAll.ChartType = SeriesChartType.Funnel;
            chart.Series.Add(seriescountAll);

            var imageStream = new MemoryStream();
            chart.SaveImage(imageStream, ChartImageFormat.Png);
            imageStream.Position = 0;
            return new FileStreamResult(imageStream, "image/png");
        }
    }
}