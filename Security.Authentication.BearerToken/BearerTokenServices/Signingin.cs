using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static async Task<AuthenticationTicket> SignInBearerToken(
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions,
    BearerTokenDataFormat bearerTokenProtector,
    RefreshTokenDataFormat refreshTokenProtector,
    DateTimeOffset currentUtc)
  {
    SetAuthenticationPropertiesExpires(authProperties, currentUtc, tokenOptions.BearerTokenExpiration);

    var bearerTokenTicket = CreateBearerTokenTicket(principal, authProperties, tokenOptions);
    var refreshTokenTicket = CreateRefreshTicket(principal, tokenOptions, currentUtc);

    var token = CreateAccessTokenResponse(bearerTokenTicket, refreshTokenTicket,
      tokenOptions, bearerTokenProtector, refreshTokenProtector);

    var tokenJsonTypeInfo = ResolveAccessTokenResponseJsonTypeInfo(context);
    await WriteResponseJsonContent(context.Response, token, tokenJsonTypeInfo, context.RequestAborted);

    LogSignInBearerToken(ResolveBearerTokenLogger(context), tokenOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return bearerTokenTicket;
  }

  public static Task<AuthenticationTicket> SignInBearerToken (
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties? authProperties = default) =>
      SignInBearerToken(
        context,
        principal,
        authProperties ?? new AuthenticationProperties(),
        ResolveRequiredService<BearerTokenOptions>(context),
        ResolveRequiredService<BearerTokenDataFormat>(context),
        ResolveRequiredService<RefreshTokenDataFormat>(context),
        ResolveRequiredService<TimeProvider>(context).GetUtcNow());
}