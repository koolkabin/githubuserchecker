using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MainChecker.Models
{
    class JSONAPIHelper<T>
    {
        public static async Task<T> ReadUrlAsync(string url)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
