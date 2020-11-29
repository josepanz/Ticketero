using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ticketero.Startup))]
namespace Ticketero
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
