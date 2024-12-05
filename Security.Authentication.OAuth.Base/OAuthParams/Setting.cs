
using System.Linq;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string SetOAuthParam(OAuthParams oauthParams, string key, string value) => oauthParams[key] = value;

  public static OAuthParams SetOAuthParams(OAuthParams oauthParams, OAuthParams additionalParams) =>
    additionalParams.Aggregate(oauthParams, (result, additioalParam) => { result[additioalParam.Key] = additioalParam.Value; return result; });
}