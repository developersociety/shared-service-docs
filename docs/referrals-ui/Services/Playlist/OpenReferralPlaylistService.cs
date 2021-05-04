using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenReferralPOV.Data;
using OpenReferralPOV.Services.HttpClientAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public class OpenReferralPlaylistService : IOpenReferralPlaylistService
    {
        private IHttpClientAdapter _httpClientAdapter;
        private readonly string _ApiBaseAddress = string.Empty;

        public OpenReferralPlaylistService(IHttpClientAdapter httpClientAdapter, IConfiguration configuration)
        {
            _httpClientAdapter = httpClientAdapter;
            _ApiBaseAddress = configuration["ORApi:BaseUrl"];
        }


        public async Task<Playlist> Update(Playlist playListToSubmit)
        {
            var responseString = await _httpClientAdapter.PostAsync(new Uri($"{ _ApiBaseAddress}/Playlist"), playListToSubmit);
            var playlist = JsonConvert.DeserializeObject<Playlist>(responseString);
            return playlist;
        }

        public async Task<Playlist> Get()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/Playlist"));
            var playlist = JsonConvert.DeserializeObject<Playlist>(responseString);
            return playlist;
        }
    }
}
