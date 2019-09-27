using Model;

namespace Service
{
    public interface IHelperClass
    {
        string formatAPIAddress(RoadDetails roadDetails);

        string formatAPIKey(RoadDetails roadDetails);

        T JsonConverter<T>(string result);
    }
}
