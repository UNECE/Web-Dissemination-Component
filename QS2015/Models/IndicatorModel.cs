using QS2015.Connectivity;
using QS2015.Factory;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class IndicatorModel
    {
        public static string SCALE_LINEAR          = "L";
        public static string SCALE_LOG_10          = "G";
        public static string SCALE_LOG_2           = "B";

        public string Code { get; set; }
	    public string Name { get; set; }
	    public string Code4GradeValues { get; set; }
	    public string Code4ColorValuesA { get; set; }
	    public string MeasurementUnit { get; set; }
        public string Footnote { get; set; }
        public string SourceWebLink { get; set; }
        public string ColorScale { get; set; }

        public static IndicatorModel getIndicator(string lang, String indicatorCode)
        {
            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(indicatorCode))
            {
                return null;
            }
            // ****************************************************************
            
            IndicatorModel thisIndicator = null;

            DataReader reader = App.getDataReader();
            DataKey thisKey = new DataKey(DataReader.KEY_INDICATOR_INDICATORCODE, indicatorCode);
            reader.Open(lang, DataReader.MODEL_INDICATOR, thisKey);

            if (reader.Read())
            {
                string indicatorName = reader.getString(DataReader.FIELD_INDICATOR_NAME);
                string code4GradeValues = reader.getString(DataReader.FIELD_INDICATOR_CODE4GRADEVALUES);
                string code4ColorValuesA = reader.getString(DataReader.FIELD_INDICATOR_CODE4COLORVALUESA);
                string measurementUnit = reader.getString(DataReader.FIELD_INDICATOR_MEASUREMENTUNIT);
                string footnote = reader.getString(DataReader.FIELD_INDICATOR_FOOTNOTE);
                string sourcelink = reader.getString(DataReader.FIELD_INDICATOR_SOURCEWEBLINK);
                string colorScale = reader.getString(DataReader.FIELD_INDICATOR_COLOR_SCALE);

                thisIndicator = new IndicatorModel();

                thisIndicator.Code = indicatorCode;
                thisIndicator.Name = indicatorName;
                thisIndicator.Code4GradeValues = code4GradeValues;
                thisIndicator.Code4ColorValuesA = code4ColorValuesA;
                thisIndicator.MeasurementUnit = measurementUnit;
                thisIndicator.Footnote = footnote;
                thisIndicator.SourceWebLink = sourcelink;
                thisIndicator.ColorScale = colorScale;

            }

            reader.Close();

            return thisIndicator;

        }


        public static string getNextIndicatorCode(string lang, string indicatorCode)
        {
            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(indicatorCode))
            {
                return null;
            }
            // ****************************************************************

            DataReader reader = App.getDataReader();
            DataKey thisKey = new DataKey(DataReader.KEY_NEXT_INDICATOR_INDICATORCODE, indicatorCode);
            reader.Open(lang, DataReader.MODEL_NEXT_INDICATOR, thisKey);

            string resultIndicatorCode = "";

            if (reader.Read())
            {
                resultIndicatorCode = reader.getString(DataReader.FIELD_NEXT_INDICATOR_CODE);
            }

            reader.Close();

            return resultIndicatorCode;
        }


        public static string getRandomIndicatorCode(string lang)
        {
            string indicatorCode = "1";

            DataReader reader = App.getDataReader();
            reader.Open(lang, DataReader.MODEL_RANDOM_INDICATOR);

            if (reader.Read())
            {
                indicatorCode = reader.getString(DataReader.FIELD_RANDOM_INDICATOR_CODE);
            }

            reader.Close();

            return indicatorCode;

        }
    }
}



