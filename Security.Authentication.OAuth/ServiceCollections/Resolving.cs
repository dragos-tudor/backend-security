using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static HttpClient ResolveHttpClient(HttpContext context) => ResolveRequiredService<HttpClient>(context);

  static PropertiesDataFormat ResolvePropertiesDataFormat(HttpContext context) => ResolveRequiredService<PropertiesDataFormat>(context);

  static TimeProvider ResolveTimeProvider(HttpContext context) => ResolveRequiredService<TimeProvider>(context);
}