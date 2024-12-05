
namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  static IEnumerable<ClaimMapper> GetGoogleClaimMappers() => [
    new JsonClaimMapper(ClaimTypes.NameIdentifier, "id"),
    new JsonClaimMapper(ClaimTypes.Name, "name"),
    new JsonClaimMapper(ClaimTypes.GivenName, "given_name"),
    new JsonClaimMapper(ClaimTypes.Surname, "family_name"),
    new JsonClaimMapper(ClaimTypes.Email, "email"),
    new JsonClaimMapper("urn:google:profile", "link"),
    new JsonClaimMapper("urn:google:image", "image:url")
  ];
}