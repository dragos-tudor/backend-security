
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

public delegate PostAuthorizeResult PostAuthorizeFunc<TOptions>(
    HttpContext context,
    TOptions oauthOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions: OAuthOptions;