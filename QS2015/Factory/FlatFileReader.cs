using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QS2015.Connectivity;
using QS2015.FlatFiles;
using System.IO;
using QS2015.Models;
using System.Globalization;

namespace QS2015.Factory
{
    public class FlatFileReader : DataReader
    {
        private int intPointer = -1;
        private List<string> FieldNames = new List<string>();
        private List<List<string>> Records = new List<List<string>>();

        public const string FILE_NAME_UPDATE                                = "a_updated.json";
        public const string FILE_NAME_WEBSITE_MESSAGE                       = "_websitemessage_";
        public const string FILE_NAME_TERRITORY_PROFILES                    = "_territoryprofiles_";
        public const string FILE_NAME_TERRITORIES                           = "_territories_";
        public const string FILE_NAME_DOMAINS                               = "_domains_";
        public const string FILE_NAME_INDICATORS                            = "_indicators_";
        public const string FILE_NAME_DATASETS                              = "_datasets.txt";
        public const string FILE_NAME_DATASETS_INDEX                        = "_dsindex.txt";
        public const string FILE_NAME_TIME_SERIES                           = "_timeseries.txt";
        public const string FILE_NAME_TIME_SERIES_INDEX                     = "_tsindex.txt";

        public JsonUpdated jsonUpdated                                      = null;

        public FlatFileReader()
        {
            jsonUpdated = JsonUpdated.Populate(this.getDataFilesFolderPath() + FILE_NAME_UPDATE);
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


            string strFileNameTerritories = strTerritoriesFilesFolder + "a" + jsonUpdated.Id + FILE_NAME_TERRITORIES + lang + ".js";
            return strFileNameTerritories;
        }

        public override void Open(string lang, int source, List<DataKey> dataKeys, List<AddOnParameter> addOnKeys)
        {
            string strUpdateId = jsonUpdated.Id;

            if (source == DataReader.MODEL_LANGUAGES)
            {
                this.setLanguagesReader(jsonUpdated);
            }
            else if (source == DataReader.MODEL_HOME)
            {
                setHomeModelReader(strUpdateId, lang);
            }
            else if (source == DataReader.MODEL_PDFCOUNTRYPROFILES)
            {
                this.setPdfTerritoryProfilesModelReader(strUpdateId, lang);
            }
            else if (source == DataReader.MODEL_COUNTRIES)
            {
                this.setTerritoriesModelReader(strUpdateId, lang);
            }
            else if (source == DataReader.MODEL_DOMAINS)
            {
                this.setDomainsModelReader(strUpdateId, lang);
            }
            else if (source == DataReader.MODEL_DOMAIN)
            {
                this.setThisDomainModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_DOMAIN_INDICATORS)
            {
                this.setThisDomainIndicatorsModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_COUNTRYRANKING_SORT_BY_VALUE)
            {
                this.setCountryRankingSortedByValueModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_COUNTRYRANKING_SORT_BY_NAME)
            {
                this.setCountryRankingSortedByNameModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_INDICATOR)
            {
                this.setIndicatorModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_RANDOM_INDICATOR)
            {
                this.setRandomIndicatorModelReader(strUpdateId, lang);
            }
            else if (source == DataReader.MODEL_NEXT_INDICATOR)
            {
                this.setNextIndicatorModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_COUNTRY)
            {
                this.setCountryModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_TIMESERIES)
            {
                this.setTimeSeriesModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_COUNTRYPROFILE)
            {
                this.setCountryProfileModelReader(strUpdateId, lang, dataKeys);
            }
            else if (source == DataReader.MODEL_COUNTRYPROFILES)
            {
                this.setManyCountryProfilesModelReader(strUpdateId, lang, dataKeys, addOnKeys);
            }


            else
            {
                throw new Exception("The model is not defined!");
            }

            // set pointer before beginning
            intPointer = -1;
        }
        
        public override void Open(string lang, int source, List<DataKey> datakeys)
        {
            this.Open(lang, source, datakeys, null);
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
            // nothing happens here
        }

