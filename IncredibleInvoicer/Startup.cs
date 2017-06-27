using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IncredibleInvoicer.Startup))]
namespace IncredibleInvoicer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
