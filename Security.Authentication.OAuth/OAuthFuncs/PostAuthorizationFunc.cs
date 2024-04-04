
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

public delegate PostAuthorizationResult PostAuthorizationFunc<TOptions>(
  HttpContext context,
  TOptions authOptions,
  PropertiesDataFormat propertiesDataFormat)
where TOptions: OAuthOptions;