using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class CountryRankingModel
    {
        public static int SORT_BY_VALUE                   = 0;
        public static int SORT_BY_COUNTRY_NAME            = 1;

        public IndicatorModel  Indicator { get; set; }
        public string PeriodCode{ get; set; }
        public List<CountryDataModel> Ranking { get; set; }

        public CountryRankingModel(string lang, string indicatorCode, int sortingMode)
        {

            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(indicatorCode))
            {
                return;
            }
            // ****************************************************************


            // start creating ranking
            Ranking = new List<CountryDataModel>();

            IndicatorModel thisIndicator = IndicatorModel.getIndicator(lang, indicatorCode);
            this.Indicator = thisIndicator;


            // open data reader
            DataReader reader = App.getDataReader();
            DataKey thisKey = new DataKey(DataReader.KEY_COUNTRYRANKING_INDICATORCODE, indicatorCode);
            
            // define sorting 
            if (sortingMode == SORT_BY_VALUE)
            {
                reader.Open(lang, DataReader.MODEL_COUNTRYRANKING_SORT_BY_VALUE, thisKey);
            }
            else if (sortingMode == SORT_BY_COUNTRY_NAME)
            {
                reader.Open(lang, DataReader.MODEL_COUNTRYRANKING_SORT_BY_NAME, thisKey);
            }
            else
            {
                return;
            }



            // launch reader
            bool firstTime = true;
            while (reader.Read())
            {
                string countryCode = reader.getString(DataReader.FIELD_COUNTRYRANKING_COUNTRYCODE);
                string countryName = reader.getString(DataReader.FIELD_COUNTRYRANKING_COUNTRYNAME);
                string periodCode = reader.getString(DataReader.FIELD_COUNTRYRANKING_PERIODCODE);
                string thisValue = reader.getString(DataReader.FIELD_COUNTRYRANKING_VALUE);

                if (firstTime)
                {
                    this.PeriodCode = periodCode;
                    firstTime = false;
                }

                CountryDataModel countryData = new CountryDataModel();
                countryData.Code = countryCode;
                countryData.Name = countryName;
                countryData.Value = thisValue;

                Ranking.Add(countryData);
            }

            reader.Close();

        }
    }
}

