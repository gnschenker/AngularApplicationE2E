using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Recipes.Domain;

namespace KWebStartup
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseServices(services =>
            {
                services.AddMvc();

                services.AddSingleton<IRecipeApplicationService, RecipeApplicationServiceFake>();
                services.AddSingleton<IUniqueKeyGenerator, UniqueKeyGenerator>();
                services.AddSingleton<InMemoryRepository, InMemoryRepository>();
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
