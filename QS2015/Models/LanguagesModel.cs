using QS2015.Connectivity;
using QS2015.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class LanguagesModel
    {
        public static System.Collections.Generic.IEnumerable<LanguageModel> getLanguages()
        {
            DataReader reader = App.getDataReader();
            reader.Open(null, DataReader.MODEL_LANGUAGES);

            LanguageModel thisLanguage = null;
            while (reader.Read())
            {
                string strLanguageCode = reader.getString(DataReader.FIELD_LANGUAGE_CODE);
                string strLanguageLongCode = reader.getString(DataReader.FIELD_LANGUAGE_LONG_CODE);
                string strDefaultLanguage = reader.getString(DataReader.FIELD_LANGUAGE_DEFAULT_LANGUAGE);

                thisLanguage = new LanguageModel();

                thisLanguage.Code = strLanguageCode;
                thisLanguage.LongCode = strLanguageLongCode;

                if (strDefaultLanguage.ToUpper().Equals("Y"))
                {
                    thisLanguage.DefaultLanguage = true;
                }
                else
                {
                    thisLanguage.DefaultLanguage = false;
                }

                yield return thisLanguage;
            }

            reader.Close();

        }
    }
}


