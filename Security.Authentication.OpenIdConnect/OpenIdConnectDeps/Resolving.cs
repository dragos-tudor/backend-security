using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OpenIdConnectDeps<TOptions> ResolveOpenIdConnectDeps<TOptions>(HttpContext context) where TOptions: OpenIdConnectOptions =>
    ResolveRequiredService<OpenIdConnectDeps<TOptions>>(context);

  static HttpClient ResolveHttpClient<TOptions>(HttpContext context) where TOptions: OpenIdConnectOptions =>
    ResolveOpenIdConnectDeps<TOptions>(context).HttpClient;

  static PropertiesDataFormat ResolvePropertiesDataFormat<TOptions>(HttpContext context) where TOptions: OpenIdConnectOptions =>
    ResolveOpenIdConnectDeps<TOptions>(context).PropertiesDataFormat;

  static StringDataFormat ResolveStringDataFormat<TOptions>(HttpContext context) where TOptions: OpenIdConnectOptions =>
    ResolveOpenIdConnectDeps<TOptions>(context).StringDataFormat;

  static TimeProvider ResolveTimeProvider<TOptions>(HttpContext context) where TOptions: OpenIdConnectOptions =>
    ResolveOpenIdConnectDeps<TOptions>(context).TimeProvider;
}