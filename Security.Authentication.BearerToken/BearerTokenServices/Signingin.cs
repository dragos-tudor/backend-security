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
    BearerTokenProtector bearerTokenProtector,
    RefreshTokenProtector refreshTokenProtector,
    DateTimeOffset currentUtc
  )
  {
    SetAuthenticationPropertiesExpires(authProperties, currentUtc, tokenOptions.BearerTokenExpiration);

    var bearerTokenTicket = CreateBearerTokenTicket(principal, authProperties, tokenOptions);
    var refreshTokenTicket = CreateRefreshTicket(principal, tokenOptions, currentUtc);

    var token = CreateAccessTokenResponse(bearerTokenTicket, refreshTokenTicket,
      tokenOptions, bearerTokenProtector, refreshTokenProtector);

    var tokenJsonTypeInfo = ResolveAccessTokenResponseJsonTypeInfo(context);
    await context.Response.WriteAsJsonAsync(token, tokenJsonTypeInfo);

    LogSignInBearerToken(Logger, tokenOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
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
        ResolveService<BearerTokenOptions>(context),
        ResolveService<BearerTokenProtector>(context),
        ResolveService<RefreshTokenProtector>(context),
        ResolveService<TimeProvider>(context).GetUtcNow());
}