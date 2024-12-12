
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string SetOAuthParam(OAuthParams oauthParams, string key, string value) => oauthParams[key] = value;

  public static OAuthParams SetOAuthParams(OAuthParams oauthParams, OAuthParams otherParams) =>
    otherParams
      .Aggregate(oauthParams, (@params, otherParam) => {
        SetOAuthParam(@params, otherParam.Key, otherParam.Value);
        return @params;
      });
}