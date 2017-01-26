using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class DomainDataModel
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public List<IndicatorDataModel> Indicators { get; set; }

        public DomainDataModel()
        {
            this.Code = "";
            this.Name = "";
            this.Indicators = new List<IndicatorDataModel>();
        }

    }
}