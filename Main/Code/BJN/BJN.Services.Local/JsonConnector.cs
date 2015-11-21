using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Services.Local
{
    public class JsonConnector : IConnector
    {
        public async Task<HttpResponseMessage> GetAsync(string baseUrl, string endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                throw new HttpRequestException("Status " + response.StatusCode);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string baseUrl, string endpoint, string payload)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await
                    client.PostAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                throw new HttpRequestException("Status " + response.StatusCode);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(string baseUrl, string endpoint, string payload)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await
                    client.PutAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                throw new HttpRequestException("Status " + response.StatusCode);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string baseUrl, string endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                throw new HttpRequestException("Status " + response.StatusCode);
            }
        }
    }
}
