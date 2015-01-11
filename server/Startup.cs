using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using System.Net;
using EventStore.ClientAPI;
using Recipes.Domain;

namespace KWebStartup
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            var endpoint = new IPEndPoint(IPAddress.Loopback, 1113);
            var connection = EventStoreConnection.Create(endpoint);
            connection.ConnectAsync().Wait();
            var factory = new AggregateFactory();
            var repository = new GesRepository(connection, factory);

            app.UseServices(services =>
            {
                services.AddMvc();

                services.AddSingleton<IUniqueKeyGenerator, UniqueKeyGenerator>();
                services.AddInstance<IRepository>(repository);
                services.AddTransient<RecipeApplicationService>();
            });

            // Add MVC to the request pipeline
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "api",
                    template: "{controller}/{id?}");
            });
        }
    }
}
