using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsoleApp.Models
{
    public class WeatherParent
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("woeid")]
        public int WoeId { get; set; }

        [JsonPropertyName("latt_long")]
        public string LattLong { get; set; }
    }
}
