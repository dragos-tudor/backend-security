using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static HttpClient ResolveHttpClient(HttpContext context) => ResolveRequiredService<HttpClient>(context);

  static PropertiesDataFormat ResolvePropertiesDataFormat(HttpContext context) => ResolveRequiredService<PropertiesDataFormat>(context);

  static StringDataFormat ResolveStringDataFormat(HttpContext context) => ResolveRequiredService<StringDataFormat>(context);

  static TimeProvider ResolveTimeProvider(HttpContext context) => ResolveRequiredService<TimeProvider>(context);
}