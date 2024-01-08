using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static Task SignInBearerToken(
    HttpContext context,
    ClaimsPrincipal user,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions,
    ISecureDataFormat<AuthenticationTicket> bearerTokenProtector,
    ISecureDataFormat<AuthenticationTicket> refershTokenProtector,
    DateTimeOffset currentUtc
  )
  {
    SetAuthenticationPropertiesExpires(authProperties, currentUtc, tokenOptions.BearerTokenExpiration);
    var token = CreateAccessTokenResponse(user, authProperties, tokenOptions,
      bearerTokenProtector, refershTokenProtector, currentUtc);

    var tokenJsonTypeInfo = ResolveAccessTokenJsonTypeInfo(context);
    return context.Response.WriteAsJsonAsync(token, tokenJsonTypeInfo);
  }
}