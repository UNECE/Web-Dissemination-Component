using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Factory
{
    public class AddOnParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public AddOnParameter (string keyName, string keyValue)
        {
            this.Name = keyName;
            this.Value = keyValue;
        }
    }
}

