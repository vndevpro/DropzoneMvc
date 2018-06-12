using GdNet.Integrations.DropzoneMvc.Services;
using Rabbit.IOC;
using SimpleInjector.Packaging;

namespace DropzoneMvcDemo.App_Start.Modules
{
    public class ServicesModule : ModuleBase, IPackage
    {
        public void RegisterServices(SimpleInjector.Container container)
        {
            container.Register<IDropzoneAttachmentSecurityCheck, AllowAllDropzoneAttachmentSecurityCheck>();
        }
    }
}