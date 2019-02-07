using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheSocialNetwork.WebApp.Startup))]
namespace TheSocialNetwork.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
