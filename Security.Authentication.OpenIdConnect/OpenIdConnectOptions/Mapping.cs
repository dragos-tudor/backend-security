
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static ClaimActionCollection MapOpenIdConnectClaimActions (ClaimActionCollection claimActions)
  {
    claimActions.DeleteClaim("nonce");
    claimActions.DeleteClaim("aud");
    claimActions.DeleteClaim("azp");
    claimActions.DeleteClaim("acr");
    claimActions.DeleteClaim("iss");
    claimActions.DeleteClaim("iat");
    claimActions.DeleteClaim("nbf");
    claimActions.DeleteClaim("exp");
    claimActions.DeleteClaim("at_hash");
    claimActions.DeleteClaim("c_hash");
    claimActions.DeleteClaim("ipaddr");
    claimActions.DeleteClaim("platf");
    claimActions.DeleteClaim("ver");

    // http://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
    claimActions.MapUniqueJsonKey("sub", "sub");
    claimActions.MapUniqueJsonKey("name", "name");
    claimActions.MapUniqueJsonKey("given_name", "given_name");
    claimActions.MapUniqueJsonKey("family_name", "family_name");
    claimActions.MapUniqueJsonKey("profile", "profile");
    claimActions.MapUniqueJsonKey("email", "email");
    return claimActions;
  }

}