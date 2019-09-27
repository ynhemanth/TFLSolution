using Model;
using System.Threading.Tasks;

namespace Service
{
    public interface IRoadStatusService
    {
        Task<RoadStatusInfo> FetchRoadStatus(RoadDetails roadDetails);
    }
}
