using System.Collections.Generic;
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
}