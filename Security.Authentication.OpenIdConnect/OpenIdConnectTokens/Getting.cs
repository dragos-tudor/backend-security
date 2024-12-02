
using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnect
{
  public static string? GetIdToken(OidcTokens oidcTokens) => oidcTokens.IdToken;

  public static string? GetAccessToken(OidcTokens oidcTokens) => oidcTokens.AccessToken;

  public static string? GetRefreshToken(OidcTokens oidcTokens) => oidcTokens.RefreshToken;

  public static string? GetExpiresIn(OidcTokens oidcTokens) => oidcTokens.ExpiresIn;

  public static string? GetTokenType(OidcTokens oidcTokens) => oidcTokens.TokenType;
}