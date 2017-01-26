using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.FlatFiles
{
    public class JsonUpdated
    {
        private string id = "";
        private string date = "";
        private JsonLanguage[] languages = new JsonLanguage[10];

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public JsonLanguage[] Languages
        {
            get { return languages; }
            set { languages = value; }
        }

        public static JsonUpdated Populate(string strFileName)
        {
            string text = System.IO.File.ReadAllText(strFileName);
            JsonUpdated m = JsonConvert.DeserializeObject<JsonUpdated>(text);

            return m;
        }
    }
}