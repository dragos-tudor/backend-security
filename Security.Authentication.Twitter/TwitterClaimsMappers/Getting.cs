
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  static IEnumerable<ClaimMapper> GetTwitterClaimMappers() =>
    [
      new JsonSubKeyClaimMapper(ClaimTypes.Name, "data", "username"),
      new JsonSubKeyClaimMapper(ClaimTypes.NameIdentifier, "data", "id"),
      new JsonSubKeyClaimMapper("urn:twitter:name", "data", "name")
    ];
}