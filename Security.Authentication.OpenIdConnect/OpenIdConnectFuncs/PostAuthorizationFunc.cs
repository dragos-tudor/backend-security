
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<PostAuthorizationResult> PostAuthorizationFunc<TOptions>(
  HttpContext context,
  TOptions authOptions,
  OpenIdConnectConfiguration oidcConfiguration,
  PropertiesDataFormat propertiesDataFormat,
  StringDataFormat stringDataFormat)
where TOptions: OpenIdConnectOptions;