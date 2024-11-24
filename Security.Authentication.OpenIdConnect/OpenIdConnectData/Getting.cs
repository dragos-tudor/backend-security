
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? GetOidcDataValue(OidcData oidcdata, string dataKey) => oidcdata.TryGetValue(dataKey, out string? value)? value: default;

  public static string? GetOidcDataAuthorizationCode(OidcData oidcData) => GetOidcDataValue(oidcData, OAuthParamNames.AuthorizationCode);

  public static string? GetOidcDataState(OidcData oidcData) => GetOidcDataValue(oidcData, OAuthParamNames.State);
}