using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IChartService : IDisposable
    {
        IEnumerable<LineSery> GetLineSeries();
        IEnumerable<SalesData> GetColumnData();
        IEnumerable<Trackball> GetTrackballData();

    }
}