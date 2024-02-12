using Microsoft.Extensions.Logging;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(BearerTokenFuncs).Namespace!);
}