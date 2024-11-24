
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

public delegate AuthorizationResult PostAuthorizationFunc<TOptions>(
  HttpContext context,
  TOptions authOptions,
  PropertiesDataFormat authPropsProtector) where TOptions: OAuthOptions;