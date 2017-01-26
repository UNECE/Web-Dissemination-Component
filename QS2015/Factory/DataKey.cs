using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Factory
{
    public class DataKey
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public DataKey (int keyName, string keyValue)
        {
            this.Id = keyName;
            this.Value = keyValue;
        }
    }
}