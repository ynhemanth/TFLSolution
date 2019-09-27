using Repository;
using Unity;
using Unity.Extension;

namespace Service
{
    public class ServiceClassContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IDataProcess, DataProcess>();           
        }
    }
}
