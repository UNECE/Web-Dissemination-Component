using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class DomainsModel
    {
        public static string DEFAULT_COUNTRY_CODE = "000";
        private string curLang = "";

        public DomainsModel(string lang)
        {
            curLang = lang;
        }

        public System.Collections.Generic.IEnumerable<DomainModel> getDomains()
        {
            DataReader reader = App.getDataReader();
            reader.Open(curLang, DataReader.MODEL_DOMAINS);

            while (reader.Read())
            {
                string domainCode = reader.getString(DataReader.FIELD_DOMAINS_CODE);
                DomainModel thisDomain = new DomainModel(curLang, domainCode);

                yield return thisDomain;
            }

            reader.Close();

        }
    }
}

