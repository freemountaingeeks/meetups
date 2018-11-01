using Microsoft.AspNet.SignalR;
using Owin;
using Warehouse.Infrastructure;

namespace Warehouse.Web
{
    public static class Startup
    {
        public static void Configure(IAppBuilder app, IFillLevelProvider provider)
        {
            var resolver = new DefaultDependencyResolver();
            app.UseStaticFiles("/Scripts");
            app.UseStaticFiles("/Images");
            app.UseStaticFiles("/Resources");
            var hubConfiguration = new HubConfiguration { Resolver = resolver };
            app.MapSignalR(hubConfiguration);
            app.UseNancy();
            FillLevelObserver.Init(resolver, provider);
        }
    }
}