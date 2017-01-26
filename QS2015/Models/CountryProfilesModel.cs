using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class CountryProfilesModel
    {
        public static List<object> GetDataset(string curLang, string firstCountryCode, string countryCodes)
        {
            /*
             * Example of the data structure
             * 
             * Indicator
             *   1
             *      Country
             *          250
             *          France
             *          1.1
             *          red
             *      
             *      Country
             *          840
             *          United States
             *          4.2
             *          blue          
             */

            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(firstCountryCode))
            {
                return null;
            }
            // ****************************************************************

            // start creating json file
            List<object> dataset = new List<object>();
            List<object> countrydata = null;

            //string countriesSet = "";
            string[] countries = countryCodes.Split(',');

            foreach (string thiscode in countries)
            {

                // protection code against SQL injection
                // ****************************************************************
                if (!App.isPhrase(thiscode))
                {
                    return null;
                }
                // ****************************************************************
            }


            var parms = countries.Select((s, i) => "@p" + i.ToString()).ToArray();

            DataReader reader = App.getDataReader();
            
            DataKey thisKey = new DataKey(DataReader.KEY_COUNTRYPROFILES_FIRSTCOUNTRYCODE, firstCountryCode);
            List<DataKey> datakeys = new List<DataKey>();
            datakeys.Add(thisKey);

            List<AddOnParameter> addOnKeys = new List<AddOnParameter>();
            for (var i = 0; i < countries.Length; i++)
            {
                AddOnParameter thisParam = new AddOnParameter(parms[i], countries[i]);
                addOnKeys.Add(thisParam);
            }

            reader.Open(curLang, DataReader.MODEL_COUNTRYPROFILES, datakeys, addOnKeys);

            int index = 0;
            string memIndCode = "";

            while (reader.Read())
            {
                // get codes
                string indicatorCode = reader.getString(DataReader.FIELD_COUNTRYPROFILES_INDICATORCODE);
                string countryCode = reader.getString(DataReader.FIELD_COUNTRYPROFILES_COUNTRYCODE);
                string countryName = reader.getString(DataReader.FIELD_COUNTRYPROFILES_NAME);
                string thisValue = reader.getString(DataReader.FIELD_COUNTRYPROFILES_VALUE);

                // init indicator object
                if (!indicatorCode.Equals(memIndCode))
                {
                    index = 0;
                    countrydata = new List<object>();

                    dataset.Add(
                        new
                        {
                            IndicatorCode = indicatorCode,
                            CountryData = countrydata
                        }
                    );

                    memIndCode = indicatorCode;
                }

                // assign new color
                string thisColor = ColorModel.getNextColor(index);
                index++;

                // add country data for this indicator
                countrydata.Add(
                    new
                    {
                        CountryCode = countryCode,
                        CountryName = countryName,
                        Value = thisValue,
                        Color = thisColor
                    }
                );
            }

            reader.Close();

            return dataset;
        }
    }
}