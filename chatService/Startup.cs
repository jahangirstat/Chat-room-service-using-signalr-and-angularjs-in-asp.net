using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(chatService.Startup))]
namespace chatService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
