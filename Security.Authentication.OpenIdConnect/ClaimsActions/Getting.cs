
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static IEnumerable<ClaimAction> GetOpenIdConnectClaimActions() => [
    new DeleteClaimAction("nonce"),
    new DeleteClaimAction("aud"),
    new DeleteClaimAction("azp"),
    new DeleteClaimAction("acr"),
    new DeleteClaimAction("iss"),
    new DeleteClaimAction("iat"),
    new DeleteClaimAction("nbf"),
    new DeleteClaimAction("exp"),
    new DeleteClaimAction("at_hash"),
    new DeleteClaimAction("c_hash"),
    new DeleteClaimAction("ipaddr"),
    new DeleteClaimAction("platf"),
    new DeleteClaimAction("ver")
  ];
}