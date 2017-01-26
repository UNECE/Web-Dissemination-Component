using QS2015.Connectivity;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QS2015.Controllers
{
    public class DataMapController : Controller
    {
        //
        // GET: /DataMap/
        private CountryRankingModel countryRanking = null;

        public ActionResult Index(string indicatorCode)
        {
            // add record to log table
            string thisLanguage = Session["Culture"].ToString();

            if (App.isPhrase(indicatorCode))
            {
                App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_MAP, indicatorCode, "indicator_code=" + indicatorCode);
            }            
            
            countryRanking = new CountryRankingModel(thisLanguage, indicatorCode, CountryRankingModel.SORT_BY_VALUE);

            return View(countryRanking);
        }


        public JsonResult GetRandomMap()
        {
            // add record to log table
            string thisLanguage = Session["Culture"].ToString();

            string randomIndicatorCode = IndicatorModel.getRandomIndicatorCode(thisLanguage);

            List<object> datamap = GetJsonData(randomIndicatorCode);
            return Json(datamap, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNextMap(string indCode)
        {
            string thisLanguage = Session["Culture"].ToString();
            string thisIndicatorCode = IndicatorModel.getNextIndicatorCode(thisLanguage, indCode);

            if (thisIndicatorCode.Equals(""))
            {
                // the end of the indicators list is reached
                return null;
            }
            else
            {
                // get json data for the next indicator
                List<object> datamap = GetJsonData(thisIndicatorCode);
                return Json(datamap, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMapByIndicatorCode(string indicatorCode)
        {
            List<object> datamap = GetJsonData(indicatorCode);
            return Json(datamap, JsonRequestBehavior.AllowGet);
        }


        public List<object> GetJsonData(string indicatorCode)
        {
            // add record to log table
            string thisLanguage = Session["Culture"].ToString();

            if (App.isPhrase(indicatorCode))
            {
                App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_MAP, indicatorCode, "indicator_code=" + indicatorCode);
            }

            countryRanking = new CountryRankingModel(thisLanguage, indicatorCode, CountryRankingModel.SORT_BY_VALUE);

            IndicatorModel thisIndicator = countryRanking.Indicator;

            // get data set params
            string title = thisIndicator.Name;

            string footnote = thisIndicator.Footnote;
            string measurementUnit = thisIndicator.MeasurementUnit;
            string gradeValues = thisIndicator.Code4GradeValues;
            string colorValues = thisIndicator.Code4ColorValuesA.Replace("'", "");

            // get comma separated lists of countries and values
            List<CountryDataModel> ranking = countryRanking.Ranking;
            string period = countryRanking.PeriodCode;
            

            string countriesList = "";
            string valuesList = "";

            foreach (CountryDataModel countryData in ranking)
            {
                string countryCode = countryData.Code;
                string countryValue = countryData.Value;

                if (countriesList.Equals(""))
                {
                    countriesList = countryCode;
                    valuesList = countryValue;
                }
                else
                {
                    countriesList = countriesList  + "," + countryCode;
                    valuesList = valuesList + "," + countryValue;
                }
            }

            
            // create json object
            List<object> datamap = new List<object>();

            datamap.Add
                (
                    new 
                    {
                        IndicatorCode = indicatorCode,
                        Title = title,
                        MeasurementUnit = measurementUnit,
                        Period = period,
                        GradeValues = gradeValues,
                        ColorValues = colorValues,
                        CountriesList = countriesList,
                        ValuesList = valuesList,
                        Footnote = footnote
                    }
                );

            return datamap;

        }

    }
}


public class CountryValue {
    public string Name { get; set; }
    public string Value { get; set; }
}