using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static async Task<AuthenticationTicket> SignInBearerToken(
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProps,
    BearerTokenOptions tokenOptions,
    DateTimeOffset currentUtc,
    BearerTokenDataFormat bearerTokenProtector,
    RefreshTokenDataFormat refreshTokenProtector,
    ILogger logger)
  {
    SetAuthPropsExpires(authProps, currentUtc, tokenOptions.BearerTokenExpiration);
    var bearerTokenTicket = CreateBearerTokenTicket(principal, authProps, tokenOptions);

    var refreshAuthProperties = new AuthenticationProperties();
    SetAuthPropsExpires(refreshAuthProperties, currentUtc, tokenOptions.RefreshTokenExpiration);
    var refreshTokenTicket = CreateRefreshTicket(principal, refreshAuthProperties, tokenOptions);

    var accessToken = CreateAccessTokenResponse(bearerTokenTicket, refreshTokenTicket, tokenOptions, bearerTokenProtector, refreshTokenProtector);
    var accessTokenJsonType = ResolveAccessTokenResponseJsonTypeInfo(context);
    await WriteHttpResponseJsonContent(context.Response, accessToken, accessTokenJsonType, context.RequestAborted);

    LogSignInBearerToken(logger, tokenOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return bearerTokenTicket;
  }
}