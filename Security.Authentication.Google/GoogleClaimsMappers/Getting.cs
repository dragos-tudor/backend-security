
namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  static IEnumerable<ClaimMapper> GetGoogleClaimMappers() =>
    [
      new JsonKeyClaimMapper(ClaimTypes.NameIdentifier, "id"),
      new JsonKeyClaimMapper(ClaimTypes.Name, "name"),
      new JsonKeyClaimMapper(ClaimTypes.GivenName, "given_name"),
      new JsonKeyClaimMapper(ClaimTypes.Surname, "family_name"),
      new JsonKeyClaimMapper(ClaimTypes.Email, "email"),
      new JsonKeyClaimMapper("urn:google:profile", "link"),
      new JsonSubKeyClaimMapper("urn:google:image", "image", "url")
    ];
}