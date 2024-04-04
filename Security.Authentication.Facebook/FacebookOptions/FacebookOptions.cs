
using Security.Authentication.OAuth;

namespace Security.Authentication.Facebook;

public sealed record FacebookOptions : OAuthOptions
{
  // https://developers.facebook.com/docs/graph-api/security#appsecret_proof
  public bool SendAppSecretProof { get; init; }

  // https://developers.facebook.com/docs/graph-api/reference/user
  public ICollection<string> Fields { get; init; } = default!;

}