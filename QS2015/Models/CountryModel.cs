using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class CountryModel
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public static CountryModel getCountry(string lang, String countryCode)
        {
            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(countryCode))
            {
                return null;
            }
            // ****************************************************************
            
            DataReader reader = App.getDataReader();
            DataKey thisKey = new DataKey(DataReader.KEY_COUNTRY_CODE, countryCode);
            reader.Open(lang, DataReader.MODEL_COUNTRY, thisKey);

            CountryModel thisCountry = null;
            if (reader.Read())
            {
                string countryName = reader.getString(DataReader.FIELD_COUNTRY_NAME);
                
                thisCountry = new CountryModel();

                thisCountry.Code = countryCode;
                thisCountry.Name = countryName;
            }

            reader.Close();

            return thisCountry;

        }
    }
}

