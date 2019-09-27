using Service;
namespace Presentation
{
    public static class ContainerSetUp
    {
        public static void Init()
        {
            InitiateContainer.Register<IRoadStatusService, RoadStatusService>();
            InitiateContainer.Register<IHelperClass, HelperClass>();
            InitiateContainer.AddExtension<ServiceClassContainer>();
        }
    }
}
