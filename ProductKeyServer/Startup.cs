using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductKeyServer.Startup))]
namespace ProductKeyServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
