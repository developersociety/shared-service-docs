using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using OpenReferralPOV.Data;
using OpenReferralPOV.Data.Enums;
using OpenReferralPOV.Services.HttpClientAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OpenReferralPOV.Services
{
    public class OpenReferralService : IOpenReferralService
    {
        private IHttpClientAdapter _httpClientAdapter;
        private readonly string _ApiBaseAddress = string.Empty;

        public OpenReferralService(IHttpClientAdapter httpClientAdapter, IConfiguration configuration)
        {
            _httpClientAdapter = httpClientAdapter;
            _ApiBaseAddress = configuration["ORApi:BaseUrl"];
        }
       

        public async Task<Service> GetServiceById(string id)
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/Services/{id}"));
            var service = JsonConvert.DeserializeObject<Service>(responseString);
            return service;
        }

        public async Task<Location> GetLocationById(string id)
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/Locations/{id}"));
            var location = JsonConvert.DeserializeObject<Location>(responseString);
            return location;
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/Services"));
            var services = JsonConvert.DeserializeObject<IEnumerable<Service>>(responseString);

            return services;
        }

        public async Task<IEnumerable<Service>> GetServicesAsync(string postcode, double proximity)
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/Services?postcode={postcode}&proximity={proximity}"));
            var services = JsonConvert.DeserializeObject<IEnumerable<Service>>(responseString);

            return services;
        }

        public async Task<Service> UpdateService(Service service)
        {
            var responseString = await _httpClientAdapter.PutAsync(new Uri($"{ _ApiBaseAddress}/Services/{service.Id}"), service);
            var updatedService = JsonConvert.DeserializeObject<Service>(responseString);
            return updatedService;
        }

        public async Task<Location> AddLocation(Location location)
        {
            var responseString = await _httpClientAdapter.PostAsync(new Uri($"{ _ApiBaseAddress}/Locations"), location);
            var addedLocation = JsonConvert.DeserializeObject<Location>(responseString);
            return addedLocation;
        }


        public async Task<Service> AddService(Service service)
        {
            var responseString = await _httpClientAdapter.PostAsync(new Uri($"{ _ApiBaseAddress}/Services"), service);
            var addedService = JsonConvert.DeserializeObject<Service>(responseString);
            return addedService;
        }

    }
}
