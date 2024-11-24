
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static DateTimeOffset? GetCorrelationCookieOptionsExpires(OAuthOptions authOptions, DateTimeOffset currentUtc) => currentUtc.Add(authOptions.AuthenticationTimeout);
}