
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static ClaimMapper? GetClaimMapper(IEnumerable<ClaimMapper> claimMappers, string key) => claimMappers.FirstOrDefault(claimMapper => EqualsClaimKey(claimMapper, key));
}