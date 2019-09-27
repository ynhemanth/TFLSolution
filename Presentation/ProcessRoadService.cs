using Service;
using Model;
using System.Threading.Tasks;

namespace Presentation
{
    public class ProcessRoadService
    {
        private readonly IRoadStatusService _roadStatusService;

        public ProcessRoadService(IRoadStatusService roadStatusService)
        {
            _roadStatusService = roadStatusService;
        }
        public async Task<RoadStatusInfo> FetchRoadStatusDetails(RoadDetails roadDetails)
        {
            return await _roadStatusService.FetchRoadStatus(roadDetails);
        }
    }
}
