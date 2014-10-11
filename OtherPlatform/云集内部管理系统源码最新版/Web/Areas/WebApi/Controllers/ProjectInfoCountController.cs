using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using IServices.IUserServices;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class ProjectInfoCountController : Controller
    {
        private readonly IProjectInfoStateService _iProjectInfoStateService;
        private readonly IProjectInfoService _IProjectInfoService;

        public ProjectInfoCountController(IProjectInfoStateService iProjectInfoStateService, IProjectInfoService iProjectInfoService)
        {
            _iProjectInfoStateService = iProjectInfoStateService;
            _IProjectInfoService = iProjectInfoService;
        }

        //
        // GET: /Platform/ProjectInfoCount/

        public ActionResult Index()
        {
            var model = _IProjectInfoService.GetAll(a => a.LastProjectInfoId == null);
            var result = new 
            {
                unfinished = model.Count(a => !a.Finish),
                finished = model.Count(a => a.Finish)
            };
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult Details(int width = 500, int height = 500)
        {
            var chart = new Chart { Height = height, Width = width };
            var chartArea = new ChartArea("Area1") { AxisX = { Interval = 1 }, Area3DStyle = { Enable3D = true } };
            chart.ChartAreas.Add(chartArea);

            var seriescountAll = new Series("项目统计");
            var countAll =
                _iProjectInfoStateService.GetAll().Select(a => new { Key = a.ProjectInfoStateName, Count = a.ProjectInfos.Count(b => !b.Deleted) });
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
