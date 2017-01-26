using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.FlatFiles
{
    public class FieldNamesConvertor
    {
        public static string getName(int fieldId)
        {
            string result = "";

            if (fieldId == DataReader.FIELD_HOME_EXPIRED) {
                result = "EXPIREDATE";

            } else if (fieldId == DataReader.FIELD_HOME_WEBLINK) {
                result = "WEBLINK";

            } else if (fieldId == DataReader.FIELD_HOME_MESSAGE_TEXT) {
                result = "MESSAGETEXT";
            }
            else if (fieldId == DataReader.FIELD_PDFCOUNTRYPROFILES_CODE)
            {
                result = "CODE";
            }
            else if (fieldId == DataReader.FIELD_PDFCOUNTRYPROFILES_NAME)
            {
                result = "NAME";
            }
            else if (fieldId == DataReader.FIELD_PDFCOUNTRYPROFILES_WEBLINK)
            {
                result = "WEBLINK";
            }
            else if (fieldId == DataReader.FIELD_COUNTRIES_CODE)
            {
                result = "CODE";
            }
            else if (fieldId == DataReader.FIELD_COUNTRIES_NAME)
            {
                result = "NAME";
            }
            else if (fieldId == DataReader.FIELD_DOMAINS_CODE)
            {
                result = "CODE";
            }
            else if (fieldId == DataReader.FIELD_DOMAIN_NAME)
            {
                result = "NAME";
            }
            else if (fieldId == DataReader.FIELD_DOMAIN_INDICATORS_CODE)
            {
                result = "CODE";
            }
            else if (fieldId == DataReader.FIELD_DOMAIN_INDICATORS_NAME)
            {
                result = "NAME";
            }
            else if (fieldId == DataReader.FIELD_DOMAIN_INDICATORS_CODE4GRADEVALUES)
            {
                result = "CODE4GRADEVALUES";
            }
            else if (fieldId == DataReader.FIELD_DOMAIN_INDICATORS_CODE4COLORVALUESA)
            {
                result = "CODE4COLORVALUESA";
            }
            else if (fieldId == DataReader.FIELD_DOMAIN_INDICATORS_MEASUREMENTUNIT)
            {
                result = "MEASUREMENTUNIT";
            }
            else if (fieldId == DataReader.FIELD_DOMAIN_INDICATORS_FOOTNOTE)
            {
                result = "FOOTNOTE";
            }
            else if (fieldId == DataReader.FIELD_INDICATOR_CODE)                
            {
                result = "CODE";
            }

            else if (fieldId == DataReader.FIELD_INDICATOR_NAME)                
            {
                result = "NAME";
            }

            else if (fieldId == DataReader.FIELD_INDICATOR_CODE4GRADEVALUES)                
            {
                result = "CODE4GRADEVALUES";
            }

            else if (fieldId == DataReader.FIELD_INDICATOR_CODE4COLORVALUESA)                
            {
                result = "CODE4COLORVALUESA";
            }

            else if (fieldId == DataReader.FIELD_INDICATOR_MEASUREMENTUNIT)                
            {
                result = "MEASUREMENTUNIT";
            }

            else if (fieldId == DataReader.FIELD_INDICATOR_FOOTNOTE)                
            {
                result = "FOOTNOTE";
            }

            else if (fieldId == DataReader.FIELD_INDICATOR_SOURCEWEBLINK)                
            {
                result = "SOURCEWEBLINK";
            }

            else if (fieldId == DataReader.FIELD_INDICATOR_COLOR_SCALE)                
            {
                result = "COLORSCALE";
            }
            else if (fieldId == DataReader.FIELD_COUNTRYRANKING_COUNTRYCODE)
            {
                result = "COUNTRYCODE";
            }

            else if (fieldId == DataReader.FIELD_COUNTRYRANKING_COUNTRYNAME)
            {
                result = "COUNTRYNAME";
            }

            else if (fieldId == DataReader.FIELD_COUNTRYRANKING_PERIODCODE)
            {
                result = "PERIODCODE";
            }

            else if (fieldId == DataReader.FIELD_COUNTRYRANKING_VALUE)
            {
                result = "VALUE";
            }

            else if (fieldId == DataReader.FIELD_RANDOM_INDICATOR_CODE)
            {
                result = "CODE";
            }

            else if (fieldId == DataReader.FIELD_NEXT_INDICATOR_CODE)
            {
                result = "CODE";
            }
            else if (fieldId == DataReader.FIELD_COUNTRY_NAME)
            {
                result = "NAME";
            }
            else if (fieldId == DataReader.FIELD_TIMESERIES_LINE)
            {
                result = "LINE";
            }
            else if (fieldId == DataReader.FIELD_COUNTRYPROFILE_PERIODCODE)
            {
                result = "PERIODCODE";
            }
            else if (fieldId == DataReader.FIELD_COUNTRYPROFILE_VALUE)
            {
                result = "VALUE";
            }
            else if (fieldId == DataReader.FIELD_COUNTRYPROFILES_INDICATORCODE)
            {
                result = "INDICATORCODE";
            }

            else if (fieldId == DataReader.FIELD_COUNTRYPROFILES_COUNTRYCODE)
            {
                result = "COUNTRYCODE";
            }

            else if (fieldId == DataReader.FIELD_COUNTRYPROFILES_NAME)
            {
                result = "NAME";
            }

            else if (fieldId == DataReader.FIELD_COUNTRYPROFILES_VALUE)
            {
                result = "VALUE";
            }
            else if (fieldId == DataReader.FIELD_LANGUAGE_CODE)
            {
                result = "CODE";
            }
            else if (fieldId == DataReader.FIELD_LANGUAGE_LONG_CODE)
            {
                result = "LONGCODE";
            }
            else if (fieldId == DataReader.FIELD_LANGUAGE_DEFAULT_LANGUAGE)
            {
                result = "DEFAULTLANGUAGE";
            }

            return result;
        }
    }
}