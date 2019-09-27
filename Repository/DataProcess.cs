using Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Repository
{
    public class DataProcess : IDataProcess
    {        
        public async Task<string> GetRoadStatusCall(RoadDetails roadDetails)
        {            
            var client = new HttpClient();
            client.BaseAddress = new Uri(roadDetails.FormattedBaseUrl);                        
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(roadDetails.ApiMediaType));                        
            
            var response = await client.GetAsync(roadDetails.FormattedApiKeyDetails);
            var result = await response.Content.ReadAsStringAsync();            
            
            client.Dispose();

            return result;
        }
    }
}
