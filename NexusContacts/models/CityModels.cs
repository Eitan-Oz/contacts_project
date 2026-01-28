using NexusContacts.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.Json.Serialization; // ספרייה חובה לצורך התרגום מה-JSON

namespace NexusContacts
{
    // 1. המחלקה שמייצגת עיר בודדת
    public class CityRecord
    {
        // ה-Attribute הזה [JsonPropertyName] הוא המתרגם:
        // הוא אומר: "קח את הערך של 'semel_yishuv' מהטקסט, ושים אותו בתוך 'CityCode'"
        [JsonPropertyName("semel_yishuv")]
        public string CityCode { get; set; }

        [JsonPropertyName("shem_yishuv")]
        public string NameHebrew { get; set; }

        [JsonPropertyName("english_name")]
        public string NameEnglish { get; set; }
    }

    // 2. מחלקת עזר: מחזיקה את רשימת הרשומות (records)
    public class GovApiResult
    {
        [JsonPropertyName("records")]
        public List<CityRecord> Records { get; set; }
    }

    // 3. מחלקת השורש: ככה נראית התשובה הראשית מהממשלה
    public class GovApiResponse
    {
        [JsonPropertyName("result")]
        public GovApiResult Result { get; set; }
    }

   public class CityConnectToDBMathods
    {
        BaseDal dal = new BaseDal();

        public List<CityRecord> GetCityFromDataTable(DataTable dt)
        {
            List<CityRecord> cities = new List<CityRecord>();

            if (dt == null) return cities; // בדיקת בטיחות

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    CityRecord city = new CityRecord();

                    // אנחנו לוקחים את השמות של העמודות ב-SQL (שיצרנו קודם)
                    // ומכניסים אותם למאפיינים של המחלקה

                    // המרה בטוחה לסטרינג (כי ב-SQL זה יכול להיות INT ובמחלקה זה string)
                    city.CityCode = row["city_code"].ToString();

                    city.NameHebrew = row["city_name_he"].ToString();

                    // בדיקה אם קיים ערך באנגלית (לפעמים זה NULL)
                    if (row["city_name_en"] != DBNull.Value)
                    {
                        city.NameEnglish = row["city_name_en"].ToString();
                    }
                    else
                    {
                        city.NameEnglish = "";
                    }

                    // הוספה לרשימה
                    cities.Add(city);
                }

                return cities;
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה, אפשר להדפיס אותה או להחזיר רשימה ריקה
                Debug.WriteLine("Error converting cities: " + ex.Message);
                return cities;
            }
        }

    }

}