
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  static ClaimActionCollection MapTwitterClaimActions (ClaimActionCollection claimActions) {
    claimActions.MapJsonSubKey(ClaimTypes.Name, "data", "username");
    claimActions.MapJsonSubKey(ClaimTypes.NameIdentifier, "data", "id");
    claimActions.MapJsonSubKey("urn:twitter:name", "data", "name");
    return claimActions;
  }

}