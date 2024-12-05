
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  static IEnumerable<ClaimMapper> GetFacebookClaimMappers() => [
    new JsonClaimMapper(ClaimTypes.NameIdentifier, "id"),
    new JsonClaimMapper("urn:facebook:age_range_min", "age_range:min"),
    new JsonClaimMapper("urn:facebook:age_range_max", "age_range:max"),
    new JsonClaimMapper(ClaimTypes.DateOfBirth, "birthday"),
    new JsonClaimMapper(ClaimTypes.Email, "email"),
    new JsonClaimMapper(ClaimTypes.Name, "name"),
    new JsonClaimMapper(ClaimTypes.GivenName, "first_name"),
    new JsonClaimMapper("urn:facebook:middle_name", "middle_name"),
    new JsonClaimMapper(ClaimTypes.Surname, "last_name"),
    new JsonClaimMapper(ClaimTypes.Gender, "gender"),
    new JsonClaimMapper("urn:facebook:link", "link"),
    new JsonClaimMapper("urn:facebook:location", "location:name"),
    new JsonClaimMapper(ClaimTypes.Locality, "locale"),
    new JsonClaimMapper("urn:facebook:timezone", "timezone")
    ];
};