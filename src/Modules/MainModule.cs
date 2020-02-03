using System.Threading.Tasks;
using ActiveDirectory.Entities;
using Carter;

namespace ActiveDirectory.Modules
{
    public class MainModule : CarterModule
    {
        public MainModule(AppSettings appSettings)
        {
            Get("/", (_, res) =>
            {
                res.Redirect(appSettings.RouteDefinition.RoutePrefix);

                return Task.CompletedTask;
            });
        }
    }
}
