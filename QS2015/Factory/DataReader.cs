using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Factory
{
    public abstract class DataReader
    {
        // ------------------------------------------------------------------------------------
        // COUNTRY MODEL
        public const int MODEL_COUNTRY                              = 0;
        public const int SOURCE_COUNTRY                             = 1;

        public const int FIELD_COUNTRY_NAME                         = 2;
        public const int KEY_COUNTRY_CODE                           = 3;


        // ------------------------------------------------------------------------------------
        // COUNTRIES MODEL
        public const int MODEL_COUNTRIES                            = 10;
        public const int SOURCE_COUNTRIES                           = 11;

        public const int FIELD_COUNTRIES_CODE                       = 12;
        public const int FIELD_COUNTRIES_NAME                       = 13;


        // ------------------------------------------------------------------------------------
        // PDF COUNTRY PROFILES MODEL
        public const int MODEL_PDFCOUNTRYPROFILES                   = 20;
        public const int SOURCE_PDFCOUNTRYPROFILES                  = 21;

        public const int FIELD_PDFCOUNTRYPROFILES_CODE              = 22;
        public const int FIELD_PDFCOUNTRYPROFILES_NAME              = 23;
        public const int FIELD_PDFCOUNTRYPROFILES_WEBLINK           = 24;


        // ------------------------------------------------------------------------------------
        // BRANCHES MODEL
        public const int MODEL_BRANCHES                             = 30;
        public const int SOURCE_BRANCHES                            = 31;

        public const int FIELD_BRANCHES_ID                          = 32;
        public const int FIELD_BRANCHES_SORTCODE                    = 33;
        public const int FIELD_BRANCHES_TITLE                       = 34;


        // ------------------------------------------------------------------------------------
        // BRANCHE MODEL
        public const int MODEL_BRANCHE                              = 40;
        public const int SOURCE_BRANCHE                             = 41;

        public const int FIELD_BRANCHE_TITLE                        = 42;
        public const int FIELD_BRANCHE_WEBLINK                      = 43;
        public const int FIELD_BRANCHE_SORTCODE                     = 44;
        public const int FIELD_BRANCHE_PARENTID                     = 45;
        public const int FIELD_BRANCHE_ID                           = 46;

        public const int KEY_BRANCHE_ID                             = 47;


        // ------------------------------------------------------------------------------------
        // COUNTRY PROFILE MODEL
        public const int MODEL_COUNTRYPROFILE                       = 50;
        public const int SOURCE_COUNTRYPROFILE_QUICKSTATSDATASETS   = 51;
        public const int SOURCE_COUNTRYPROFILE_QUICKSTATSINDICATORS = 52;
        public const int SOURCE_COUNTRYPROFILE_QUICKSTATSDOMAINS    = 53;
        
        public const int FIELD_COUNTRYPROFILE_NAME                  = 54;
        public const int FIELD_COUNTRYPROFILE_DOMAINNAME_ORIGINAL   = 55;
        public const int FIELD_COUNTRYPROFILE_DOMAINCODE            = 56;
        public const int FIELD_COUNTRYPROFILE_DOMAINNAME            = 57;
        public const int FIELD_COUNTRYPROFILE_INDICATORCODE         = 58;
        public const int FIELD_COUNTRYPROFILE_INDICATORNAME         = 59;
        public const int FIELD_COUNTRYPROFILE_PERIODCODE            = 60;
        public const int FIELD_COUNTRYPROFILE_MEASUREMENTUNIT       = 62;
        public const int FIELD_COUNTRYPROFILE_VALUE                 = 63;

        public const int KEY_COUNTRYPROFILE_COUNTRYCODE             = 64;
        public const int KEY_COUNTRYPROFILE_INDICATORCODE           = 65;




        // ------------------------------------------------------------------------------------
        // TIME SERIES MODEL
        public const int MODEL_TIMESERIES                           = 70;
        public const int SOURCE_TIMESERIES                          = 71;

        public const int FIELD_TIMESERIES_LINE                      = 72;

        public const int KEY_TIMESERIES_INDICATOR                   = 73;
        public const int KEY_TIMESERIES_COUNTRY                     = 74;





        // ------------------------------------------------------------------------------------
        // DOMAINS MODEL
        public const int MODEL_DOMAINS                              = 80;
        public const int SOURCE_DOMAINS                             = 81;

        public const int FIELD_DOMAINS_CODE                         = 82;





        // ------------------------------------------------------------------------------------
        // DOMAIN MODEL
        public const int MODEL_DOMAIN                               = 90;
        public const int SOURCE_DOMAIN                              = 91;

        public const int FIELD_DOMAIN_NAME                          = 92;
        public const int FIELD_DOMAIN_MEASUREMENTUNIT               = 93;
        public const int FIELD_DOMAIN_FOOTNOTE                      = 94;

        public const int KEY_DOMAIN_DOMAINCODE                      = 95;



        // ------------------------------------------------------------------------------------
        // DOMAIN MODEL, INDICATORS
        public const int MODEL_DOMAIN_INDICATORS                    = 100;
        public const int SOURCE_DOMAIN_INDICATORS                   = 101;

        public const int FIELD_DOMAIN_INDICATORS_NAME               = 102;
        public const int FIELD_DOMAIN_INDICATORS_MEASUREMENTUNIT    = 103;
        public const int FIELD_DOMAIN_INDICATORS_FOOTNOTE           = 104;
        public const int FIELD_DOMAIN_INDICATORS_CODE               = 105;
        public const int FIELD_DOMAIN_INDICATORS_CODE4GRADEVALUES   = 106;
        public const int FIELD_DOMAIN_INDICATORS_CODE4COLORVALUESA  = 107;

        public const int KEY_DOMAIN_INDICATORS_DOMAINCODE           = 108;










        // ------------------------------------------------------------------------------------
        // RANDOM INDICATOR MODEL
        public const int MODEL_RANDOM_INDICATOR                      = 120;
        public const int SOURCE_RANDOM_INDICATOR                     = 121;

        public const int FIELD_RANDOM_INDICATOR_CODE                 = 122;






        // ------------------------------------------------------------------------------------
        // INDICATOR MODEL
        public const int MODEL_INDICATOR                            = 130;
        public const int SOURCE_INDICATOR                           = 131;

        public const int FIELD_INDICATOR_CODE                       = 132;
        public const int FIELD_INDICATOR_NAME                       = 133;
        public const int FIELD_INDICATOR_CODE4GRADEVALUES           = 134;
        public const int FIELD_INDICATOR_CODE4COLORVALUESA          = 135;
        public const int FIELD_INDICATOR_MEASUREMENTUNIT            = 136;
        public const int FIELD_INDICATOR_FOOTNOTE                   = 137;
        public const int FIELD_INDICATOR_SOURCEWEBLINK              = 138;
        public const int FIELD_INDICATOR_COLOR_SCALE                = 13801;
         
        public const int KEY_INDICATOR_INDICATORCODE                = 139;





        // ------------------------------------------------------------------------------------
        // NEXT INDICATOR MODEL
        public const int MODEL_NEXT_INDICATOR                       = 150;
        public const int FIELD_NEXT_INDICATOR_CODE                  = 151;
        public const int FIELD_NEXT_INDICATOR_NAME                  = 152;
        public const int FIELD_NEXT_INDICATOR_DOMAIN                = 153;
        public const int KEY_NEXT_INDICATOR_INDICATORCODE           = 154;






        // ------------------------------------------------------------------------------------
        // COUNTRY RANKING MODEL
        public const int MODEL_COUNTRYRANKING_SORT_BY_VALUE         = 170;
        public const int MODEL_COUNTRYRANKING_SORT_BY_NAME          = 171;

        public const int FIELD_COUNTRYRANKING_COUNTRYCODE           = 172;
        public const int FIELD_COUNTRYRANKING_COUNTRYNAME           = 173;
        public const int FIELD_COUNTRYRANKING_PERIODCODE            = 174;
        public const int FIELD_COUNTRYRANKING_VALUE                 = 175;
        public const int FIELD_COUNTRYRANKING_NAME                  = 176;
         
        public const int KEY_COUNTRYRANKING_INDICATORCODE           = 177;








        // ------------------------------------------------------------------------------------
        // COUNTRY PROFILES MODEL
        public const int MODEL_COUNTRYPROFILES                      = 200;

        public const int FIELD_COUNTRYPROFILES_INDICATORCODE        = 201;
        public const int FIELD_COUNTRYPROFILES_COUNTRYCODE          = 202;
        public const int FIELD_COUNTRYPROFILES_NAME                 = 203;
        public const int FIELD_COUNTRYPROFILES_VALUE                = 204;

        public const int KEY_COUNTRYPROFILES_FIRSTCOUNTRYCODE       = 205;





        // ------------------------------------------------------------------------------------
        // HOME MODEL
        public const int MODEL_HOME                                 = 400;

        public const int FIELD_HOME_EXPIRED                         = 406;
        public const int FIELD_HOME_WEBLINK                         = 407;
        public const int FIELD_HOME_MESSAGE_TEXT                    = 408;


        // ------------------------------------------------------------------------------------
        // LANGUAGE MODEL
        public const int MODEL_LANGUAGES                            = 500;

        public const int FIELD_LANGUAGE_CODE                        = 501;
        public const int FIELD_LANGUAGE_LONG_CODE                   = 502;
        public const int FIELD_LANGUAGE_DEFAULT_LANGUAGE            = 503;


        // ------------------------------------------------------------------------------------
        // UPDATE PARAMETERS
        public const int KEY_LAST_UPDATE_ID                         = 10000;

        public abstract void Open(string lang, int source, List<DataKey> datakeys, List<AddOnParameter> addonkeys);
        public abstract void Open(string lang, int source, List<DataKey> datakeys);
        public abstract void Open(string lang, int source, DataKey datakey);
        public abstract void Open(string lang, int source);
        public abstract Boolean Read();
        public abstract string getString(int fieldId);
        public abstract void Close();

        public abstract string getTerritoriesJavascriptFilePath(string lang);
    }
}

