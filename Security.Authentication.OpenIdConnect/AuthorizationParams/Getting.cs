
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? GetOAuthParamsState(OAuthParams authParams) => GetOAuthParam(authParams, OAuthParamNames.State);
}