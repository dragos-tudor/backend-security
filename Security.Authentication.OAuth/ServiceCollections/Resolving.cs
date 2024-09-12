using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static HttpClient ResolveHttpClient<TOptions>(HttpContext context) where TOptions: OAuthOptions => ResolveRequiredService<HttpClient>(context);

  static PropertiesDataFormat ResolvePropertiesDataFormat<TOptions>(HttpContext context) where TOptions: OAuthOptions => ResolveRequiredService<PropertiesDataFormat>(context);

  static TimeProvider ResolveTimeProvider<TOptions>(HttpContext context) where TOptions: OAuthOptions => ResolveRequiredService<TimeProvider>(context);
}