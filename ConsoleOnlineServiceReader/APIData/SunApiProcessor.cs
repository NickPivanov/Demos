using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleOnlineServiceReader.APIData
{
    public class SunApiProcessor
    {
        public static async Task<Sun> GetDataForKaliningradAsync()
        {
            string url = "https://api.sunrise-sunset.org/json?lat=54.719350&lng=20.502985&date=today";

            using (HttpResponseMessage response = await ClientInitializer.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var sunApi = await response.Content.ReadAsAsync<SunApiModel>();
                    return sunApi.Results;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
