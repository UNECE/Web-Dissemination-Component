
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.FlatFiles
{
    public class JsonWebsiteMessage
    {
        public string ExpireDate { get; set; }
        public string WebLink { get; set; }
        public string MessageText { get; set; }

        public static JsonWebsiteMessage Populate(string strFileName)
        {
            string text = System.IO.File.ReadAllText(strFileName);
            JsonWebsiteMessage m = JsonConvert.DeserializeObject<JsonWebsiteMessage>(text);

            return m;
        }
    }
}

