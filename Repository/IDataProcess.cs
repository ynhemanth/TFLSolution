using Model;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDataProcess
    {
        Task<string> GetRoadStatusCall(RoadDetails roadStatusInfo);
    }
}
