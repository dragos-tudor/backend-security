
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  static IEnumerable<ClaimMapper> GetFacebookClaimMappers() =>
    [
      new JsonKeyClaimMapper(ClaimTypes.NameIdentifier, "id"),
      new JsonSubKeyClaimMapper("urn:facebook:age_range_min", "age_range", "min"),
      new JsonSubKeyClaimMapper("urn:facebook:age_range_max", "age_range", "max"),
      new JsonKeyClaimMapper(ClaimTypes.DateOfBirth, "birthday"),
      new JsonKeyClaimMapper(ClaimTypes.Email, "email"),
      new JsonKeyClaimMapper(ClaimTypes.Name, "name"),
      new JsonKeyClaimMapper(ClaimTypes.GivenName, "first_name"),
      new JsonKeyClaimMapper("urn:facebook:middle_name", "middle_name"),
      new JsonKeyClaimMapper(ClaimTypes.Surname, "last_name"),
      new JsonKeyClaimMapper(ClaimTypes.Gender, "gender"),
      new JsonKeyClaimMapper("urn:facebook:link", "link"),
      new JsonSubKeyClaimMapper("urn:facebook:location", "location", "name"),
      new JsonKeyClaimMapper(ClaimTypes.Locality, "locale"),
      new JsonKeyClaimMapper("u;rn:facebook:timezone", "timezone")
    ];
};