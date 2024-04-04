using Microsoft.Extensions.Logging;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(OpenIdConnectFuncs).Namespace!);
}