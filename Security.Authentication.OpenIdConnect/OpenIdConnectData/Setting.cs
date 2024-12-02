
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string SetOidcData(OidcData oidcData, string key, string value) => oidcData[key] = value;

  static string SetOidcDataState(OidcData oidcData, string state) => SetOidcData(oidcData, OidcParamNames.State, state);
}