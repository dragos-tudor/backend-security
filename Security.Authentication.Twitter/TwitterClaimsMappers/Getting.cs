
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  static IEnumerable<ClaimMapper> GetTwitterClaimMappers() => [
    new JsonClaimMapper(ClaimTypes.Name, "data:username"),
    new JsonClaimMapper(ClaimTypes.NameIdentifier, "data:id"),
    new JsonClaimMapper("urn:twitter:name", "data:name")
  ];
}