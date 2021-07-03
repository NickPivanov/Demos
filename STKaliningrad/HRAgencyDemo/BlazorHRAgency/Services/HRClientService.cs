using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorHRAgency.Services
{
    public class HRClientService
    {
        private IHttpClientFactory _httpClientFactory;
        private HttpClient httpClient;
        private hrClient client;
        public hrClient HrClient { get; set; }
        public HRClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            httpClient = _httpClientFactory.CreateClient("hrAPI");
            client = new hrClient("", httpClient);
            HrClient = client;
        }
    }
}
