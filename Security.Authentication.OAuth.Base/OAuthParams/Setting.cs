
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string SetOAuthParam(OAuthParams oauthParams, string key, string value) => oauthParams[key] = value;

  public static OAuthParams SetAdditionalOAuthParams(OAuthParams oauthParams, OAuthParams otherParams) =>
    otherParams
      .Aggregate(oauthParams, (result, otherParam) => {
        SetOAuthParam(result, otherParam.Key, otherParam.Value);
        return result;
      });
}