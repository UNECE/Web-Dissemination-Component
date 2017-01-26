using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class LanguageModel
    {
        private string code = "";
        private string longCode = "";
        private Boolean defaultLanguage = false;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string LongCode
        {
            get { return longCode; }
            set { longCode = value; }
        }

        public Boolean DefaultLanguage
        {
            get { return defaultLanguage; }
            set { defaultLanguage = value; }
        }

    }
}