        public override Boolean Read()
        {
            this.intPointer++;

            if (this.intPointer >= this.Records.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public override string getString(int fieldId)
        {
            string fieldName = FieldNamesConvertor.getName(fieldId);

            if (fieldName.Equals("")) 
            {
                throw new Exception ("The field name is not found for the field id " + fieldId.ToString() + "!");
            } else {
                int recordId = this.FieldNames.IndexOf(fieldName);

                if (recordId == -1) {
                    throw new Exception ("The field name " + fieldName + " is not found in the fields list!");
                } else {
                    // get record by pointer
                    List<string> thisRecord = this.Records.ElementAt(intPointer);

                    // get value by found column id
                    return thisRecord.ElementAt(recordId);
                }

            }            
        }

        public string getDataFilesFolderPath()
        {
            String rootPath = HttpContext.Current.Server.MapPath("~").Replace("\\", "/");

            string strDataFilesFolder = App.getAppSettingsKeyValue("FlatFiles.DataFolder").Replace("\\", "/");

            string result = "";

            if (strDataFilesFolder.EndsWith("\\") || strDataFilesFolder.EndsWith("/"))
            {
                result = rootPath + strDataFilesFolder;
            }
            else
            {
                result = rootPath + strDataFilesFolder + "/";
            }

            return result;
        }

        public void setHomeModelReader(string strUpdateId, string lang)
        {
            string strFileNameWebsiteMessage = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_WEBSITE_MESSAGE + lang + ".json";

            JsonWebsiteMessage jsonWebsiteMessage = JsonWebsiteMessage.Populate(strFileNameWebsiteMessage);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_HOME_EXPIRED));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_HOME_MESSAGE_TEXT));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_HOME_WEBLINK));

            List<string> thisRecord = new List<string>();

            thisRecord.Add(jsonWebsiteMessage.ExpireDate);
            thisRecord.Add(jsonWebsiteMessage.MessageText);
            thisRecord.Add(jsonWebsiteMessage.WebLink);

            this.Records.Add(thisRecord);
        }

        public void setPdfTerritoryProfilesModelReader(string strUpdateId, string lang)
        {
            string strFileNameTerritoryProfiles = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_TERRITORY_PROFILES + lang + ".json";

            List<JsonPdfTerritoryProfile> listPdfTerritoryProfiles = JsonPdfTerritoryProfile.Populate(strFileNameTerritoryProfiles);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_PDFCOUNTRYPROFILES_CODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_PDFCOUNTRYPROFILES_NAME));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_PDFCOUNTRYPROFILES_WEBLINK));

            foreach (JsonPdfTerritoryProfile p in listPdfTerritoryProfiles)
            {
                List<string> thisRecord = new List<string>();

                thisRecord.Add(p.Code);
                thisRecord.Add(p.Name);
                thisRecord.Add(p.Weblink);

                this.Records.Add(thisRecord);
            }
        }

        


        public void setTerritoriesModelReader(string strUpdateId, string lang)
        {
            string strFileNameTerritories = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_TERRITORIES + lang + ".json";

            List<JsonTerritory> listTerritorîes = JsonTerritory.Populate(strFileNameTerritories);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRIES_CODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRIES_NAME));

            foreach (JsonTerritory p in listTerritorîes)
            {
                List<string> thisRecord = new List<string>();

                string strTerritoryCode = p.Code;
                string strTerritoryName = p.Name;

                thisRecord.Add(strTerritoryCode);
                thisRecord.Add(strTerritoryName);

                // add the record sorted by name
                int pos = -1;
                for (int i = 0; i < this.Records.Count; i++)
                {
                    string strCurrentTerritoryName = this.Records.ElementAt(i).ElementAt(1);

                    if (strCurrentTerritoryName.CompareTo(strTerritoryName) > 0)
                    {
                        pos = i;
                        break;
                    }
                }

                if (pos == -1)
                {
                    // new record is appended at the end of the list because it has the smallest value
                    this.Records.Add(thisRecord);
                }
                else
                {
                    this.Records.Insert(pos, thisRecord);
                } 

            }
            
        }

        public void setDomainsModelReader(string strUpdateId, string lang)
        {
            string strFileNameDomains = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DOMAINS + lang + ".json";

            List<JsonDomain> listDomains = JsonDomain.Populate(strFileNameDomains);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_DOMAINS_CODE));

            foreach (JsonDomain p in listDomains)
            {
                List<string> thisRecord = new List<string>();

                thisRecord.Add(p.Code);
                this.Records.Add(thisRecord);
            }
        }

        public void setThisDomainModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            if (dataKeys.Count > 1)
            {
                throw new Exception("The list contains more than one data keys!");
            }

            DataKey domainKey = dataKeys.ElementAt(0);

            string strFileNameDomains = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DOMAINS + lang + ".json";

            List<JsonDomain> listDomains = JsonDomain.Populate(strFileNameDomains);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_DOMAIN_NAME));

            if (domainKey.Id == DataReader.KEY_DOMAIN_DOMAINCODE)
            {
                string domainCode = domainKey.Value;

                foreach (JsonDomain p in listDomains)
                {
                    if (p.Code.Equals(domainCode))
                    {
                        List<string> thisRecord = new List<string>();

                        thisRecord.Add(p.Name);
                        this.Records.Add(thisRecord);

                        return;
                    }
                }
            }
            else
            {
                throw new Exception("Data key in domain model reader is not correctly defined.");
            }

        }

        public void setThisDomainIndicatorsModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            if (dataKeys.Count > 1)
            {
                throw new Exception("The list contains more than one data keys!");
            }

            DataKey domainKey = dataKeys.ElementAt(0);

            string strFileNameIndicators = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_INDICATORS + lang + ".json";

            List<JsonIndicator> listIndicators = JsonIndicator.Populate(strFileNameIndicators);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_DOMAIN_INDICATORS_CODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_DOMAIN_INDICATORS_NAME));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_DOMAIN_INDICATORS_CODE4GRADEVALUES));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_DOMAIN_INDICATORS_CODE4COLORVALUESA));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_DOMAIN_INDICATORS_MEASUREMENTUNIT));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_DOMAIN_INDICATORS_FOOTNOTE));

            if (domainKey.Id == DataReader.KEY_DOMAIN_DOMAINCODE)
            {
                string domainCode = domainKey.Value;

                foreach (JsonIndicator p in listIndicators)
                {
                    if (p.Domain_Id.Equals(domainCode))
                    {
                        List<string> thisRecord = new List<string>();

                        thisRecord.Add(p.Code);
                        thisRecord.Add(p.Name);
                        thisRecord.Add(p.GradeValues);
                        thisRecord.Add(p.GradeColors);
                        thisRecord.Add(p.Measure);
                        thisRecord.Add(p.Note);
                        
                        this.Records.Add(thisRecord);
                    }
                }
            }
            else
            {
                throw new Exception("Data key in domain indicator model reader is not correctly defined.");
            }
        }

        public void setIndicatorModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            if (dataKeys.Count > 1)
            {
                throw new Exception("The list contains more than one data keys!");
            }

            DataKey indicatorKey = dataKeys.ElementAt(0);
            string indicatorCode = indicatorKey.Value;

            string strFileNameIndicators = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_INDICATORS + lang + ".json";

            List<JsonIndicator> listIndicators = JsonIndicator.Populate(strFileNameIndicators);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_INDICATOR_CODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_INDICATOR_NAME));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_INDICATOR_CODE4GRADEVALUES));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_INDICATOR_CODE4COLORVALUESA));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_INDICATOR_MEASUREMENTUNIT));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_INDICATOR_FOOTNOTE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_INDICATOR_SOURCEWEBLINK));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_INDICATOR_COLOR_SCALE));

            foreach (JsonIndicator p in listIndicators)
            {
                if (p.Code.Equals(indicatorCode))
                {
                    List<string> thisRecord = new List<string>();

                    thisRecord.Add(p.Code);
                    thisRecord.Add(p.Name);
                    thisRecord.Add(p.GradeValues);
                    thisRecord.Add(p.GradeColors);
                    thisRecord.Add(p.Measure);
                    thisRecord.Add(p.Note);
                    thisRecord.Add(p.SourceWebLink);
                    thisRecord.Add(p.ColorScale);

                    this.Records.Add(thisRecord);
                }
            }
        }

        public void setRandomIndicatorModelReader(string strUpdateId, string lang)
        {
            string strFileNameIndicators = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_INDICATORS + lang + ".json";

            List<JsonIndicator> listIndicators = JsonIndicator.Populate(strFileNameIndicators);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_RANDOM_INDICATOR_CODE));

            List<string> thisRecord = new List<string>();

            // get random indicator code
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int intRandomIndicatorCode = rnd.Next(listIndicators.Count);

            JsonIndicator p = listIndicators.ElementAt(intRandomIndicatorCode);

            thisRecord.Add(p.Code);
            this.Records.Add(thisRecord);
        }



        public void setNextIndicatorModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            if (dataKeys.Count > 1)
            {
                throw new Exception("The list contains more than one data keys!");
            }

            DataKey indicatorKey = dataKeys.ElementAt(0);
            string indicatorCode = indicatorKey.Value;

            string strFileNameIndicators = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_INDICATORS + lang + ".json";

            List<JsonIndicator> listIndicators = JsonIndicator.Populate(strFileNameIndicators);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_NEXT_INDICATOR_CODE));

            List<string> thisRecord = new List<string>();

            for (int i = 0; i < listIndicators.Count; i++)
            {
                JsonIndicator thisIndicator = listIndicators.ElementAt(i);

                if (thisIndicator.Code.Equals(indicatorCode))
                {
                    if (i == listIndicators.Count - 1)
                    {
                        // last element
                        thisRecord.Add(listIndicators.ElementAt(0).Code);
                    }
                    else
                    {
                        // not the last element
                        thisRecord.Add(listIndicators.ElementAt(i+1).Code);
                    }

                    this.Records.Add(thisRecord);

                    break;
                }
            }

            
        }

        public void setCountryRankingSortedByValueModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            if (dataKeys.Count > 1)
            {
                throw new Exception("The list contains more than one data keys!");
            }

            DataKey indicatorKey = dataKeys.ElementAt(0);
            string indicatorCode = indicatorKey.Value;

            string strFileNameDatasets = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DATASETS;
            string strFileNameDatasetIndex = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DATASETS_INDEX;
            string strFileNameTerritories = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_TERRITORIES + lang + ".json";

            // initialize field names
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_COUNTRYCODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_COUNTRYNAME));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_PERIODCODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_VALUE));

            // get territories list
            List<JsonTerritory> listTerritorîes = JsonTerritory.Populate(strFileNameTerritories);

            // read dataset index file
                string thisLine = "";
                System.IO.StreamReader file = new System.IO.StreamReader(strFileNameDatasetIndex);
                while ((thisLine = file.ReadLine()) != null)
                {
                    string[] theseFields = thisLine.Split(',');
                    string strIndicatorCode = theseFields[0].Trim();

                    if (indicatorCode.Equals(strIndicatorCode))
                    {
                        string strTerritoryCode = theseFields[1].Trim();

                        // get territory name
                        Boolean found = false;
                        string strTerritoryName = "";
                        foreach (JsonTerritory thisTerritory in listTerritorîes)
                        {
                            if (thisTerritory.Code.Equals(strTerritoryCode))
                            {
                                found = true;
                                strTerritoryName = thisTerritory.Name;
                                break;
                            }
                        }

                        if (found)
                        {
                            // set the record
                            List<string> thisRecord = new List<string>();

                            thisRecord.Add(strTerritoryCode);
                            thisRecord.Add(strTerritoryName);

                            // get values
                            int strPointer = Int32.Parse(theseFields[2].Trim());

                            using (FileStream fs = new FileStream(strFileNameDatasets, FileMode.Open, FileAccess.Read))
                            {
                                fs.Seek(strPointer, SeekOrigin.Begin);
                                using (StreamReader reader = new StreamReader(fs))
                                {
                                    string thisDatasetLine = reader.ReadLine();

                                    string[] dsRecord = thisDatasetLine.Split(',');

                                    if (dsRecord[0].Equals(indicatorCode) && dsRecord[1].Equals(strTerritoryCode))
                                    {
                                        // index file works fine
                                        string strPeriodCode = dsRecord[2].Trim();
                                        string strValue = dsRecord[3].Trim();

                                        if (strValue.EndsWith(";"))
                                        {
                                            strValue = strValue.Substring(0, strValue.Length - 1);
                                        }

                                        thisRecord.Add(strPeriodCode);
                                        thisRecord.Add(strValue);

                                        // add the record sorted by value
                                        //double dblThisValue = Convert.ToDouble(strValue);
                                        double dblThisValue = double.Parse(strValue, CultureInfo.InvariantCulture);

                                        int pos = -1;
                                        for (int i = 0; i < this.Records.Count; i++)
                                        {
                                            //double dblCurrentValue = Convert.ToDouble(this.Records.ElementAt(i).ElementAt(3));
                                            double dblCurrentValue = double.Parse(this.Records.ElementAt(i).ElementAt(3), CultureInfo.InvariantCulture);

                                            if (dblThisValue > dblCurrentValue)
                                            {
                                                pos = i;
                                                break;
                                            }
                                        }

                                        if (pos == -1)
                                        {
                                            // new record is appended at the end of the list because it has the smallest value
                                            this.Records.Add(thisRecord);
                                        }
                                        else
                                        {
                                            this.Records.Insert(pos, thisRecord);
                                        }                                        
                                    }
                                    else
                                    {
                                        throw new Exception("Index file is corrupted for the indicator code " + strIndicatorCode);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //throw new Exception("Territory name is not found for code " + strTerritoryCode + " in the indicator code " + strIndicatorCode);
                            int i = 0;
                            i++;
                        }
                    }
                }

                file.Close();
   
            return;
        }

        public void setCountryRankingSortedByNameModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            if (dataKeys.Count > 1)
            {
                throw new Exception("The list contains more than one data keys!");
            }

            DataKey indicatorKey = dataKeys.ElementAt(0);
            string indicatorCode = indicatorKey.Value;

            string strFileNameDatasets = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DATASETS;
            string strFileNameDatasetIndex = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DATASETS_INDEX;
            string strFileNameTerritories = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_TERRITORIES + lang + ".json";

            // initialize field names
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_COUNTRYCODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_COUNTRYNAME));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_PERIODCODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_VALUE));

            // get territories list
            List<JsonTerritory> listTerritorîes = JsonTerritory.Populate(strFileNameTerritories);

            // read dataset index file
            string thisLine = "";
            System.IO.StreamReader file = new System.IO.StreamReader(strFileNameDatasetIndex);
            while ((thisLine = file.ReadLine()) != null)
            {
                string[] theseFields = thisLine.Split(',');
                string strIndicatorCode = theseFields[0].Trim();

                if (indicatorCode.Equals(strIndicatorCode))
                {
                    string strTerritoryCode = theseFields[1].Trim();

                    // get territory name
                    Boolean found = false;
                    string strTerritoryName = "";
                    foreach (JsonTerritory thisTerritory in listTerritorîes)
                    {
                        if (thisTerritory.Code.Equals(strTerritoryCode))
                        {
                            found = true;
                            strTerritoryName = thisTerritory.Name;
                            break;
                        }
                    }

                    if (found)
                    {
                        // set the record
                        List<string> thisRecord = new List<string>();

                        thisRecord.Add(strTerritoryCode);
                        thisRecord.Add(strTerritoryName);

                        // get values
                        int intPointer = Int32.Parse(theseFields[2].Trim());

                        using (FileStream fs = new FileStream(strFileNameDatasets, FileMode.Open, FileAccess.Read))
                        {
                            fs.Seek(intPointer, SeekOrigin.Begin);
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                string thisDatasetLine = reader.ReadLine();

                                string[] dsRecord = thisDatasetLine.Split(',');

                                if (dsRecord[0].Equals(indicatorCode) && dsRecord[1].Equals(strTerritoryCode))
                                {
                                    // index file works fine
                                    string strPeriodCode = dsRecord[2].Trim();
                                    string strValue = dsRecord[3].Trim();

                                    if (strValue.EndsWith(";"))
                                    {
                                        strValue = strValue.Substring(0, strValue.Length - 1);
                                    }

                                    thisRecord.Add(strPeriodCode);
                                    thisRecord.Add(strValue);

                                    // add the record sorted by name
                                    int pos = -1;
                                    for (int i = 0; i < this.Records.Count; i++)
                                    {
                                        string strCurrentTerritoryName = this.Records.ElementAt(i).ElementAt(1);

                                        if (strCurrentTerritoryName.CompareTo(strTerritoryName) > 0)
                                        {
                                            pos = i;
                                            break;
                                        }
                                    }


                                    if (pos == -1)
                                    {
                                        // new record is appended at the end of the list because it has the smallest value
                                        this.Records.Add(thisRecord);
                                    }
                                    else
                                    {
                                        this.Records.Insert(pos, thisRecord);
                                    } 
                                }
                                else
                                {
                                    throw new Exception("Index file is corrupted for the indicator code " + strIndicatorCode);
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Territory name is not found for code " + strTerritoryCode + " in the indicator code " + strIndicatorCode);
                    }
                }
            }

            file.Close();

            return;
        }

        public void setCountryModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            if (dataKeys.Count > 1)
            {
                throw new Exception("The list contains more than one data keys!");
            }

            DataKey territoryKey = dataKeys.ElementAt(0);
            string territoryCode = territoryKey.Value;

            // read json file
            string strFileNameTerritories = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_TERRITORIES + lang + ".json";

            List<JsonTerritory> listTerritorîes = JsonTerritory.Populate(strFileNameTerritories);

            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRIES_NAME));

            foreach (JsonTerritory p in listTerritorîes)
            {
                if (territoryCode.Equals(p.Code))
                {
                    List<string> thisRecord = new List<string>();
                    thisRecord.Add(p.Name);

                    this.Records.Add(thisRecord);
                    break;
                }
            }
        }


        public void setTimeSeriesModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            string strCountryCode = "";
            string strIndicatorCode = "";

            foreach (DataKey datakey in dataKeys)
            {
                if (datakey.Id == DataReader.KEY_TIMESERIES_COUNTRY)
                {
                    strCountryCode = datakey.Value;
                }
                else if (datakey.Id == DataReader.KEY_TIMESERIES_INDICATOR)
                {
                    strIndicatorCode = datakey.Value;
                } 
            }

            // add column name
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_TIMESERIES_LINE));


            // start reading the time series and index files
            string strFileNameTimeSeries = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_TIME_SERIES;
            string strFileNameTimeSeriesIndex = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_TIME_SERIES_INDEX;

            string thisLine = "";
            System.IO.StreamReader file = new System.IO.StreamReader(strFileNameTimeSeriesIndex);
            while ((thisLine = file.ReadLine()) != null)
            {
                string[] theseFields = thisLine.Split(',');
                string strCurrentIndicatorCode = theseFields[0].Trim();
                string strCurrentTerritoryCode = theseFields[1].Trim();

                if (strIndicatorCode.Equals(strCurrentIndicatorCode) && strCountryCode.Equals(strCurrentTerritoryCode))
                {
                    // get pointer
                    int strPointer = Int32.Parse(theseFields[2].Trim());

                    using (FileStream fs = new FileStream(strFileNameTimeSeries, FileMode.Open, FileAccess.Read))
                    {
                        fs.Seek(strPointer, SeekOrigin.Begin);
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            string thisDatasetLine = reader.ReadLine();

                            string[] timeSeriesRecord = thisDatasetLine.Split(',');

                            if (timeSeriesRecord[0].Equals(strIndicatorCode) && timeSeriesRecord[1].Equals(strCountryCode))
                            {
                                // get rid of the semicolon
                                if (thisDatasetLine.EndsWith(";"))
                                {
                                    thisDatasetLine = thisDatasetLine.Substring(0, thisDatasetLine.Length-1);
                                }

                                // add the first line after making sure that the index file is not corrupt
                                List<string> thisRecord = new List<string>();
                                thisRecord.Add(thisDatasetLine);


                                // **********************************************************************
                                // add the record sorted by alphabet
                                int pos = -1;
                                for (int i = 0; i < this.Records.Count; i++)
                                {
                                    string strCurrentLine = this.Records.ElementAt(i).ElementAt(0);

                                    if (strCurrentLine.CompareTo(thisDatasetLine) > 0)
                                    {
                                        pos = i;
                                        break;
                                    }
                                }

                                if (pos == -1)
                                {
                                    // new record is appended at the end of the list because it has the smallest value
                                    this.Records.Add(thisRecord);
                                }
                                else
                                {
                                    this.Records.Insert(pos, thisRecord);
                                }
                                // **********************************************************************


                                // make sure to include all subsequent lines until the indeicator and/or territory code changes
                                string strNextDatasetLine = "";
                                while ((strNextDatasetLine = reader.ReadLine()) != null)
                                {
                                    string[] timeSeriesNewRecord = strNextDatasetLine.Split(',');

                                    if (timeSeriesNewRecord[0].Equals(strIndicatorCode) && timeSeriesNewRecord[1].Equals(strCountryCode))
                                    {
                                        // get rid of the semicolon
                                        if (strNextDatasetLine.EndsWith(";"))
                                        {
                                            strNextDatasetLine = strNextDatasetLine.Substring(0, strNextDatasetLine.Length-1);
                                        }

                                        // **********************************************************************
                                        // add values for the current time series
                                        List<string> thisNewRecord = new List<string>();
                                        thisNewRecord.Add(strNextDatasetLine);


                                        // add the record sorted by alphabet
                                        pos = -1;
                                        for (int i = 0; i < this.Records.Count; i++)
                                        {
                                            string strCurrentLine = this.Records.ElementAt(i).ElementAt(0);

                                            if (strCurrentLine.CompareTo(strNextDatasetLine) > 0)
                                            {
                                                pos = i;
                                                break;
                                            }
                                        }

                                        if (pos == -1)
                                        {
                                            // new record is appended at the end of the list because it has the smallest value
                                            this.Records.Add(thisNewRecord);

                                        }
                                        else
                                        {
                                            this.Records.Insert(pos, thisNewRecord);

                                        }
                                        // **********************************************************************

                                    }
                                    else
                                    {
                                        // quit streaming time series file
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Index file is corrupted for the indicator code " + strIndicatorCode);
                            }
                        }
                    }

                    // quit streaming index file 
                    break;

                }
            }

            file.Close();

        }



        public void setCountryProfileModelReader(string strUpdateId, string lang, List<DataKey> dataKeys)
        {
            string strCountryCode = "";
            string strIndicatorCode = "";

            foreach (DataKey datakey in dataKeys)
            {
                if (datakey.Id == DataReader.KEY_COUNTRYPROFILE_COUNTRYCODE)
                {
                    strCountryCode = datakey.Value;
                }
                else if (datakey.Id == DataReader.KEY_COUNTRYPROFILE_INDICATORCODE)
                {
                    strIndicatorCode = datakey.Value;
                }
            }

            Boolean found = false;

            string strFileNameDatasets = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DATASETS;
            string strFileNameDatasetIndex = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DATASETS_INDEX;

            // initialize field names
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_PERIODCODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYRANKING_VALUE));

            // read dataset index file
            int intLinePointer = -1;
            string thisLine = "";
            System.IO.StreamReader file = new System.IO.StreamReader(strFileNameDatasetIndex);
            while ((thisLine = file.ReadLine()) != null)
            {
                string[] theseFields = thisLine.Split(',');
                string strCurrentIndicatorCode = theseFields[0].Trim();
                string strCurrentTerritoryCode = theseFields[1].Trim();

                if (strCurrentIndicatorCode.Equals(strIndicatorCode) && strCurrentTerritoryCode.Equals(strCountryCode))
                {
                    found = true;
                    intLinePointer = Int32.Parse(theseFields[2].Trim());
                }
                // get values
                
            }

            file.Close();


            if (found)
            {
                // index pointer value is found
                using (FileStream fs = new FileStream(strFileNameDatasets, FileMode.Open, FileAccess.Read))
                {
                    fs.Seek(intLinePointer, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string thisDatasetLine = reader.ReadLine();

                        string[] dsRecord = thisDatasetLine.Split(',');

                        if (dsRecord[0].Equals(strIndicatorCode) && dsRecord[1].Equals(strCountryCode))
                        {
                            // index file works fine
                            string strPeriodCode = dsRecord[2].Trim();
                            string strValue = dsRecord[3].Trim();

                            if (strValue.EndsWith(";"))
                            {
                                strValue = strValue.Substring(0, strValue.Length - 1);
                            }

                            List<string> thisRecord = new List<string>();
                            thisRecord.Add(strPeriodCode);
                            thisRecord.Add(strValue);

                            this.Records.Add(thisRecord);
                        }
                        else
                        {
                            throw new Exception("Index file is corrupted for the indicator code " + strIndicatorCode);
                        }
                    }
                }
            }   
        }





        public void setManyCountryProfilesModelReader(string strUpdateId, string lang, List<DataKey> dataKeys, List<AddOnParameter> addOnKeys)
        {
            // get file names
            string strFileNameDatasets = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DATASETS;
            string strFileNameDatasetIndex = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_DATASETS_INDEX;

            // populate territories list
            List<JsonTerritory> sortedTerritoryList = new List<JsonTerritory>();

            // get list of territories
            string strFileNameTerritories = this.getDataFilesFolderPath() + "a" + strUpdateId + FILE_NAME_TERRITORIES + lang + ".json";
            List<JsonTerritory> listTerritorîes = JsonTerritory.Populate(strFileNameTerritories);

            // sort territories by name in a descending order
            foreach (AddOnParameter thisParam in addOnKeys)
            {
                if (thisParam.Name.StartsWith("@p"))
                {
                    string strTerritoryCode = thisParam.Value;

                    foreach (JsonTerritory thisTerritory in listTerritorîes)
                    {
                        if (thisTerritory.Code.Equals(strTerritoryCode))
                        {
                            int pos = -1;
                            for (int i = 0; i < sortedTerritoryList.Count; i++)
                            {
                                string strCurrentTerritoryName = sortedTerritoryList.ElementAt(i).Name;

                                //if (strCurrentTerritoryName.CompareTo(thisTerritory.Name) > 0)
                                if (thisTerritory.Name.CompareTo(strCurrentTerritoryName) > 0)
                                {
                                    pos = i;
                                    break;
                                }
                            }

                            if (pos == -1)
                            {
                                sortedTerritoryList.Add(thisTerritory);

                            }
                            else
                            {
                                sortedTerritoryList.Insert(pos, thisTerritory);
                            }
                        }
                    }
                }
            }


            // get search list of sorted territories
            List<string> searchTerritoryList = new List<string>();
            for (int i = 0; i < sortedTerritoryList.Count; i++) {
                searchTerritoryList.Add(sortedTerritoryList.ElementAt(i).Code);
            }

            // get indicator codes for the territory that was chosen first
            List<string> searchFirstTerritoryIndicatorCodesList = new List<string>();

            if (dataKeys.Count > 1)
            {
                throw new Exception("The list contains more than one data keys!");
            }

            DataKey territoryKey = dataKeys.ElementAt(0);
            string firstTerritoryCode = territoryKey.Value;

            string thisLine = "";

            System.IO.StreamReader file = new System.IO.StreamReader(strFileNameDatasetIndex);
            while ((thisLine = file.ReadLine()) != null)
            {
                string[] theseFields = thisLine.Split(',');
                string strCurrentIndicatorCode = theseFields[0].Trim();
                string strCurrentTerritoryCode = theseFields[1].Trim();

                if (strCurrentTerritoryCode.Equals(firstTerritoryCode))
                {
                    searchFirstTerritoryIndicatorCodesList.Add(strCurrentIndicatorCode);
                }
            }

            file.Close();

            // build the empty pointers field
            List<List<int>> dataPointers = new List<List<int>>();
            for (int i = 0; i < searchFirstTerritoryIndicatorCodesList.Count; i++)
            {
                List<int> thisPonterLine = new List<int>();
                for (int j = 0; j < searchTerritoryList.Count; j++)
                {
                    thisPonterLine.Add(-1);
                }

                dataPointers.Add(thisPonterLine);
            }

            // loop through the index file to get all pointers for territories to be added
            System.IO.StreamReader pointersFile = new System.IO.StreamReader(strFileNameDatasetIndex);
            while ((thisLine = pointersFile.ReadLine()) != null)
            {
                string[] theseFields = thisLine.Split(',');                
                string strCurrentIndicatorCode = theseFields[0].Trim();
                string strCurrentTerritoryCode = theseFields[1].Trim();
                int intPointer = Int32.Parse(theseFields[2].Trim());


                int intIndicatorPosition = searchFirstTerritoryIndicatorCodesList.IndexOf(strCurrentIndicatorCode);
                int intTerritoryPosition = searchTerritoryList.IndexOf(strCurrentTerritoryCode);

                if (intIndicatorPosition >= 0 && intTerritoryPosition >= 0)
                {
                    // pointer is found
                    List<int> theseTerritories = dataPointers.ElementAt(intIndicatorPosition);
                    theseTerritories[intTerritoryPosition] = intPointer;
                }
            }

            pointersFile.Close();

            // ******************************************************************************************************
            // add values to the recordset using valid pointers
            // ******************************************************************************************************
            
            // initialize field names
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYPROFILES_INDICATORCODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYPROFILES_COUNTRYCODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYPROFILES_NAME));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_COUNTRYPROFILES_VALUE));

            // scroll through pointer's list
            for (int i = 0; i < dataPointers.Count; i++)
            {
                List<int> theseTerritories = dataPointers.ElementAt(i);

                for (int j = 0; j < theseTerritories.Count; j++)
                {
                    int intPointer = theseTerritories.ElementAt(j);

                    if (intPointer >= 0)
                    {
                        // this is the valid pointer
                        // find the value using the pointer
                        using (FileStream fs = new FileStream(strFileNameDatasets, FileMode.Open, FileAccess.Read))
                        {
                            fs.Seek(intPointer, SeekOrigin.Begin);
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                string thisDatasetLine = reader.ReadLine();
                                string[] dsRecord = thisDatasetLine.Split(',');

                                string strResultIndicatorCode = searchFirstTerritoryIndicatorCodesList.ElementAt(i); // pointer and territory lists match each other
                                string strResultTerritoryCode = searchTerritoryList.ElementAt(j);
                                string strResultTerritoryName = sortedTerritoryList.ElementAt(j).Name; // json sorted list and territory lists match each other
                                string strValue = dsRecord[3];

                                // strip value of the semicolon
                                if (strValue.EndsWith(";"))
                                {
                                    strValue = strValue.Substring(0, strValue.Length - 1);
                                }

                                // add data to the recordset
                                List<string> thisRecord = new List<string>();
                                thisRecord.Add(strResultIndicatorCode);
                                thisRecord.Add(strResultTerritoryCode);

                                thisRecord.Add(strResultTerritoryName);
                                thisRecord.Add(strValue);

                                this.Records.Add(thisRecord);

                            }
                        }
                    }
                }                
            }
        }








        public void setLanguagesReader(JsonUpdated jsonUpdated)
        {
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_LANGUAGE_CODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_LANGUAGE_LONG_CODE));
            this.FieldNames.Add(FieldNamesConvertor.getName(DataReader.FIELD_LANGUAGE_DEFAULT_LANGUAGE));

            foreach (JsonLanguage l in jsonUpdated.Languages)
            {
                List<string> thisRecord = new List<string>();

                string strLanguageCode = l.Code;
                string strLanguageLongCode = l.LongCode;
                string strDefaultLanguage = l.DefaultLanguage;

                thisRecord.Add(strLanguageCode);
                thisRecord.Add(strLanguageLongCode);
                thisRecord.Add(strDefaultLanguage);

                // add the record sorted by name
                this.Records.Add(thisRecord);
            }

        }
    }
}

