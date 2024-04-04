
using System.Security.Claims;
using System.Text.Json;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static ClaimsIdentity AddClaimsIdentityClaims(
    ClaimsIdentity identity,
    JsonDocument userData,
    RemoteAuthenticationOptions remoteOptions)
  {
    var claimsIssuer = GetClaimsIssuer(remoteOptions);
    foreach (var claimAction in remoteOptions.ClaimActions)
      claimAction.Run(userData.RootElement, identity, claimsIssuer!);
    return identity;
  }
}