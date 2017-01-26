using QS2015.Connectivity;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QS2015.Controllers
{
    public class CountryRankingController : Controller
    {
        //
        // GET: /CountryRanking/

        private CountryRankingModel countryRanking = null;

        public ActionResult Index(string indicatorCode)
        {
            string thisLanguage = Session["Culture"].ToString();

            if (App.isPhrase(indicatorCode))
            {
                App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_RANKING, indicatorCode, "indicator_code=" + indicatorCode);
            }

            countryRanking = new CountryRankingModel(thisLanguage, indicatorCode, CountryRankingModel.SORT_BY_VALUE);

            return View(countryRanking);
        }
    }
}
