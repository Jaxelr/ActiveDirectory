using ServiceStack.MiniProfiler;
using System;

namespace Api
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var appHost = new AppHost();
            appHost.Init();
        }

#if DEBUG

        protected void Application_BeginRequest(object src, EventArgs e) => Profiler.Start();

        protected void Application_EndRequest(object src, EventArgs e) => Profiler.Stop();

#endif
    }
}