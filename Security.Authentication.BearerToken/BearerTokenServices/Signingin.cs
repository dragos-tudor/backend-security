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
    DateTimeOffset currentUtc,
    BearerTokenDataFormat bearerTokenProtector,
    RefreshTokenDataFormat refreshTokenProtector,
    ILogger logger)
  {
    SetAuthenticationPropertiesExpires(authProperties, currentUtc, tokenOptions.BearerTokenExpiration);
    var bearerTokenTicket = CreateBearerTokenTicket(principal, authProperties, tokenOptions);

    var refreshAuthProperties = new AuthenticationProperties();
    SetAuthenticationPropertiesExpires(refreshAuthProperties, currentUtc, tokenOptions.RefreshTokenExpiration);
    var refreshTokenTicket = CreateRefreshTicket(principal, refreshAuthProperties, tokenOptions);

    var accessToken = CreateAccessTokenResponse(bearerTokenTicket, refreshTokenTicket, tokenOptions, bearerTokenProtector, refreshTokenProtector);
    var accessTokenJsonType = ResolveAccessTokenResponseJsonTypeInfo(context);
    await WriteResponseJsonContent(context.Response, accessToken, accessTokenJsonType, context.RequestAborted);

    LogSignInBearerToken(logger, tokenOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return bearerTokenTicket;
  }

  public static Task<AuthenticationTicket> SignInBearerToken(
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties? authProperties = default) =>
      SignInBearerToken(
        context,
        principal,
        authProperties ?? new AuthenticationProperties(),
        ResolveRequiredService<BearerTokenOptions>(context),
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ResolveRequiredService<BearerTokenDataFormat>(context),
        ResolveRequiredService<RefreshTokenDataFormat>(context),
        ResolveBearerTokenLogger(context));
}