using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [RoutePrefix("api/charts")]
    public class ChartsController : ApiController
    {
        private IChartService _chartService;

        public ChartsController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [Route("lineseries")]
        [HttpGet]
        public IEnumerable<LineSery> GetLineSeries()
        {
            return _chartService.GetLineSeries();
        }

        [Route("column")]
        [HttpGet]
        public IEnumerable<SalesData> GetColumnData()
        {
            return _chartService.GetColumnData();
        }

        [Route("trackball")]
        [HttpGet]
        public IEnumerable<Trackball> GetTrackballData()
        {
            return _chartService.GetTrackballData();
        }
    }
}
