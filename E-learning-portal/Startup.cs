using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_learning_portal.Startup))]
namespace E_learning_portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
