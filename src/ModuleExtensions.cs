using System;
using System.Threading.Tasks;
using Carter.Response;
using Microsoft.AspNetCore.Http;

namespace ActiveDirectory
{
    public static class ModuleExtensions
    {
        public static Task RunHandler<TOut>(this HttpResponse res, Func<TOut> handler)
        {
            try
            {
                var response = handler();

                if (response == null)
                {
                    res.StatusCode = 204;
                    return Task.CompletedTask;
                }

                res.StatusCode = 200;
                return res.Negotiate(response);
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                return res.Negotiate(ex.Message);
            }
        }
    }
}
