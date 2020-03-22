using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsoleApp.Models
{
    public class ConsolidatedWeather
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("weather_state_name")]
        public string StateName { get; set; }

        [JsonPropertyName("weather_state_abbr")]
        public string StateAbbr { get; set; }

        [JsonPropertyName("wind_direction_compass")]
        public string WindDirectionCompass { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("applicable_date")]
        public DateTime ApplicableDate { get; set; }
    }
}
