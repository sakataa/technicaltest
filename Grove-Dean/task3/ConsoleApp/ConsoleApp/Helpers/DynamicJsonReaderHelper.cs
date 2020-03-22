using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ConsoleApp.Helpers
{
    public static class DynamicJsonReaderHelper
    {
        public static void ReadFromString(string jsonString)
        {
            using (var document = JsonDocument.Parse(jsonString))
            {
                var root = document.RootElement;
                ReadJsonElement(root);
            }
        }

        private static void ReadJsonElement(JsonElement jElement)
        {
            foreach (var item in jElement.EnumerateObject())
            {
                if (item.Value.ValueKind == JsonValueKind.Array)
                {
                    var index = 0;
                    Console.WriteLine($"Array of Property Name: {item.Name}");
                    foreach (var element in item.Value.EnumerateArray())
                    {
                        Console.WriteLine($"Index: {index++}");
                        ReadJsonElement(element);
                        Console.WriteLine("***************************");
                    }
                }
                else if (item.Value.ValueKind == JsonValueKind.Object)
                {
                    Console.WriteLine($"Object Key of Property Name: {item.Name}");
                    ReadJsonElement(item.Value);
                    Console.WriteLine("===================================");
                }
                else
                {
                    Console.WriteLine($"Name: {item.Name} - Value: {item.Value}");
                    Console.WriteLine("-------------------------");
                }
            }
        }
    }
}
