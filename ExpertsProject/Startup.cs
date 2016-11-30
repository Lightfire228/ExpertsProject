using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExpertsProject.Startup))]
namespace ExpertsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
