
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string CategoryNameLogger = nameof(OpenIdConnectFuncs);

  static ILogger ResolveOpenIdConnectLogger (HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, CategoryNameLogger);
}