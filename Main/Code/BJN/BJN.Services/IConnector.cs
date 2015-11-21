using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Services
{
    public interface IConnector
    {
        Task<HttpResponseMessage> GetAsync(string baseUrl, string endpoint);
        Task<HttpResponseMessage> PostAsync(string baseUrl, string endpoint, string payload);
        Task<HttpResponseMessage> PutAsync(string baseUrl, string endpoint, string payload);
        Task<HttpResponseMessage> DeleteAsync(string baseUrl, string endpoint);
    }
}
