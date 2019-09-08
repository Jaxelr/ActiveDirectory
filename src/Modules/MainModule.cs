using System.Threading.Tasks;
using ActiveDirectory.Entities;
using Carter;

namespace ActiveDirectory.Modules
{
    public class MainModule : CarterModule
    {
        public MainModule(AppSettings appSettings)
        {
            Get("/", (req, res, routeData) =>
            {
                res.Redirect(appSettings.RouteDefinition.RoutePrefix);

                return Task.CompletedTask;
            });
        }
    }
}
