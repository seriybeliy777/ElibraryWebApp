using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ELibraryWebApp.Startup))]
namespace ELibraryWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
