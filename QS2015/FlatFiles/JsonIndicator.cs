using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.FlatFiles
{
    public class JsonIndicator
    {
        public string Code { get; set; }
        public string Domain_Id { get; set; }
        public string Name { get; set; }
        public string GradeValues { get; set; }
        public string GradeColors { get; set; }
        public string Measure { get; set; }
        public string Note { get; set; }
        public string SourceWebLink { get; set; }
        public string ColorScale { get; set; }

        public static List<JsonIndicator> Populate(string strFileName)
        {
            string text = System.IO.File.ReadAllText(strFileName);
            List<JsonIndicator> m = JsonConvert.DeserializeObject<List<JsonIndicator>>(text);

            return m;
        }
    }
}

