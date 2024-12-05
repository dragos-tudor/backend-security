
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? GetOAuthParam(OAuthParams oauthParams, string paramKey) => oauthParams.TryGetValue(paramKey, out string? paramValue)? paramValue: default;
}