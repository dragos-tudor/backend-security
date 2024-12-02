
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static Claim[] MapOAuthOptionsClaims(OAuthOptions authOptions, JsonElement claims) =>
    authOptions.ClaimMappers
      .SelectMany(mapper => MapClaim(mapper, claims, GetClaimsIssuer(authOptions)))
      .ToArray();
}