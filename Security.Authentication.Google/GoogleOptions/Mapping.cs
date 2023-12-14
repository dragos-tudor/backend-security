
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.Google;

partial class GoogleFuncs {

  static ClaimActionCollection MapGoogleClaimActions (ClaimActionCollection claimActions) {
    claimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
    claimActions.MapJsonKey(ClaimTypes.Name, "name");
    claimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
    claimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
    claimActions.MapJsonKey(ClaimTypes.Email, "email");
    claimActions.MapJsonKey("urn:google:profile", "link");
    claimActions.MapJsonSubKey("urn:google:image", "image", "url");
    return claimActions;
  }

}