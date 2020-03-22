using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsoleApp.Models
{
    public class MetaWeather
    {
        [JsonPropertyName("timezone_name")]
        public string TimezoneName { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("consolidated_weather")]
        public IEnumerable<ConsolidatedWeather> ConsolidatedWeathers { get; set; }

        [JsonPropertyName("parent")]
        public WeatherParent WeatherParent { get; set; }
    }
}
