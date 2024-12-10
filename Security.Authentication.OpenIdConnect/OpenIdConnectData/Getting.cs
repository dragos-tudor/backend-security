
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? GetOidcDataAuthorizationCode(OidcData oidcData) => GetOidcDataValue(oidcData, OAuthParamNames.AuthorizationCode);

  static string? GetOidcDataAccessToken(OidcData oidcData) => GetOidcDataValue(oidcData, OidcParamNames.AccessToken);

  static string? GetOidcDataExpiresIn(OidcData oidcData) => GetOidcDataValue(oidcData, OidcParamNames.ExpiresIn);

  static string? GetOidcDataIdToken(OidcData oidcData) => GetOidcDataValue(oidcData, OidcParamNames.IdToken);

  static string? GetOidcDataRefreshToken(OidcData oidcData) => GetOidcDataValue(oidcData, OidcParamNames.RefreshToken);

  static string? GetOidcDataIss(OidcData oidcData) => GetOidcDataValue(oidcData, OidcParamNames.Iss);

  static string? GetOidcDataTokenType(OidcData oidcData) => GetOidcDataValue(oidcData, OidcParamNames.TokenType);

  static string? GetOidcDataSessionState(OidcData oidcData) => GetOidcDataValue(oidcData, OpenIdConnectSessionProperties.SessionState);

  static string? GetOidcDataState(OidcData oidcData) => GetOidcDataValue(oidcData, OidcParamNames.State);

  static string? GetOidcDataSid(OidcData oidcData) => GetOidcDataValue(oidcData, OidcParamNames.Sid);

  public static string? GetOidcDataValue(OidcData oidcdata, string dataKey) => oidcdata.TryGetValue(dataKey, out string? value) ? value : default;
}