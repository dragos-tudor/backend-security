
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? GetChallengeParamsState(OAuthParams oauthParams) => GetOAuthParam(oauthParams, OAuthParamNames.State);
}