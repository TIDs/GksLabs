using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GKSLab.Startup))]
namespace GKSLab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
