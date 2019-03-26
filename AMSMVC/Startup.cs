using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AMSMVC.Startup))]
namespace AMSMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
