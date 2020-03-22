using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Helpers
{
    public static class WeatherHelper
    {
        public static void PrintInfo(MetaWeather metaWeather)
        {
            Console.WriteLine($"LocationType: {metaWeather.LocationType}");
            Console.WriteLine($"TimeZone: {metaWeather.TimeZone}");

            Console.WriteLine("------------------------------");
            Console.WriteLine("Consolidated Weather:");
            foreach (var item in metaWeather.ConsolidatedWeathers)
            {
                Console.WriteLine($"Id: {item.Id}");
                Console.WriteLine($"StateName: {item.StateName}");
                Console.WriteLine($"WindDirectionCompass: {item.WindDirectionCompass}");
                Console.WriteLine("**********************************");
            }
            Console.WriteLine("------------------------------");

            Console.WriteLine($"Parent WoeId: {metaWeather.WeatherParent.WoeId}");
            Console.WriteLine($"Parent Title: {metaWeather.WeatherParent.Title}");
            Console.WriteLine($"Parent LattLong: {metaWeather.WeatherParent.LattLong}");
        }
    }
}
