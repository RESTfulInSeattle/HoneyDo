using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HoneyDo.Startup))]
namespace HoneyDo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
