using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class CountriesModel
    {
        private string curLang = "";

        public CountriesModel(string lang)
        {
            curLang = lang;
        }

        public System.Collections.Generic.IEnumerable<CountryModel> getCountries()
        {
            DataReader reader = App.getDataReader();
            reader.Open(curLang, DataReader.MODEL_COUNTRIES);

            CountryModel thisCountry = null;
            while (reader.Read())
            {
                string countryCode = reader.getString(DataReader.FIELD_COUNTRIES_CODE);
                string countryName = reader.getString(DataReader.FIELD_COUNTRIES_NAME);

                thisCountry = new CountryModel();

                thisCountry.Code = countryCode;
                thisCountry.Name = countryName;

                yield return thisCountry;
            }

            reader.Close();            

        }
    }
}