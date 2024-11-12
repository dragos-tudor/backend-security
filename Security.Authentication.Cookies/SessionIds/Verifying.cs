
namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool IsSessionIdClaim(Claim claim) => claim.Type.Equals(SessionIdClaim, StringComparison.Ordinal);
}