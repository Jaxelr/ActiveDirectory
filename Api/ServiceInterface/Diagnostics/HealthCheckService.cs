using Api.Model.ActiveDirectory.Operations;
using NotDeadYet;
using NotDeadYet.Results;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using System.Net;

namespace Api.ServiceInterface
{
    public class HealthCheckService : Service
    {
        private readonly IHealthChecker healthChecker;

        public HealthCheckService(IHealthChecker healthChecker)
        {
            this.healthChecker = healthChecker;
        }

#pragma warning disable IDE0060 // Remove unused parameter
        public object Get(HealthCheck request)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var result = healthChecker.Check();
            var statusCode = (result.Status == HealthCheckStatus.NotOkay)
                ? HttpStatusCode.ServiceUnavailable
                : HttpStatusCode.OK;

            return new HttpResult(result, statusCode);
        }
    }
}
