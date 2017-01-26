using QS2015.Connectivity;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QS2015.Controllers
{
    public class PDFCountryProfilesController : Controller
    {
        //
        // GET: /PDFCountryProfiles/
        private PDFCountryProfilesModel countryProfile = null;

        public ActionResult Index()
        {
            string thisLanguage = Session["Culture"].ToString();
            App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_PDF_PROFILE, "", "");

            countryProfile = new PDFCountryProfilesModel(thisLanguage);

            return View(countryProfile);
        }

    }
}
