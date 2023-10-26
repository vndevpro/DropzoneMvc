using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DropzoneMvcDemo.Startup))]
namespace DropzoneMvcDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
