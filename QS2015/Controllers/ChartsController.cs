using QS2015.Connectivity;
using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace QS2015.Controllers
{
    public class ChartsController : Controller
    {
        private CountryRankingModel countryRanking = null;

        public ActionResult Index(string indicatorCode, string countryCode, string lang)
        {
            // add record to log table
            string thisLanguage = Session["Culture"].ToString();

            // make sure SQL injection attack is impossible
            if (App.isPhrase(indicatorCode))
            {
                if (App.isPhrase(countryCode))
                {
                    App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_CHART, indicatorCode + "," + countryCode, "indicator_code,country_code=" + indicatorCode + "," + countryCode);
                }                
            }

            countryRanking = new CountryRankingModel(thisLanguage, indicatorCode, CountryRankingModel.SORT_BY_COUNTRY_NAME);

            ViewBag.CountryCode = countryCode;

            return View(countryRanking);
        }

        public JsonResult GetChartByCodes(string indicatorCode, string countriesList)
        {
            // remove chk, checkbox prefix and make an array
            string[] countries = countriesList.Replace("chk", "").Split(',');

            object chartdata = GetJsonData(indicatorCode, countries);
            return Json(chartdata, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNextChart(string indicatorCode)
        {
            string thisLanguage = Session["Culture"].ToString();
            string nextIndicatorCode = IndicatorModel.getNextIndicatorCode(thisLanguage, indicatorCode);
            return GetChartByIndicator(nextIndicatorCode);
        }


        public JsonResult GetChartByIndicator(string indicatorCode)
        {
            string thisLanguage = Session["Culture"].ToString();

            if (!indicatorCode.Equals(""))
            {
                countryRanking = new CountryRankingModel(thisLanguage, indicatorCode, CountryRankingModel.SORT_BY_COUNTRY_NAME);

                string countryCode = countryRanking.Ranking.ElementAt(0).Code;
                string[] countries = new string[1] { countryCode };

                // remove chk, checkbox prefix and make an array
                object chartdata = GetJsonData(indicatorCode, countries);
                return Json(chartdata, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        public object GetJsonData(string indicatorCode, string[] countries)
        {
            // add record to log table
            string thisLanguage = Session["Culture"].ToString();

            if (App.isPhrase(indicatorCode))
            {
                App.addQuickStatsLogRecord(thisLanguage, App.QUICK_STATS_TIME_SERIES, indicatorCode, "indicator_code=" + indicatorCode);
            }            

            // ************************************************************************************************************
            // get chart title and measurement unit
            IndicatorModel thisIndicator = IndicatorModel.getIndicator(thisLanguage, indicatorCode);
            string measurementUnit = thisIndicator.MeasurementUnit;
            string indicatorTitle = thisIndicator.Name;


            // ************************************************************************************************************
            // get years
            List<string> thesePeriods = new List<string>();

            foreach (string countryCode in countries)
            {
                TimeSeriesModel thisDataCollection = new TimeSeriesModel(thisLanguage, countryCode, indicatorCode);

                foreach (string periodCode in thisDataCollection.PeriodCodes)
                {
                    if (!thesePeriods.Contains(periodCode))
                    {
                        // add new period to the list
                        thesePeriods.Add(periodCode);
                    }
                }              
            }

            // sort periods
            thesePeriods.Sort();

            // add periods to the categories list
            List<object> thisCategory = new List<object>();
            
            foreach (string periodCode in thesePeriods)
            {
                thisCategory.Add(new { label = periodCode });
            }

            List<object> theseCategories = new List<object>();
            theseCategories.Add(new { category = thisCategory });


            // ************************************************************************************************************
            // get time series
            List<object> thisDataset = new List<object>();

            foreach (string countryCode in countries) {
                TimeSeriesModel thisDataCollection = new TimeSeriesModel(thisLanguage, countryCode, indicatorCode);
                string countryName = thisDataCollection.Country.Name;

                // add time series values
                List<object> timeSeries = new List<object>();

                List<TimeSeriesValueModel> theseTimeSeriesValues = thisDataCollection.TimeSeries;

                // go through all periods, if found in the time series, add to the category, otherwise put empty string
                foreach (string periodCode in thesePeriods) {

                    TimeSeriesValueModel thisTimeSeriesValue = theseTimeSeriesValues.Find
                            (
                                x => x.PeriodCode.Equals(periodCode)
                            );

                    if (thisTimeSeriesValue == null)
                    {
                        // value is not found, add empty string
                        timeSeries.Add(new { value = "" });
                    }
                    else
                    {
                        // value is found
                        string thisStrValue = thisTimeSeriesValue.Value;
                        timeSeries.Add(new { value = thisStrValue });
                    }
                }

                // add this time series to the dataset
                thisDataset.Add(
                    new
                    {
                        seriesname = countryName,
                        data = timeSeries
                    }
                );
            }


            // ************************************************************************************************************
            // get countries list from the country ranking
            CountryRankingModel thisCountryRanking = new CountryRankingModel(thisLanguage, indicatorCode, CountryRankingModel.SORT_BY_COUNTRY_NAME);

            string[] mycountries = new string[thisCountryRanking.Ranking.Count];

            int i = 0;
            foreach (CountryDataModel thisCountryDataModel in thisCountryRanking.Ranking)
            {
                string countryDataCode = thisCountryDataModel.Code;
                mycountries[i] = countryDataCode;

                i++;
            }


            // get time axis name
            ResourceManager rm = Resources.Resources.ResourceManager;
            string dateString = rm.GetString("Charts_Year", CultureInfo.CurrentCulture);

            // ************************************************************************************************************
            // populate json object
            object thisChart = 
                    new
                    {
                        indicator = indicatorCode,
                        countries = mycountries,
                        fusioncharts =
                            new
                            {
                                type = "msline",
                                renderAt = "chartContainer",
                                width = "100%",
                                height = "250",
                                id = "mychartid",
                                dataFormat = "json",
                                dataSource = new
                                {
                                    chart = new
                                    {
                                        caption = indicatorTitle,
                                        xAxisname = dateString,
                                        yAxisName = measurementUnit,
                                        theme = "fint"
                                    },
                                    categories = theseCategories,
                                    dataset = thisDataset
                                }
                            }
                    };

            return thisChart;

        }

    }

}
