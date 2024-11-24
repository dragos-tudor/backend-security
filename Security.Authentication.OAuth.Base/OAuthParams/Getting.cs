
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? GetOAuthParam(OAuthParams authParams, string paramKey) => authParams.TryGetValue(paramKey, out string? paramValue)? paramValue: default;
}