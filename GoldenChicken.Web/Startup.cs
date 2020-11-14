using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoldenChicken.Web.Startup))]
namespace GoldenChicken.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
