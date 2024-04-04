using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static OAuthDeps<TOptions> ResolveOAuthDeps<TOptions>(HttpContext context) where TOptions: OAuthOptions =>
    ResolveService<OAuthDeps<TOptions>>(context);

  static HttpClient ResolveHttpClient<TOptions>(HttpContext context) where TOptions: OAuthOptions =>
    ResolveOAuthDeps<TOptions>(context).HttpClient;

  static PropertiesDataFormat ResolvePropertiesDataFormat<TOptions>(HttpContext context) where TOptions: OAuthOptions =>
    ResolveOAuthDeps<TOptions>(context).PropertiesDataFormat;

  static TimeProvider ResolveTimeProvider<TOptions>(HttpContext context) where TOptions: OAuthOptions =>
    ResolveOAuthDeps<TOptions>(context).TimeProvider;
}