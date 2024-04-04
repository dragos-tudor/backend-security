namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static void AddIdentityClaim(ClaimsIdentity identity, Claim claim) =>
    identity.AddClaim(claim);
}