using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class BranchesModel
    {
        private string curLang = "";

        public BranchesModel(string lang)
        {
            curLang = lang;
        }

        public System.Collections.Generic.IEnumerable<BranchModel> getBranches()
        {
            DataReader reader = App.getDataReader();
            reader.Open(curLang, DataReader.MODEL_BRANCHES);

            BranchModel thisBranch = null;
            while (reader.Read())
            {
                string id = reader.getString(DataReader.FIELD_BRANCHES_ID);
                string sortCode = reader.getString(DataReader.FIELD_BRANCHES_SORTCODE);
                string title = reader.getString(DataReader.FIELD_BRANCHES_TITLE);

                if (!title.Equals(""))
                {
                    thisBranch = new BranchModel(curLang, sortCode, id, title);
                    yield return thisBranch;
                }
            }

            reader.Close();

        }

    }
}
