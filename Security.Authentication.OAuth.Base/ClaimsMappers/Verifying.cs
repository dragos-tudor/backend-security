
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static bool EqualsClaimKey(ClaimMapper claimMapper, string key) => string.Equals(claimMapper.KeyName, key, StringComparison.OrdinalIgnoreCase);
}