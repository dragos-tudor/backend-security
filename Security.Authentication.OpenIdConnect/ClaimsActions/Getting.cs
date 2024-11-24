
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static IEnumerable<ClaimAction> GetOpenIdConnectClaimActions() =>
    [
      new DeleteKeyClaimAction("nonce"),
      new DeleteKeyClaimAction("aud"),
      new DeleteKeyClaimAction("azp"),
      new DeleteKeyClaimAction("acr"),
      new DeleteKeyClaimAction("iss"),
      new DeleteKeyClaimAction("iat"),
      new DeleteKeyClaimAction("nbf"),
      new DeleteKeyClaimAction("exp"),
      new DeleteKeyClaimAction("at_hash"),
      new DeleteKeyClaimAction("c_hash"),
      new DeleteKeyClaimAction("ipaddr"),
      new DeleteKeyClaimAction("platf"),
      new DeleteKeyClaimAction("ver"),

      // http://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
      new UniqueKeyClaimAction("sub", "sub"),
      new UniqueKeyClaimAction("name", "name"),
      new UniqueKeyClaimAction("given_name", "given_name"),
      new UniqueKeyClaimAction("family_name", "family_name"),
      new UniqueKeyClaimAction("profile", "profile"),
      new UniqueKeyClaimAction("email", "email")
    ];
}