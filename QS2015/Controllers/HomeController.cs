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
    public class HomeController : Controller
    {
        private HomeModel myhome = null;

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            if (App.isWord(lang))
            {
                Session["Culture"] = new CultureInfo(lang);
                string strNewUrl = App.getUrlForNewCulture(lang, returnUrl);

                return Redirect(strNewUrl);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Index()
        {
            string appPath = App.getApplicationPath();

            string thisUrl = Request.RawUrl;

            if (thisUrl.Equals(appPath, StringComparison.CurrentCultureIgnoreCase) || thisUrl.Equals(appPath + "/", StringComparison.CurrentCultureIgnoreCase))
            {
                string result = "";

                string strDefaultLanguageCode = App.getDefaultLanguage().Code;

                if (appPath.EndsWith("/"))
                {
                    result = appPath + strDefaultLanguageCode;
                }
                else
                {
                    result = appPath + "/" + strDefaultLanguageCode;
                }

                return Redirect(result);
            }

            // add record to log table
            string thisLanguage = Session["Culture"].ToString();
            App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_HOME, "", "");

            ViewBag.Message = "home page";


            myhome = new HomeModel(thisLanguage);
            return View(myhome);
        }

        public ActionResult About()
        {
            ViewBag.Message = "home page";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "home page";

            return View();
        }
    }
}
