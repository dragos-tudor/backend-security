
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? GetOAuthParamsState(OAuthParams oauthParams) => GetOAuthParam(oauthParams, OAuthParamNames.State);
}