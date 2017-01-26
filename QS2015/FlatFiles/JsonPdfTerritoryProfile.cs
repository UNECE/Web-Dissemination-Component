using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.FlatFiles
{
    public class JsonPdfTerritoryProfile
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Weblink { get; set; }

        public static List<JsonPdfTerritoryProfile> Populate(string strFileName)
        {
            string text = System.IO.File.ReadAllText(strFileName);
            List<JsonPdfTerritoryProfile> m = JsonConvert.DeserializeObject<List<JsonPdfTerritoryProfile>>(text);

            return m;
        }
    }
}

