using QS2015.Factory;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

namespace QS2015.Connectivity
{
    public class App
    {
        public const string QUICK_STATS_PROFILE                     = "profile";
        public const string QUICK_STATS_CHART                       = "chart";
        public const string QUICK_STATS_TIME_SERIES                 = "timeseries";
        public const string QUICK_STATS_RANKING                     = "ranking";
        public const string QUICK_STATS_PDF_PROFILE                 = "pdf";
        public const string QUICK_STATS_MAP                         = "map";
        public const string QUICK_STATS_HOME                        = "home";
        public const string QUICK_STATS_ABOUT                       = "about";
        public const string QUICK_STATS_CHART_DOMAINS               = "chartdomains";
        public const string QUICK_STATS_CHART_COUNTRIES             = "countries";
        public const string QUICK_STATS_CHART_MAP_DOMAIN            = "mapdomain";
        public const string QUICK_STATS_CHART_MAP_RANKING_DOMAINS   = "rankingdomains";

        // ------------------------------------------------------------------------------------------
        public const string DATA_READER_SQL_TRANSACT                = "sql.transact";
        public const string DATA_READER_WEBSERVICE_FLATFILES_JSON   = "flatfiles.json";
        // ------------------------------------------------------------------------------------------

        public const string SDMX_XML                                = "xml";
        public const string SDMX_JSON                               = "json";

        public static DataReader getDataReader()
        {
            String readerType = App.getAppSettingsKeyValue("DataReaderType");

            if (readerType.ToLower().Equals(DATA_READER_SQL_TRANSACT))
            {
                return new SQLReader();
            }
            else if (readerType.ToLower().Equals(DATA_READER_WEBSERVICE_FLATFILES_JSON))
            {
                return new FlatFileReader();
            }
            else
            {
                return new SQLReader();
            }
        }


        public static string getMapTerritoriesJavascriptFilePath(string lang)
        {
            // get data reader type
            String readerType = App.getAppSettingsKeyValue("DataReaderType");

            if (readerType.ToLower().Equals(DATA_READER_SQL_TRANSACT))
            {
                string strJavasciptMapFile = ((SQLReader)App.getDataReader()).getTerritoriesJavascriptFilePath(lang);
                return strJavasciptMapFile;
            }
            else if (readerType.ToLower().Equals(DATA_READER_WEBSERVICE_FLATFILES_JSON))
            {
                string strJavasciptMapFile = ((FlatFileReader)App.getDataReader()).getTerritoriesJavascriptFilePath(lang);
                return strJavasciptMapFile;
            }
            else
            {
                return "undefined";
            }
        }

        // get key value from Web.Config file
        public static string getAppSettingsKeyValue(string keyname)
        {

            string result = WebConfigurationManager.AppSettings[keyname];

            if (result == null)
            {
                result = "";
            }

            return result.Trim();
        }        

        // Make sure that the input argument contains alphabet characters only as a precaution measure against SQL injection
        public static Boolean isWord(string input)
        {
            Boolean result = false;

            if (Regex.IsMatch(input, @"^[a-zA-Z]+$"))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        // Make sure that the input argument contains alphabet characters and numbers only as a precaution measure against SQL injection
        public static Boolean isPhrase(string input)
        {
            Boolean result = false;

            if (Regex.IsMatch(input, @"^[a-zA-Z0-9_,]+$"))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }


        public static void addQuickStatsLogRecord(string lang, string type, string id, string description)
        {
            // protect from the SQL injection attack
            if (!App.isWord(lang))
            {
                return;
            }

            // get web stats log type, web address and IP address
            String strWebStatsLog = App.getAppSettingsKeyValue("WebStatsLog").Trim().ToLower();
            string website = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
            string ipaddress = App.getIPAddress();

            if (strWebStatsLog.Equals("disabled"))
            {
                return;
            }
            else if (strWebStatsLog.Equals("sql.transact"))
            {
                using (SqlConnection myConnection = DataLink.getConnection())
                {
                    string sql =
                        "INSERT INTO PXWebQueries.dbo.QuickStatsLog " +
                        "( " +
                        "[Date], " +
                        "[Type], " +
                        "[Id], " +
                        "[Description], " +
                        "[Website], " +
                        "[Lang], " +
                        "[IPAddress] " +
                        ") " +
                        "SELECT " +
                        "    GETDATE() [Date], " +
                        "    @type [Type], " +
                        "    @id [Id], " +
                        "    @description [Description], " +
                        "    @website [Website], " +
                        "    @lang [Lang], " +
                        "    @ipaddress [IPAddress] ";

                    SqlCommand myCommand = new SqlCommand(sql, myConnection);
                    myCommand.Parameters.AddWithValue("type", type);
                    myCommand.Parameters.AddWithValue("id", id);
                    myCommand.Parameters.AddWithValue("description", description);
                    myCommand.Parameters.AddWithValue("website", website);
                    myCommand.Parameters.AddWithValue("lang", lang);
                    myCommand.Parameters.AddWithValue("ipaddress", ipaddress);

                    myCommand.ExecuteNonQuery();

                    myCommand.Dispose();
                }
            }
            else if (strWebStatsLog.Equals("flatfile.csv"))
            {
                String strLogFilePath = App.getAppSettingsKeyValue("WebStatsLogFilePath");

                // add record
                using (StreamWriter sw = File.AppendText(strLogFilePath))
                {
                    string strToday = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                    sw.Write("\"" + "date=" + strToday + "\"" + ",");
                    sw.Write("\"" + "type=" + type + "\"" + ",");
                    sw.Write("\"" + "id=" + id + "\"" + ",");
                    sw.Write("\"" + "description=" + description + "\"" + ",");
                    sw.Write("\"" + "website=" + website + "\"" + ",");
                    sw.Write("\"" + "lang=" + lang + "\"" + ",");
                    sw.WriteLine("\"" + "ipaddress=" + ipaddress + "\"" + ",");
                }	
            }
        }

        public static string getIPAddress()
        {
            string visitorsIPAddr = string.Empty;

            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                visitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                visitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            
            return visitorsIPAddr;
        }


        public static string getUrlForNewCulture(string lang, string url)
        {
            string appPath = App.getApplicationPath();

            string result = url;

            if (url.Equals(appPath, StringComparison.CurrentCultureIgnoreCase))
            {
                result = url + "/" + lang;
            }
            else if (url.Equals(appPath + "/", StringComparison.CurrentCultureIgnoreCase))
            {
                result = url + lang;
            }
            else
            {
                string path = url.Substring(appPath.Length + 1);

                int pos = path.IndexOf("/");

                if (pos >= 0)
                {
                    result = appPath + "/" + lang + "/" + path.Substring(pos + 1);
                }
                else
                {
                    result = appPath + "/" + lang;
                }
            }

            return result;
        }


        public static string getApplicationPath()
        {
            return HttpContext.Current.Request.ApplicationPath;
        }


        public static LanguageModel getDefaultLanguage()
        {
            LanguageModel defaultLanguage = null;

            foreach (LanguageModel thisLanguage in LanguagesModel.getLanguages())
            {
                if (thisLanguage.DefaultLanguage)
                {
                    defaultLanguage = thisLanguage;
                    break;
                }
            }

            return defaultLanguage;
        }

    }
}


