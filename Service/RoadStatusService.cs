using Model;
using Newtonsoft.Json;
using Repository;
using System.Threading.Tasks;

namespace Service
{
    public class RoadStatusService : IRoadStatusService
    {
        private readonly IDataProcess _dataProcess;
        private readonly IHelperClass _helperClass;

        public RoadStatusService(IDataProcess dataProcess,
            IHelperClass helperClass)
        {
            _dataProcess = dataProcess;
            _helperClass = helperClass;
        }

        public async Task<RoadStatusInfo> FetchRoadStatus(RoadDetails roadDetails)
        {
            roadDetails.FormattedBaseUrl = _helperClass.formatAPIAddress(roadDetails);
            roadDetails.FormattedApiKeyDetails = _helperClass.formatAPIKey(roadDetails);
            roadDetails.ApiMediaType = Constants.ApiMediaType;

            var dataResult = await _dataProcess.GetRoadStatusCall(roadDetails);

            if (dataResult.Contains(Constants.statusCode) &&
                dataResult.Contains(Constants.status) )
            {
                return setUnsuccesData(roadDetails.RoadName);
            }

            var formattedStatus = _helperClass.JsonConverter<RoadStatusInfo>(dataResult);
            return formattedStatus;
        }

        private RoadStatusInfo setUnsuccesData(string RoadName)
        {
            return new RoadStatusInfo {
                httpStatusCode = Constants.statusCode,
                message= RoadName + Constants.notFoundMessage
            };
        }
         
    }
}
