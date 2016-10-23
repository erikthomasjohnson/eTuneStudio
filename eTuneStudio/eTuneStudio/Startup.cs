using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eTuneStudio.Startup))]
namespace eTuneStudio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
