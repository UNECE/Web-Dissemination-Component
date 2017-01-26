using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.FlatFiles
{
    public class JsonTerritory
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public static List<JsonTerritory> Populate(string strFileName)
        {
            string text = System.IO.File.ReadAllText(strFileName);
            List<JsonTerritory> m = JsonConvert.DeserializeObject<List<JsonTerritory>>(text);

            return m;
        }

    }
}
