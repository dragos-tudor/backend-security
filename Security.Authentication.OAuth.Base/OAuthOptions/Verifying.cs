
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static bool IsCodeResponseType(OAuthOptions authOptions) => string.Equals(authOptions.ResponseType, OAuthResponseType.Code, StringComparison.Ordinal);
}