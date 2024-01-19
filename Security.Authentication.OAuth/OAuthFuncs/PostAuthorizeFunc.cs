
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

public delegate (AuthenticationProperties?, string?) PostAuthorizeFunc<TOptions>(
  HttpContext context,
  TOptions authOptions,
  PropertiesDataFormat propertiesDataFormat)
where TOptions: OAuthOptions;