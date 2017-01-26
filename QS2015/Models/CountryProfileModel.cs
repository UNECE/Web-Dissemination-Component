using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class CountryProfileModel
    {
        public CountryModel Country { get; set; }
        public List<DomainDataModel> Profile { get; set; }

        public CountryProfileModel(string lang, string countryCode)
        {
            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(countryCode))
            {
                return;
            }
            // ****************************************************************

            Profile = new List<DomainDataModel>();
            Country = CountryModel.getCountry(lang, countryCode);

            // add country key
            DataKey countryKey = new DataKey(DataReader.KEY_COUNTRYPROFILE_COUNTRYCODE, countryCode);

            // start scrolling through domains and indicators
            DomainsModel dm = new DomainsModel(lang);

            foreach(DomainModel thisDomainModel in dm.getDomains()) {
                string strDomainCode = thisDomainModel.Code;
                string strDomainName = thisDomainModel.Name;

                Boolean dataFound = false;
                DomainDataModel thisDomain = new DomainDataModel();
                thisDomain.Code = strDomainCode;
                thisDomain.Name = strDomainName;

                foreach (IndicatorModel thisIndicatorModel in thisDomainModel.Indicators)
                {
                    string strIndicatorCode = thisIndicatorModel.Code;
                    string strIndicatorName = thisIndicatorModel.Name;
                    string strMeasurementUnit = thisIndicatorModel.MeasurementUnit;

                    // add indicator key
                    DataKey indicatorKey = new DataKey(DataReader.KEY_COUNTRYPROFILE_INDICATORCODE, strIndicatorCode);

                    // get a list of keys
                    List<DataKey> keysList = new List<DataKey>();
                    keysList.Add(countryKey);
                    keysList.Add(indicatorKey);

                    // get value
                    DataReader reader = App.getDataReader();
                    reader.Open(lang, DataReader.MODEL_COUNTRYPROFILE, keysList);

                    if (reader.Read())
                    {
                        string periodCode = reader.getString(DataReader.FIELD_COUNTRYPROFILE_PERIODCODE);
                        string thisValue = reader.getString(DataReader.FIELD_COUNTRYPROFILE_VALUE);

                        // populate data classes
                        IndicatorDataModel thisIndicator = new IndicatorDataModel();

                        thisIndicator.Code = strIndicatorCode;
                        thisIndicator.Name = strIndicatorName;
                        thisIndicator.PeriodCode = periodCode;
                        thisIndicator.MeasurementUnit = strMeasurementUnit;
                        thisIndicator.Value = thisValue;

                        thisDomain.Indicators.Add(thisIndicator);
                        dataFound = true;
                    }

                    reader.Close();
                }

                if (dataFound)
                {
                    Profile.Add(thisDomain);
                }
            }
        }
    }
}

