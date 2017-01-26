using QS2015.Connectivity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace QS2015
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }


        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                object curCulture = this.Session["Culture"];
                string mylang = App.getDefaultLanguage().Code;

                if (curCulture != null)
                {
                    if (!curCulture.ToString().Equals("", StringComparison.CurrentCultureIgnoreCase))
                    {
                        mylang = curCulture.ToString();
                    }
                }

                string url = Request.RawUrl.ToString();
                string root = HttpContext.Current.Request.ApplicationPath;
                if (url != null)
                {
                    if (url.StartsWith(root))
                    {
                        if (url.Length > root.Length + 2)
                        {
                            mylang = url.Substring(root.Length +1, 2);
                        }
                    }
                }

                CultureInfo ci = new CultureInfo(mylang);
                this.Session["Culture"] = ci;

                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

            }
        }

    }
}