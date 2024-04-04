
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  static ClaimActionCollection MapFacebookClaimActions (ClaimActionCollection claimActions) {
    claimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
    claimActions.MapJsonSubKey("urn:facebook:age_range_min", "age_range", "min");
    claimActions.MapJsonSubKey("urn:facebook:age_range_max", "age_range", "max");
    claimActions.MapJsonKey(ClaimTypes.DateOfBirth, "birthday");
    claimActions.MapJsonKey(ClaimTypes.Email, "email");
    claimActions.MapJsonKey(ClaimTypes.Name, "name");
    claimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
    claimActions.MapJsonKey("urn:facebook:middle_name", "middle_name");
    claimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
    claimActions.MapJsonKey(ClaimTypes.Gender, "gender");
    claimActions.MapJsonKey("urn:facebook:link", "link");
    claimActions.MapJsonSubKey("urn:facebook:location", "location", "name");
    claimActions.MapJsonKey(ClaimTypes.Locality, "locale");
    claimActions.MapJsonKey("urn:facebook:timezone", "timezone");
    return claimActions;
  }

}