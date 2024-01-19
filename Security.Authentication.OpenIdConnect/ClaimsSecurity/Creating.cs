namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static Claim CreateClaim(
    string claimType,
    string value,
    string valueType,
    string issuer) =>
      new(claimType, value, valueType, issuer);

}