
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static Claim? ApplyDeleteClaimAction(DeleteClaimAction claimAction, Claim claim) => EqualsClaimType(claimAction, claim.Type)? default: claim;

  public static Claim? ApplyClaimAction(ClaimAction claimAction, Claim claim) =>
    claimAction switch {
      DeleteClaimAction deleteClaimAction => ApplyDeleteClaimAction(deleteClaimAction, claim),
      _ => claim
    };

  public static IEnumerable<Claim> ApplyClaimActions(IEnumerable<ClaimAction> claimActions, IEnumerable<Claim> claims) =>
    claims.Where(claim => claimActions.Select(claimAction => ApplyClaimAction(claimAction, claim)).All(claim => claim is not null));
}