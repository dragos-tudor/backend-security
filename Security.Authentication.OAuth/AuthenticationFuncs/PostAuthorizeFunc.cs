
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

public delegate (AuthenticationProperties?, string?) PostAuthorizeFunc<T>(
  HttpContext context,
  T authOptions,
  ISecureDataFormat<AuthenticationProperties> secureDataFormat)
where T: OAuthOptions;