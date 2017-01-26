using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class IndicatorDataModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string MeasurementUnit { get; set; }
        public string PeriodCode { get; set; }

        public string Value { get; set; }
    }
}

