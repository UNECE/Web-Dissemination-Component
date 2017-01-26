using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class PDFCountryProfilesModel
    {
        public List<PDFModel> Profile { get; set; }

        public PDFCountryProfilesModel(string lang)
        {
            Profile = new List<PDFModel>();

            DataReader reader = App.getDataReader();
            reader.Open(lang, DataReader.MODEL_PDFCOUNTRYPROFILES);

            while (reader.Read())
            {
                string countryCode = reader.getString(DataReader.FIELD_PDFCOUNTRYPROFILES_CODE);
                string countryName = reader.getString(DataReader.FIELD_PDFCOUNTRYPROFILES_NAME);
                string countryWeblink = reader.getString(DataReader.FIELD_PDFCOUNTRYPROFILES_WEBLINK);

                PDFModel thisPDFDoc = new PDFModel();
                thisPDFDoc.Code = countryCode;
                thisPDFDoc.Name = countryName;
                thisPDFDoc.Weblink = countryWeblink;

                Profile.Add(thisPDFDoc);
            }
            
            reader.Close();            

        }
    }
}