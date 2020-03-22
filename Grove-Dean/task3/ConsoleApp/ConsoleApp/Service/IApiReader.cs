using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public interface IApiReader
    {
        Task<T> ReadFromUrlAsync<T>(string url) where T : class;

        Task<string> ReadAsString(string url);
    }
}
