
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static ClaimMapper? GetClaimMapper(IEnumerable<ClaimMapper> claimMappers, KeyValuePair<string, string> claim) =>
    claimMappers.FirstOrDefault(claimMapper => EqualsClaimKey(claimMapper, claim.Key));
}