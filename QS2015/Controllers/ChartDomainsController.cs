﻿using QS2015.Connectivity;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QS2015.Controllers
{
    public class ChartDomainsController : Controller
    {
        //
        // GET: /ChartDomains/

        private DomainsModel domains = null;

        public ActionResult Index()
        {
            string thisLanguage = Session["Culture"].ToString();
            App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_CHART_COUNTRIES, "", "");

            domains = new DomainsModel(thisLanguage);
            return View(domains);
        }

    }
}
