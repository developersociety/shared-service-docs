using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services.HttpClientAdapter
{
    public interface IHttpClientAdapter
    {
        Task<string> GetAsync(Uri endpoint);
        Task<string> PostAsync(Uri endpoint, object payload);


        Task<string> DeleteAsync(Uri endpoint, object payload);

        Task<string> PutAsync(Uri endpoint, object payload);
    }
}
