using QS2015.Connectivity;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QS2015.Controllers
{
    public class TimeSeriesController : Controller
    {
        //
        // GET: /TimeSeries/

        private TimeSeriesModel timeSeries = null;

        public ActionResult Index(string indicatorCode, string countryCode)
        {
            string thisLanguage = Session["Culture"].ToString();

            if (App.isPhrase(indicatorCode))
            {
                if (App.isPhrase(countryCode))
                {
                    App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_TIME_SERIES, indicatorCode + "," + countryCode, "indicator_code,country_code=" + indicatorCode + "," + countryCode);
                }
            }

            timeSeries = new TimeSeriesModel(thisLanguage, countryCode, indicatorCode);

            return View(timeSeries);
        }

    }
}
