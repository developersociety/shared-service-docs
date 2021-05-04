using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using OpenReferralPOV.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services.HttpClientAdapter
{
    public class HttpClientAdapter : IHttpClientAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly string _Scope = string.Empty;
        private readonly string _ApiBaseAddress = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;

        public HttpClientAdapter(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _Scope = configuration["ORApi:Scope"];
            _ApiBaseAddress = configuration["ORApi:BaseUrl"];
        }
        public async Task<string> GetAsync(Uri endpoint)
        {
            await PrepareAuthenticatedClient();

            var response = await _httpClient.GetAsync(endpoint);
            if ((int)response.StatusCode == 400)
            {
                var error = JsonConvert.DeserializeObject<ValidationError>(await response.Content.ReadAsStringAsync());
                throw error;
            }
            if ((int)response.StatusCode > 400)
            {
                throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.", null, response.StatusCode);
            }
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        private async Task<string> PayloadAsync(Uri endpoint, object payload, HttpMethod method)
        {
            await PrepareAuthenticatedClient();

            var message = new HttpRequestMessage();
            message.RequestUri = endpoint;
            message.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            message.Method = method;

            var response = await _httpClient.SendAsync(message);
            if ((int)response.StatusCode == 400)
            {
                var error = JsonConvert.DeserializeObject<ValidationError>(await response.Content.ReadAsStringAsync());
                throw error;
            }
            if ((int)response.StatusCode > 400)
            {
                throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.", null, response.StatusCode);
            }
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> PostAsync(Uri endpoint, object payload)
        {
            return await PayloadAsync(endpoint, payload, HttpMethod.Post);
        }

        public async Task<string> DeleteAsync(Uri endpoint, object payload)
        {
            return await PayloadAsync(endpoint, payload, HttpMethod.Delete);
        }

        public async Task<string> PutAsync(Uri endpoint, object payload)
        {
            return await PayloadAsync(endpoint, payload, HttpMethod.Put);
        }

        private async Task PrepareAuthenticatedClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _Scope });
            Debug.WriteLine($"access token-{accessToken}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
