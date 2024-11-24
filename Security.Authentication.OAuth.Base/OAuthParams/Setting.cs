
using System.Linq;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string SetOAuthParam(OAuthParams authParams, string key, string value) => authParams[key] = value;

  public static OAuthParams SetOAuthParams(OAuthParams authParams, OAuthParams additionalParams) =>
    additionalParams.Aggregate(authParams, (result, additioalParam) => { result[additioalParam.Key] = additioalParam.Value; return result; });
}