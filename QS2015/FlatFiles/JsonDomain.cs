using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.FlatFiles
{
    public class JsonDomain
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public static List<JsonDomain> Populate(string strFileName)
        {
            string text = System.IO.File.ReadAllText(strFileName);
            List<JsonDomain> m = JsonConvert.DeserializeObject<List<JsonDomain>>(text);

            return m;
        }
    }
}

