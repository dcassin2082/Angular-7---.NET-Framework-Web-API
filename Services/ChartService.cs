using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Services
{
    public class ChartService : ServicesBase, IChartService
    {
        public IEnumerable<SalesData> GetColumnData()
        {
            IEnumerable<SalesData> data = dbContext.SalesDatas;
            foreach (var d in data)
            {
                d.DateString = d.Day.Value.ToShortDateString();
            }
            return data.OrderBy(d => d.Day);
        }

        public IEnumerable<LineSery> GetLineSeries()
        {
            return dbContext.LineSeries;
        }

        public IEnumerable<Trackball> GetTrackballData()
        {
            return dbContext.Trackballs.OrderBy(x => x.x);
        }
    }
}