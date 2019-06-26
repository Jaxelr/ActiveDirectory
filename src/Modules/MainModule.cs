using System.Threading.Tasks;
using ActiveDirectory.Entities;
using Carter;

namespace ActiveDirectory.Modules
{
    public class MainModule : CarterModule
    {
        public MainModule()
        {
            Get("/", (req, res, routeData) =>
            {
                res.Redirect(RouteDefinition.RoutePrefix);

                return Task.CompletedTask;
            });
        }
    }
}
