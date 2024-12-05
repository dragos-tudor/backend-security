
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static DateTimeOffset? GetCorrelationCookieOptionsExpires(OAuthOptions oauthOptions, DateTimeOffset currentUtc) => currentUtc.Add(oauthOptions.AuthenticationTimeout);
}