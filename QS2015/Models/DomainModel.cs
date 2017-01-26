using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class DomainModel
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public List<IndicatorModel> Indicators { get; set; }


        public DomainModel (string curLang, String domainCode)
        {
            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(domainCode))
            {
                return;
            }
            // ****************************************************************

            this.Indicators = new List<IndicatorModel>();

            DataReader reader = App.getDataReader();
            DataKey thisKey = new DataKey(DataReader.KEY_DOMAIN_DOMAINCODE, domainCode);
            reader.Open(curLang, DataReader.MODEL_DOMAIN, thisKey);

            while (reader.Read())
            {
                string domainName = reader.getString(DataReader.FIELD_DOMAIN_NAME);

                this.Code = domainCode;
                this.Name = domainName;
            }

            reader.Close();

            // ***********************************************************************
            // get indicators list
            DataReader readerInd = App.getDataReader();
            DataKey thisKeyInd = new DataKey(DataReader.KEY_DOMAIN_INDICATORS_DOMAINCODE, domainCode);
            readerInd.Open(curLang, DataReader.MODEL_DOMAIN_INDICATORS, thisKey);

            while (readerInd.Read())
            {
                string indicatorCode = readerInd.getString(DataReader.FIELD_DOMAIN_INDICATORS_CODE);
                string indicatorName = readerInd.getString(DataReader.FIELD_DOMAIN_INDICATORS_NAME);
                string code4GradeValues = readerInd.getString(DataReader.FIELD_DOMAIN_INDICATORS_CODE4GRADEVALUES);
                string code4ColorValuesA = readerInd.getString(DataReader.FIELD_DOMAIN_INDICATORS_CODE4COLORVALUESA);
                string measurementUnit = readerInd.getString(DataReader.FIELD_DOMAIN_INDICATORS_MEASUREMENTUNIT);
                string footnote = readerInd.getString(DataReader.FIELD_DOMAIN_INDICATORS_FOOTNOTE);

                IndicatorModel thisIndicator = new IndicatorModel();

                thisIndicator.Code = indicatorCode;
                thisIndicator.Name = indicatorName;
                thisIndicator.Code4GradeValues = code4GradeValues;
                thisIndicator.Code4ColorValuesA = code4ColorValuesA;
                thisIndicator.MeasurementUnit = measurementUnit;
                thisIndicator.Footnote = footnote;

                Indicators.Add(thisIndicator);
            }

            readerInd.Close();

        }
    }
}


