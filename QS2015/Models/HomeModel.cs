using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class HomeModel
    {
        private string curLang = "";

        private string strWebLink = "";
        private string strText = "";
        private string strExpired = "";

        public HomeModel(string lang)
        {
            curLang = lang;

            DataReader reader = App.getDataReader();
            reader.Open(lang, DataReader.MODEL_HOME);

            if (reader.Read())
            {
                this.strWebLink = reader.getString(DataReader.FIELD_HOME_WEBLINK);
                this.strText = reader.getString(DataReader.FIELD_HOME_MESSAGE_TEXT);
                this.strExpired = reader.getString(DataReader.FIELD_HOME_EXPIRED);
            }

            reader.Close();

        }

        public string getWebLink()
        {
            return strWebLink;
        }

        public string getMessage()
        {
            return strText;
        }

        public Boolean showMessage()
        {
            Boolean result = false;
            DateTime localDate = DateTime.Now;

            string strCurrentDateTime = localDate.ToString("yyyy-MM-dd HH:mm:ss");
            int intCompare = this.strExpired.CompareTo(strCurrentDateTime);

            if (intCompare > 0)
            {
                result = true;
            }

            return result;
        }
    }
}

