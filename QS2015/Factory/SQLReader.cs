using QS2015.Connectivity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Factory
{
    public class SQLReader : DataReader
    {
        public const string FILE_NAME_TERRITORIES = "territories_";

        private SqlDataReader thisReader = null;
        private SqlConnection thisConnection = null;
        SqlCommand thisCommand = null;
        private string thisLang = null;

        public SQLReader()
        {

        }

        public override string getTerritoriesJavascriptFilePath(string lang)
        {
            String strTerritoriesFilesFolder = App.getAppSettingsKeyValue("TerritoriesFileFolder").Replace("\\", "/"); ;

            if (
                !(strTerritoriesFilesFolder.EndsWith("\\") || strTerritoriesFilesFolder.EndsWith("/"))
                )
            {
                strTerritoriesFilesFolder = strTerritoriesFilesFolder + "/";
            }


            string strFileNameTerritories = strTerritoriesFilesFolder + FILE_NAME_TERRITORIES + lang + ".js";
            return strFileNameTerritories;
        }

        public override void Open(string lang, int source, List<DataKey> dataKeys, List<AddOnParameter> addOnKeys)
        {
            thisLang = lang;
            thisConnection = DataLink.getConnection();

            // get SQL string and keys
            string sqlCode = this.getSQLString(source);
            List<AddOnParameter> resultAddOnKeys = this.getSQLParameters(source, dataKeys);

            string resultSqlCode = "";

            if (addOnKeys == null)
            {
                resultSqlCode = sqlCode;
            }
            else
            {
                // inject SQL parameters into the original SQL code
                string inclause = "";

                foreach (AddOnParameter thissqlkey in addOnKeys)
                {
                    if (inclause.Equals("")) {
                        inclause = thissqlkey.Name;
                    } else {
                        inclause = inclause + "," + thissqlkey.Name;
                    }
                }

                resultSqlCode = string.Format(sqlCode, inclause);
            }


            thisCommand = new SqlCommand(resultSqlCode, thisConnection);

            // add additional parameters to SQL keys result list
            if (addOnKeys != null)
            {
                resultAddOnKeys.AddRange(addOnKeys);
            } 

            // add parameters to SQL command
            if (resultAddOnKeys != null)
            {
                foreach (AddOnParameter thiskey in resultAddOnKeys)
                {
                    thisCommand.Parameters.AddWithValue(thiskey.Name, thiskey.Value);
                }
            }

            // open reader
            thisReader = thisCommand.ExecuteReader();

        }
        

        public override void Open(string lang, int source, List<DataKey> datakeys)
        {
            this.Open (lang, source, datakeys, null);
        }

        public override void Open(string lang, int source, DataKey datakey)
        {
            List<DataKey> keysList = new List<DataKey>();
            keysList.Add(datakey);

            this.Open(lang, source, keysList);
        }


        public override void Open(string lang, int source)
        {
            List<DataKey> keysList = new List<DataKey>();
            this.Open(lang, source, keysList);
        }

        public override void Close()
        {
            thisReader.Close();
            thisReader.Dispose();

            thisCommand.Dispose();

            thisConnection.Close();
            thisConnection.Dispose();
        }

        public override Boolean Read()
        {
            return thisReader.Read();
        }

        public override string getString(int fieldId)
        {
            string fieldName = this.getObjectName(fieldId);
            return thisReader[fieldName].ToString();
        }

        private List<AddOnParameter> getSQLParameters(int source, List<DataKey> datakeys)
        {
            List<AddOnParameter> sqlParams = new List<AddOnParameter>();

            if (datakeys != null)
            {
                foreach (DataKey thiskey in datakeys)
                {
                    string paramName = this.getObjectName(thiskey.Id);

                    if (paramName == null)
                    {
                        return null;
                    }
                    else
                    {
                        AddOnParameter thisParam = new AddOnParameter(paramName, thiskey.Value);
                        sqlParams.Add(thisParam);
                    }
                }
            }












            // **********************************************************************************************************************************
            // customize SQL queries
            // **********************************************************************************************************************************

            string latestUpdateId = DataLink.getLatestUpdate(thisConnection);
            
            switch (source)
            {
                case DataReader.MODEL_COUNTRY:
                    AddOnParameter thisParam1 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam1);
                    break;

                case DataReader.MODEL_COUNTRIES:
                    AddOnParameter thisParam2 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam2);
                    break;

                case DataReader.MODEL_PDFCOUNTRYPROFILES:
                    break;

                case DataReader.MODEL_BRANCHES:
                    break;

                case DataReader.MODEL_COUNTRYPROFILE:
                    AddOnParameter thisParam3 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam3);
                    break;

                case DataReader.MODEL_TIMESERIES:
                    AddOnParameter thisParam4 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam4);
                    break;

                case DataReader.MODEL_DOMAINS:
                    AddOnParameter thisParam5 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam5);
                    break;

                case DataReader.MODEL_DOMAIN:
                    AddOnParameter thisParam6 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam6);
                    break;

                case DataReader.MODEL_DOMAIN_INDICATORS:
                    AddOnParameter thisParam7 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam7);
                    break;

                case DataReader.MODEL_RANDOM_INDICATOR:
                    AddOnParameter thisParam8 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam8);
                    break;

                case DataReader.MODEL_INDICATOR:
                    AddOnParameter thisParam9 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam9);
                    break;

                case DataReader.MODEL_NEXT_INDICATOR:
                    AddOnParameter thisParam10 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam10);
                    break;

                case DataReader.MODEL_COUNTRYRANKING_SORT_BY_VALUE:
                    AddOnParameter thisParam11 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam11);
                    break;

                case DataReader.MODEL_COUNTRYRANKING_SORT_BY_NAME:
                    AddOnParameter thisParam12 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam12);
                    break;

                case DataReader.MODEL_COUNTRYPROFILES:
                    AddOnParameter thisParam13 = new AddOnParameter(this.getObjectName(DataReader.KEY_LAST_UPDATE_ID), latestUpdateId);
                    sqlParams.Add(thisParam13);
                    break;


            }

            return sqlParams;
        }

        private string getSQLString(int source)
        {
            string sqlCode = null;

            switch (source)
            {
                case DataReader.MODEL_COUNTRY:
                    string tableName1 = this.getObjectName(DataReader.SOURCE_COUNTRY);
                    string nameField1 = this.getObjectName(DataReader.FIELD_COUNTRY_NAME);

                    string keyCountryCode1 = this.getObjectName(DataReader.KEY_COUNTRY_CODE);
                    string keyLastUpdateId1 = this.getObjectName(DataReader.KEY_LAST_UPDATE_ID);

                    sqlCode =
                            "SELECT " + nameField1 + " " +
                            "FROM " + tableName1 + " " +
                            "WHERE " +
                                "UPDATEID = @" + keyLastUpdateId1 + " AND " + 
                                "CODE = @" + keyCountryCode1;
                    break;


                case DataReader.MODEL_COUNTRIES:
                    string tableName2 = this.getObjectName(DataReader.SOURCE_COUNTRIES);
                    string codeField2 = this.getObjectName(DataReader.FIELD_COUNTRIES_CODE);
                    string nameField2 = this.getObjectName(DataReader.FIELD_COUNTRIES_NAME);

                    string keyLastUpdateId2 = this.getObjectName(DataReader.KEY_LAST_UPDATE_ID);

                    sqlCode =
                            "SELECT " + codeField2 + ", " + nameField2 + " " +
                            "FROM " + tableName2 + " " +
                            "WHERE " +
                                "UPDATEID = @" + keyLastUpdateId2 + " " +
                            "ORDER BY " + nameField2;
                    break;


                case DataReader.MODEL_PDFCOUNTRYPROFILES:
                    string tableName3 = this.getObjectName(DataReader.SOURCE_PDFCOUNTRYPROFILES);
                    string codeField3 = this.getObjectName(DataReader.FIELD_PDFCOUNTRYPROFILES_CODE);
                    string nameField3 = this.getObjectName(DataReader.FIELD_PDFCOUNTRYPROFILES_NAME);
                    string weblinkField3 = this.getObjectName(DataReader.FIELD_PDFCOUNTRYPROFILES_WEBLINK);

                    sqlCode =
                            "SELECT " + codeField3 + ", " + nameField3 + ", " + weblinkField3 + " " +
                            "FROM " + tableName3 + " " +
                            "ORDER BY " + nameField3;
                    break;



                case DataReader.MODEL_BRANCHES:
                    string tableName4 = this.getObjectName(DataReader.SOURCE_BRANCHES);
                    string idField4 = this.getObjectName(DataReader.FIELD_BRANCHES_ID);
                    string sortcodeField4 = this.getObjectName(DataReader.FIELD_BRANCHES_SORTCODE);
                    string titleField4 = this.getObjectName(DataReader.FIELD_BRANCHES_TITLE);

                    sqlCode =
                            "SELECT " + idField4 + ", " + sortcodeField4 + ", " + titleField4 + " " +
                            "FROM " + tableName4 + " " +
                            "ORDER BY " + sortcodeField4;
                    break;




                case DataReader.MODEL_BRANCHE:
                    string tableName5 = this.getObjectName(DataReader.SOURCE_BRANCHE);
                    string sortcodeField5 = this.getObjectName(DataReader.FIELD_BRANCHE_SORTCODE);
                    string parentIdField5 = this.getObjectName(DataReader.FIELD_BRANCHE_PARENTID);
                    string idField5 = this.getObjectName(DataReader.FIELD_BRANCHE_ID);
                    string titleField5 = this.getObjectName(DataReader.FIELD_BRANCHE_TITLE);
                    string weblinkField5 = this.getObjectName(DataReader.FIELD_BRANCHE_WEBLINK);                    

                    sqlCode =
                            "SELECT " +
                                sortcodeField5 + ", " +
                                parentIdField5 + ", " +
                                idField5 + ", " +
                                titleField5 + ", " +
                                weblinkField5 + " " +
                            "FROM  " + tableName5 + " " +
                            "WHERE " + parentIdField5 + " = @id" + " " +
                            "ORDER BY " + sortcodeField5;

                    break;



                case DataReader.MODEL_COUNTRYPROFILE:
                    string periodCode6 = this.getObjectName(DataReader.FIELD_COUNTRYPROFILE_PERIODCODE);
                    string thisValue6 = this.getObjectName(DataReader.FIELD_COUNTRYPROFILE_VALUE);

                    string quickStatsDatasets = this.getObjectName(DataReader.SOURCE_COUNTRYPROFILE_QUICKSTATSDATASETS);

                    sqlCode =
                        "SELECT " + periodCode6 + ", " + thisValue6 + " " + 
                        "FROM " + quickStatsDatasets + " " + 
                        "WHERE " +
                            "UpdateId = @latestUpdateId AND " +
                            "CountryCode = @countryCode AND	" +
                            "IndicatorCode = @indicatorCode ";
                    break;




                case DataReader.MODEL_TIMESERIES:
                    string tableName6 = this.getObjectName(DataReader.SOURCE_TIMESERIES);
                    string lineName6 = this.getObjectName(DataReader.FIELD_TIMESERIES_LINE);

                    sqlCode =
                            "SELECT " + lineName6 + " " +
                            "FROM " + tableName6 + " " +
                            "WHERE " + 
                                "UpdateId = @latestUpdateId AND " + 
                                lineName6 + " " + "LIKE " + "@INDICATOR + ',' + @COUNTRY + '%' " +
                            "ORDER BY " + lineName6;
                    break;





                case DataReader.MODEL_DOMAINS:
                    string tableName7 = this.getObjectName(DataReader.SOURCE_DOMAINS);
                    string codeName7 = this.getObjectName(DataReader.FIELD_DOMAINS_CODE);

                    sqlCode = 
                        "SELECT " + codeName7 + " " +
                        "FROM " + tableName7 + " " +
                        "WHERE UPDATEID=@LATESTUPDATEID " + 
                        "ORDER BY Name";
                    break;




                case DataReader.MODEL_DOMAIN:
                    string tableName8 = this.getObjectName(DataReader.SOURCE_DOMAIN);
                    string nameField8 = this.getObjectName(DataReader.FIELD_DOMAIN_NAME);

                    sqlCode =
                        "SELECT " + nameField8 + " " + 
                        "FROM " + tableName8 + " " +
                        "WHERE UPDATEID = @LATESTUPDATEID AND CODE = @DOMAINCODE " +
                        "ORDER BY " + nameField8;                    
                    break;





                case DataReader.MODEL_DOMAIN_INDICATORS:
                    string tableName9 = this.getObjectName(DataReader.SOURCE_DOMAIN_INDICATORS);
                    string nameField9 = this.getObjectName(DataReader.FIELD_DOMAIN_INDICATORS_NAME);
                    string measurementUnitField9 = this.getObjectName(DataReader.FIELD_DOMAIN_INDICATORS_MEASUREMENTUNIT);
                    string footnoteField9 = this.getObjectName(DataReader.FIELD_DOMAIN_INDICATORS_FOOTNOTE);
                    string gradeField9 = this.getObjectName(DataReader.FIELD_DOMAIN_INDICATORS_CODE4GRADEVALUES);
                    string colorField9 = this.getObjectName(DataReader.FIELD_DOMAIN_INDICATORS_CODE4COLORVALUESA);

                    sqlCode =
                        "SELECT " +
                            "I.Code, " +
                            "I." + nameField9 + ", " +
                            "I." + gradeField9 + ", " +
                            "I." + colorField9 + ", " +
                            "I." + measurementUnitField9 + ", " +
                            "I." + footnoteField9 + " " +
                        "FROM " + tableName9 + " As I " +
                        "WHERE I.UpdateId = @latestUpdateId And I.Domain_Id = @domainCode " +
                        "ORDER BY I." + nameField9;
                    break;






                case DataReader.MODEL_RANDOM_INDICATOR:
                    string tableName10 = this.getObjectName(DataReader.SOURCE_RANDOM_INDICATOR);
                    string codeName10 = this.getObjectName(DataReader.FIELD_RANDOM_INDICATOR_CODE);
                    sqlCode =
                        "SELECT TOP 1 " + codeName10 + " FROM  " + tableName10 + " WHERE UpdateId=@latestUpdateId ORDER BY NEWID()";
                    break;






                case DataReader.MODEL_INDICATOR:
                    string tableName11 = this.getObjectName(DataReader.SOURCE_INDICATOR);
                    string codeField11 = this.getObjectName(DataReader.FIELD_INDICATOR_CODE);
                    string nameField11 = this.getObjectName(DataReader.FIELD_INDICATOR_NAME);
                    string measurementUnitField11 = this.getObjectName(DataReader.FIELD_INDICATOR_MEASUREMENTUNIT);
                    string footnoteField11 = this.getObjectName(DataReader.FIELD_INDICATOR_FOOTNOTE);
                    string sourcewebField11 = this.getObjectName(DataReader.FIELD_INDICATOR_SOURCEWEBLINK);
                    string colorScaleField11 = this.getObjectName(DataReader.FIELD_INDICATOR_COLOR_SCALE);
                    string gradeField11 = this.getObjectName(DataReader.FIELD_INDICATOR_CODE4GRADEVALUES);
                    string colorField11 = this.getObjectName(DataReader.FIELD_INDICATOR_CODE4COLORVALUESA);

                    sqlCode =
                        "SELECT " +
                            "I." + codeField11 + ", " +
                            "I." + nameField11 + ", " +
                            "I." + gradeField11 + ", " +
                            "I." + colorField11 + ", " +
                            "I." + measurementUnitField11 + ", " +
                            "I." + footnoteField11 + ", " +
                            "I." + colorScaleField11 + ", " +                         
                            "I." + sourcewebField11 + " " +
                        "FROM " + tableName11 + " As I " +
                        "WHERE UpdateId=@latestUpdateId And Code=@indicatorCode";                        
                    break;










                case DataReader.MODEL_NEXT_INDICATOR:
                    string codeField12 = this.getObjectName(DataReader.FIELD_NEXT_INDICATOR_CODE);
                    string nameField12 = this.getObjectName(DataReader.FIELD_NEXT_INDICATOR_NAME);
                    string domainField12 = this.getObjectName(DataReader.FIELD_NEXT_INDICATOR_DOMAIN);

                    sqlCode = 
                        "SELECT TOP 1 " + codeField12 + " " +
                        "FROM  " +
                        "    ( " +
                        "    SELECT I." + nameField12 + ", I.Code, ROW_NUMBER() OVER (ORDER BY D." + domainField12 + ", I." + nameField12 + ") AS SortCode " +
                        "    FROM   " +
                        "        dbo.QuickStatsIndicators as I, " +
                        "        dbo.QuickStatsDomains as D " +
                        "    WHERE  " +
                        "        I.Domain_Id = D.Code AND " +
                        "        I.UpdateID = D.UpdateId And " +
                        "        I.UpdateID = @latestUpdateId  " +
                        "    ) As Z " +
                        "WHERE SortCode >  " +
                        "    ( " +
                        "    SELECT SortCode " +
                        "    FROM  " +
                        "        ( " +
                        "        SELECT I." + nameField12 + ", I.Code, ROW_NUMBER() OVER (ORDER BY D." + domainField12 + ", I." + nameField12 + ") AS SortCode " +
                        "        FROM   " +
                        "            dbo.QuickStatsIndicators as I, " +
                        "            dbo.QuickStatsDomains as D " +
                        "        WHERE  " +
                        "            I.Domain_Id = D.Code AND " +
                        "            I.UpdateID = D.UpdateId And " +
                        "            I.UpdateID = @latestUpdateId " +
                        "        ) As T " +
                        "    WHERE Code = @indicatorCode " +
                        "    ) ";
                    break;









                case DataReader.MODEL_COUNTRYRANKING_SORT_BY_VALUE:
                    string nameField13 = this.getObjectName(DataReader.FIELD_COUNTRYRANKING_NAME);
                    
                    sqlCode =
                        "SELECT D.CountryCode, T." + nameField13 + " As CountryName , D.PeriodCode, D.Value  " +
                        "FROM dbo.QuickStatsDatasets As D, dbo.QuickStatsTerritories As T " +
                        "WHERE " +
                        "	D.CountryCode = T.Code AND " +
                        "	T.UpdateId = @latestUpdateId AND " +
                        "	D.UpdateId = @latestUpdateId AND " +
                        "	D.IndicatorCode = @indicatorCode " +
                        "ORDER BY " + "CAST(D.Value AS FLOAT) DESC, D.CountryCode ASC ";
                    break;


                case DataReader.MODEL_COUNTRYRANKING_SORT_BY_NAME:
                    string nameField14 = this.getObjectName(DataReader.FIELD_COUNTRYRANKING_NAME);

                    sqlCode =
                        "SELECT D.CountryCode, T." + nameField14 + " As CountryName , D.PeriodCode, D.Value  " +
                        "FROM dbo.QuickStatsDatasets As D, dbo.QuickStatsTerritories As T " +
                        "WHERE " +
                        "	D.CountryCode = T.Code AND " +
                        "	T.UpdateId = @latestUpdateId AND " +
                        "	D.UpdateId = @latestUpdateId AND " +
                        "	D.IndicatorCode = @indicatorCode " +
                        "ORDER BY " + "T." + nameField14 + " ASC";

                    break;




                case DataReader.MODEL_COUNTRYPROFILES:
                    string nameField15 = this.getObjectName(DataReader.FIELD_COUNTRYPROFILES_NAME);
                    sqlCode =
                        "SELECT " +
                            "V.INDICATORCODE, " +
                            "V.COUNTRYCODE, " +
                            "T." + nameField15 + ", " +
                            "V.VALUE " +
                        "FROM  " +
                        "	DBO.QUICKSTATSTERRITORIES AS T,  " +
                        "	DBO.QUICKSTATSVALUES AS V, " +
                        "	( " +
                        "	SELECT INDICATORCODE, PERIODCODE  " +
                        "	FROM QUICKSTATSDATASETS " +
                        "	WHERE  " +
                        "		UPDATEID=@LATESTUPDATEID AND " +
                        "		COUNTRYCODE=@FIRSTCOUNTRYCODE " +
                        "	) AS D " +
                        "WHERE  " +
                        "	V.COUNTRYCODE = T.CODE AND " +
                        "	T.UPDATEID = @LATESTUPDATEID AND " +
                        "	D.INDICATORCODE = V.INDICATORCODE AND " +
                        "	D.PERIODCODE = V.PERIODCODE AND " +
                        "	V.UPDATEID = @LATESTUPDATEID AND " +
                        "	V.COUNTRYCODE IN ({0}) " +
                        "ORDER BY V.INDICATORCODE ASC, T." + nameField15 + " DESC";

                    break;





                case DataReader.MODEL_HOME:
                    string nameFieldWebLink = this.getObjectName(DataReader.FIELD_HOME_WEBLINK);
                    string nameFieldMessageText = this.getObjectName(DataReader.FIELD_HOME_MESSAGE_TEXT);

                    sqlCode =
                        "SELECT TOP 1 " +
                            "[Id], " +
                            "[" + nameFieldWebLink + "], " +
                            "[" + nameFieldMessageText + "], " +
                            "CONVERT(VARCHAR(100), [ExpireDate], 20) AS Expired " + 
                        "FROM [PCAxis].[dbo].[WebSiteMessages] " +
                        "ORDER BY [Id] DESC";

                    break;




                case DataReader.MODEL_LANGUAGES:
                    string nameLanguageCode = this.getObjectName(DataReader.FIELD_LANGUAGE_CODE);
                    string nameLanguageLongCode = this.getObjectName(DataReader.FIELD_LANGUAGE_LONG_CODE);
                    string nameLanguageDefaultLanguage = this.getObjectName(DataReader.FIELD_LANGUAGE_DEFAULT_LANGUAGE);

                    sqlCode =
                        "SELECT " +
                            "[" + nameLanguageCode + "], " +
                            "[" + nameLanguageLongCode + "], " +
                            "[" + nameLanguageDefaultLanguage + "] " +
                        "FROM [PCAxis].[dbo].[QuickStatsLanguages] "
                        ;

                    break;



                default:
                    sqlCode = null;
                    break;
            }

            return sqlCode;
        }

        private string getObjectName(int source)
        {
            string objectName = null;

            switch (source)
            {
                case DataReader.KEY_LAST_UPDATE_ID:
                    objectName = "LATESTUPDATEID";
                    break;





                case DataReader.SOURCE_COUNTRY:
                    objectName = "DBO.QUICKSTATSTERRITORIES";
                    break;

                case DataReader.FIELD_COUNTRY_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.KEY_COUNTRY_CODE:
                    objectName = "COUNTRYCODE";
                    break;





                case DataReader.SOURCE_COUNTRIES:
                    objectName = "DBO.QUICKSTATSTERRITORIES";
                    break;

                case DataReader.FIELD_COUNTRIES_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.FIELD_COUNTRIES_CODE:
                    objectName = "CODE";
                    break;






                case DataReader.SOURCE_PDFCOUNTRYPROFILES:
                    objectName = "DBO.QUICKSTATSCOUNTRYPROFILES";
                    break;

                case DataReader.FIELD_PDFCOUNTRYPROFILES_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.FIELD_PDFCOUNTRYPROFILES_CODE:
                    objectName = "CODE";
                    break;

                case DataReader.FIELD_PDFCOUNTRYPROFILES_WEBLINK:
                    objectName = "WEBLINK";
                    break;






                case DataReader.SOURCE_BRANCHES:
                    objectName = "DBO.QUICKSTATSBRANCHES";
                    break;

                case DataReader.FIELD_BRANCHES_ID:
                    objectName = "ID";
                    break;

                case DataReader.FIELD_BRANCHES_SORTCODE:
                    objectName = "SORTCODE";
                    break;

                case DataReader.FIELD_BRANCHES_TITLE:
                    objectName = "TITLE" + "_" + thisLang;
                    break;





                case DataReader.SOURCE_BRANCHE:
                    objectName = "DBO.QUICKSTATSCUBEGROUP";
                    break;

                case DataReader.FIELD_BRANCHE_TITLE:
                    objectName = "TITLE" + "_" + thisLang;
                    break;

                case DataReader.FIELD_BRANCHE_WEBLINK:
                    objectName = "WEBLINK" + "_" + thisLang;
                    break;

                case DataReader.FIELD_BRANCHE_SORTCODE:
                    objectName = "SORTCODE";
                    break;

                case DataReader.FIELD_BRANCHE_PARENTID:
                    objectName = "PARENTID";
                    break;

                case DataReader.FIELD_BRANCHE_ID:
                    objectName = "ID";
                    break;

                case DataReader.KEY_BRANCHE_ID:
                    objectName = "ID";
                    break;

                    





                case DataReader.SOURCE_COUNTRYPROFILE_QUICKSTATSDATASETS:
                    objectName = "DBO.QUICKSTATSDATASETS";
                    break;

                case DataReader.SOURCE_COUNTRYPROFILE_QUICKSTATSINDICATORS:
                    objectName = "DBO.QUICKSTATSINDICATORS";
                    break;

                case DataReader.SOURCE_COUNTRYPROFILE_QUICKSTATSDOMAINS:
                    objectName = "DBO.QUICKSTATSDOMAINS";
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_DOMAINNAME_ORIGINAL:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_DOMAINCODE:
                    objectName = "DOMAINCODE";
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_DOMAINNAME:
                    objectName = "DOMAINNAME";
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_INDICATORCODE:
                    objectName = "INDICATORCODE";
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_INDICATORNAME:
                    objectName = "INDICATORNAME";
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_PERIODCODE:
                    objectName = "PERIODCODE";
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_MEASUREMENTUNIT:
                    objectName = "MEASUREMENTUNIT" + "_" + thisLang;
                    break;

                case DataReader.FIELD_COUNTRYPROFILE_VALUE:
                    objectName = "VALUE";
                    break;

                case DataReader.KEY_COUNTRYPROFILE_COUNTRYCODE:
                    objectName = "COUNTRYCODE";
                    break;

                case DataReader.KEY_COUNTRYPROFILE_INDICATORCODE:
                    objectName = "INDICATORCODE";
                    break;










                case DataReader.SOURCE_TIMESERIES:
                    objectName = "DBO.QUICKSTATSTIMESERIES";
                    break;

                case DataReader.FIELD_TIMESERIES_LINE:
                    objectName = "LINE";
                    break;

                case DataReader.KEY_TIMESERIES_INDICATOR:
                    objectName = "INDICATOR";
                    break;

                case DataReader.KEY_TIMESERIES_COUNTRY:
                    objectName = "COUNTRY";
                    break;











                case DataReader.SOURCE_DOMAINS:
                    objectName = "DBO.QUICKSTATSDOMAINS";
                    break;

                case DataReader.FIELD_DOMAINS_CODE:
                    objectName = "CODE";
                    break;










               case DataReader.SOURCE_DOMAIN:
                    objectName = "DBO.QUICKSTATSDOMAINS";
                    break;

                case DataReader.FIELD_DOMAIN_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.FIELD_DOMAIN_MEASUREMENTUNIT:
                    objectName = "MEASUREMENTUNIT" + "_" + thisLang;
                    break;

                case DataReader.FIELD_DOMAIN_FOOTNOTE:
                    objectName = "FOOTNOTE" + "_" + thisLang;
                    break;

                case DataReader.KEY_DOMAIN_DOMAINCODE:
                    objectName = "DOMAINCODE";
                    break;

        








                case DataReader.SOURCE_DOMAIN_INDICATORS:
                    objectName = "DBO.QUICKSTATSINDICATORS";
                    break;

                case DataReader.FIELD_DOMAIN_INDICATORS_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.FIELD_DOMAIN_INDICATORS_MEASUREMENTUNIT:
                    objectName = "MEASUREMENTUNIT" + "_" + thisLang;
                    break;

                case DataReader.FIELD_DOMAIN_INDICATORS_FOOTNOTE:
                    objectName = "FOOTNOTE" + "_" + thisLang;
                    break;

                case DataReader.FIELD_DOMAIN_INDICATORS_CODE:
                    objectName = "CODE";
                    break;

                case DataReader.FIELD_DOMAIN_INDICATORS_CODE4GRADEVALUES:
                    objectName = "CODE4GRADEVALUES";
                    break;

                case DataReader.FIELD_DOMAIN_INDICATORS_CODE4COLORVALUESA:
                    objectName = "CODE4COLORVALUESA";
                    break;

                case DataReader.KEY_DOMAIN_INDICATORS_DOMAINCODE:
                    objectName = "DOMAINCODE";
                    break;








                case DataReader.SOURCE_RANDOM_INDICATOR:
                    objectName = "DBO.QUICKSTATSINDICATORS";
                    break;

                case DataReader.FIELD_RANDOM_INDICATOR_CODE:
                    objectName = "CODE";
                    break;










                case DataReader.SOURCE_INDICATOR:
                    objectName = "DBO.QUICKSTATSINDICATORS";
                    break;

                case DataReader.FIELD_INDICATOR_CODE:
                    objectName = "CODE";
                    break;

                case DataReader.FIELD_INDICATOR_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.FIELD_INDICATOR_CODE4GRADEVALUES:
                    objectName = "CODE4GRADEVALUES";
                    break;

                case DataReader.FIELD_INDICATOR_CODE4COLORVALUESA:
                    objectName = "CODE4COLORVALUESA";
                    break;

                case DataReader.FIELD_INDICATOR_MEASUREMENTUNIT:
                    objectName = "MEASUREMENTUNIT" + "_" + thisLang;
                    break;

                case DataReader.FIELD_INDICATOR_FOOTNOTE:
                    objectName = "FOOTNOTE" + "_" + thisLang;
                    break;

                case DataReader.FIELD_INDICATOR_SOURCEWEBLINK:
                    objectName = "SOURCEWEBLINK" + "_" + thisLang;
                    break;

                case DataReader.FIELD_INDICATOR_COLOR_SCALE:
                    objectName = "COLORSCALE";
                    break;

                case DataReader.KEY_INDICATOR_INDICATORCODE:
                    objectName = "INDICATORCODE";
                    break;


                    






                case DataReader.FIELD_NEXT_INDICATOR_CODE:
                    objectName = "CODE";
                    break;

                case DataReader.FIELD_NEXT_INDICATOR_NAME:
                    objectName = "NAME";
                    break;

                case DataReader.FIELD_NEXT_INDICATOR_DOMAIN:
                    objectName = "NAME";
                    break;

                case DataReader.KEY_NEXT_INDICATOR_INDICATORCODE:
                    objectName = "INDICATORCODE";
                    break;

        








                case DataReader.FIELD_COUNTRYRANKING_COUNTRYCODE:
                    objectName = "COUNTRYCODE";
                    break;

                case DataReader.FIELD_COUNTRYRANKING_COUNTRYNAME:
                    objectName = "COUNTRYNAME";
                    break;

                case DataReader.FIELD_COUNTRYRANKING_PERIODCODE:
                    objectName = "PERIODCODE";
                    break;

                case DataReader.FIELD_COUNTRYRANKING_VALUE:
                    objectName = "VALUE";
                    break;

                case DataReader.FIELD_COUNTRYRANKING_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.KEY_COUNTRYRANKING_INDICATORCODE:
                    objectName = "INDICATORCODE";
                    break;








                            
                case DataReader.FIELD_COUNTRYPROFILES_INDICATORCODE:
                    objectName = "INDICATORCODE";
                    break;

                case DataReader.FIELD_COUNTRYPROFILES_COUNTRYCODE:
                    objectName = "COUNTRYCODE";
                    break;

                case DataReader.FIELD_COUNTRYPROFILES_NAME:
                    objectName = "NAME" + "_" + thisLang;
                    break;

                case DataReader.FIELD_COUNTRYPROFILES_VALUE:
                    objectName = "VALUE";
                    break;

                case DataReader.KEY_COUNTRYPROFILES_FIRSTCOUNTRYCODE:
                    objectName = "FIRSTCOUNTRYCODE";
                    break;









                case DataReader.FIELD_HOME_WEBLINK:
                    objectName = "WEBLINK" + "_" + thisLang;
                    break;

                case DataReader.FIELD_HOME_MESSAGE_TEXT:
                    objectName = "TEXT" + "_" + thisLang;
                    break;

                case DataReader.FIELD_HOME_EXPIRED:
                    objectName = "EXPIRED";
                    break;




                case DataReader.FIELD_LANGUAGE_CODE:
                    objectName = "CODE";
                    break;

                case DataReader.FIELD_LANGUAGE_LONG_CODE:
                    objectName = "LONGCODE";
                    break;

                case DataReader.FIELD_LANGUAGE_DEFAULT_LANGUAGE:
                    objectName = "DEFAULTLANGUAGE";
                    break;



                default:
                    objectName = null;
                    break;
            }

            return objectName;
        }
    }
}

