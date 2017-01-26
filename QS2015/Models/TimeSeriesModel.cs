using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class TimeSeriesModel
    {
        public CountryModel Country { get; set; }
        public IndicatorModel Indicator { get; set; }
        public List<TimeSeriesValueModel> TimeSeries { get; set; }

        public List<string> PeriodCodes { get; set; }

        public TimeSeriesModel(string lang, string countryCode, string indicatorCode)
        {
            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(countryCode))
            {
                return;
            }
            // ****************************************************************

            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(indicatorCode))
            {
                return;
            }
            // ****************************************************************

            // get time series
            this.Country = CountryModel.getCountry(lang, countryCode);
            this.Indicator = IndicatorModel.getIndicator(lang, indicatorCode);

            this.TimeSeries = new List<TimeSeriesValueModel>();
            this.PeriodCodes = new List<string>();

            DataReader reader = App.getDataReader();
            
            List<DataKey> keysList = new List<DataKey>();
            DataKey indicatorKey = new DataKey(DataReader.KEY_TIMESERIES_INDICATOR, indicatorCode);
            DataKey countryKey = new DataKey(DataReader.KEY_TIMESERIES_COUNTRY, countryCode);

            keysList.Add(indicatorKey);
            keysList.Add(countryKey);

            reader.Open(lang, DataReader.MODEL_TIMESERIES, keysList);

            while (reader.Read())
            {
                string thisLine = reader.getString(DataReader.FIELD_TIMESERIES_LINE);

                string[] myValues = thisLine.Split(new char[] { ',' });

                string periodCode = myValues[2];
                string thisValue = myValues[3];

                PeriodCodes.Add(periodCode);

                TimeSeriesValueModel thisTimeSeriesValue = new TimeSeriesValueModel();
                thisTimeSeriesValue.PeriodCode = periodCode;
                thisTimeSeriesValue.Value = thisValue;

                this.TimeSeries.Add(thisTimeSeriesValue);
            }

            reader.Close();

        }
    }
}

