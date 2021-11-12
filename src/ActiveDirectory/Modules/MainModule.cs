using System.Threading.Tasks;
using ActiveDirectory.Models.Internal;
using Carter;

namespace ActiveDirectory.Modules
{
    public class MainModule : CarterModule
    {
        public MainModule(AppSettings appSettings)
        {
            Get("/", (ctx) =>
            {
                ctx.Response.Redirect(appSettings.RouteDefinition.RoutePrefix);

                return Task.CompletedTask;
            });
        }
    }
}
