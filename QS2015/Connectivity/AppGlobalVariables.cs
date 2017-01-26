using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Connectivity
{
    public static class AppGlobalVariables
    {
        private static string strAppVersion = App.getAppSettingsKeyValue("WebApplicationVersion");

        public static string AppVersion
        {
            get { return AppGlobalVariables.strAppVersion; }
            set { AppGlobalVariables.strAppVersion = value; }
        }
    }
}

