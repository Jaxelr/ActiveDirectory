namespace ActiveDirectory
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
#pragma warning disable IDE0060 // Remove unused parameter

        public static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseHealthChecks("/healthcheck")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
