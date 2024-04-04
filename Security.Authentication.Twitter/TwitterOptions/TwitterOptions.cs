
using Security.Authentication.OAuth;

namespace Security.Authentication.Twitter;

public sealed record TwitterOptions : OAuthOptions
{
  public IEnumerable<string> Expansions { get; init; } = default!;
  public IEnumerable<string> TweetFields { get; init; } = default!;
  public IEnumerable<string> UserFields { get; init; } = default!;
}
