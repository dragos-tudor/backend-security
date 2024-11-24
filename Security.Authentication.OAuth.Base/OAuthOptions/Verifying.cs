
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static bool IsCodeResponseType(OAuthOptions oidcOptions) => string.Equals(oidcOptions.ResponseType, OAuthResponseType.Code, StringComparison.Ordinal);
}