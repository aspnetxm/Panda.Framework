using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Panda.Web.Startup))]
namespace Panda.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
