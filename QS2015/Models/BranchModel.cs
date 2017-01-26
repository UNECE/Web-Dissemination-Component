using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class BranchModel
    {
        private string curLang = "";

        public string SortCode { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }

        public List<CubeGroupModel> CubeGroups { get; set; }

        public BranchModel(string lang, string sortCode, string id, string title)
        {
            // protection code against SQL injection
            // ****************************************************************
            if (!App.isPhrase(id))
            {
                return;
            }
            // ****************************************************************

            curLang = lang;
            SortCode = sortCode;
            Id = id;
            Title = title;

            CubeGroups = new List<CubeGroupModel>();

            DataReader reader = App.getDataReader();
            DataKey thisKey = new DataKey(DataReader.KEY_BRANCHE_ID, id);
            reader.Open(curLang, DataReader.MODEL_BRANCHE, thisKey);

            while (reader.Read())
            {
                string cubeGroupSortCode = reader.getString(DataReader.FIELD_BRANCHE_SORTCODE);
                string cubeGroupParentId = reader.getString(DataReader.FIELD_BRANCHE_PARENTID);
                string cubeGroupId = reader.getString(DataReader.FIELD_BRANCHE_ID);
                string cubeGroupTitle = reader.getString(DataReader.FIELD_BRANCHE_TITLE);
                string webLink = reader.getString(DataReader.FIELD_BRANCHE_WEBLINK);

                if (!cubeGroupTitle.Equals(""))
                {
                    CubeGroupModel thisCubeGroup = new CubeGroupModel();

                    thisCubeGroup.SortCode = cubeGroupSortCode;
                    thisCubeGroup.ParentId = cubeGroupParentId;
                    thisCubeGroup.Id = cubeGroupId;
                    thisCubeGroup.Title = cubeGroupTitle;
                    thisCubeGroup.WebLink = webLink;

                    CubeGroups.Add(thisCubeGroup);
                }
            }

            reader.Close();

        }
    }
}

