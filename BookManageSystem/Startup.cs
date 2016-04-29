using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookManageSystem.Startup))]
namespace BookManageSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
