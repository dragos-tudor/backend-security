using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(OAuthFuncs).Namespace!);
}