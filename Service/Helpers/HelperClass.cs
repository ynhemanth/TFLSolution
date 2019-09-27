using Model;
using Newtonsoft.Json;

namespace Service
{
    public class HelperClass : IHelperClass
    {
        public string formatAPIAddress(RoadDetails roadDetails)
        {
            return string.Format("{0}/{1}/{2}", roadDetails.BaseUrl, roadDetails.ApiName, roadDetails.RoadName);            
        }

        public string formatAPIKey(RoadDetails roadDetails)
        {
            return string.Format("?app_id={0}&app_key={1}", roadDetails.ApiId, roadDetails.ApiKey);
        }

        public T JsonConverter<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result.Substring(1, result.Length - 2));
        }
    }
}
