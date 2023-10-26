using FileUploadHandler;
using Rabbit.IOC;
using SimpleInjector.Packaging;

namespace DropzoneMvcDemo.App_Start.Modules
{
    public class ServicesModule : ModuleBase, IPackage
    {
        public void RegisterServices(SimpleInjector.Container container)
        {
            container.Register<IAttachmentSecurityService, AllowAllAttachmentSecurityService>();
        }
    }
}