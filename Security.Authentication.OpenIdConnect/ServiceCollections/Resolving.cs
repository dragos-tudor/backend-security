using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static HttpClient ResolveHttpClient(HttpContext context) => ResolveRequiredService<HttpClient>(context);

  static PropertiesDataFormat ResolvePropertiesDataFormat(HttpContext context) => ResolveRequiredService<PropertiesDataFormat>(context);

  static TimeProvider ResolveTimeProvider(HttpContext context) => ResolveRequiredService<TimeProvider>(context);
}