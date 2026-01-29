using NexusContacts.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace NexusContacts
{
    /// <summary>
    /// Represents a single city record.
    /// </summary>
    public class CityRecord
    {
        /// <summary>
        /// Gets or sets the city code (city_code).
        /// </summary>
        [JsonPropertyName("city_code")]
        public int CityCode { get; set; }

        /// <summary>
        /// Gets or sets the Hebrew name of the city (city_name_he).
        /// </summary>
        [JsonPropertyName("city_name_he")]
        public string NameHebrew { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the English name of the city (city_name_en).
        /// </summary>
        [JsonPropertyName("city_name_en")]
        public string NameEnglish { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds a list of city records as returned from the government API.
    /// </summary>
    public class GovApiResult
    {
        [JsonPropertyName("records")]
        public List<CityRecord> Records { get; set; }
    }

    /// <summary>
    /// Root object for the government API response.
    /// </summary>
    public class GovApiResponse
    {
        [JsonPropertyName("result")]
        public GovApiResult Result { get; set; }
    }

    /// <summary>
    /// Provides methods to interact with the database for city records.
    /// </summary>
    public class CityDbMethods
    {
        private readonly BaseDal dal;

        /// <summary>
        /// Initializes a new instance of CityDbMethods. Allows dependency injection of BaseDal.
        /// </summary>
        /// <param name="dal">Optional BaseDal instance. If not provided, a new one is created.</param>
        public CityDbMethods(BaseDal dal = null)
        {
            this.dal = dal ?? new BaseDal();
        }

        /// <summary>
        /// Converts a DataTable to a list of CityRecord objects.
        /// </summary>
        /// <param name="dt">The DataTable containing city data.</param>
        /// <returns>List of CityRecord objects.</returns>
        public List<CityRecord> GetCitiesFromDataTable(DataTable dt)
        {
            var cities = new List<CityRecord>();
            if (dt == null) return cities;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    var city = new CityRecord
                    {
                        CityCode = Convert.ToInt32(row["city_code"]),
                        NameHebrew = row["city_name_he"].ToString(),
                        NameEnglish = row["city_name_en"] != DBNull.Value ? row["city_name_en"].ToString() : string.Empty
                    };
                    cities.Add(city);
                }
                return cities;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error converting cities: " + ex.Message);
                return cities;
            }
        }
    }
}