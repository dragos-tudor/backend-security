using Microsoft.Extensions.Logging;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(AuthorizationFuncs).Namespace!);
}