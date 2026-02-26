using System;
using Microsoft.Extensions.Logging;

namespace ActiveDirectory.Loggers;
internal static partial class AdLogger
{
    [LoggerMessage(Level = LogLevel.Critical, Message = "A critical exception doesnt allow to load the correct dependencies")]
    internal static partial void CriticalDependencyLoad(ILogger logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Method: Authenticate user logged an exception")]
    internal static partial void WarningAuthenticateUser(ILogger logger, Exception ex);
}
