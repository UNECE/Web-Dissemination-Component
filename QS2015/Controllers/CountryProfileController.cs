using QS2015.Connectivity;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QS2015.Controllers
{
    public class CountryProfileController : Controller
    {
        //
        // GET: /CountryProfile/

        private CountryProfileModel countryProfile = null;

        public ActionResult Index(string countryCode)
        {
            // add record to log table
            string thisLanguage = Session["Culture"].ToString();

            if (App.isPhrase(countryCode))
            {
                App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_PROFILE, countryCode, "country_code=" + countryCode);
            }

            countryProfile = new CountryProfileModel(thisLanguage, countryCode);

            return View(countryProfile);
        }


        public JsonResult GetCountriesData(string firstCountryCode, string countryCodes)
        {
            List<object> dataset = GetDataSet(firstCountryCode, countryCodes);

            return Json(dataset, JsonRequestBehavior.AllowGet);
        }

        private List<object> GetDataSet(string firstCountryCode, string countryCodes)
        {
            // add record to log table
            string thisLanguage = Session["Culture"].ToString();

            if (App.isPhrase(countryCodes))
            {
                App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_PROFILE, countryCodes, "country_code=" + countryCodes);
            }
            

            List<object> dataset = CountryProfilesModel.GetDataset(thisLanguage, firstCountryCode, countryCodes);
            return dataset;
        }
    }
}
