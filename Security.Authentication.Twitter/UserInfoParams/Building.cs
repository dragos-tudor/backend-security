
namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  const string Expansions = "expansions";
  const string TweetFields = "tweet.fields";
  const string UserFields = "user.fields";
  const char Separator = ',';

  static KeyValuePair<string, string?>[] BuildSpecificUserInfoParams(TwitterOptions authOptions) =>
    [
      ExistsTwitterOptionsParam(authOptions.Expansions) ? new(Expansions, JoinStrings(authOptions.Expansions)): default,
      ExistsTwitterOptionsParam(authOptions.TweetFields) ? new(TweetFields, JoinStrings(authOptions.TweetFields)): default,
      ExistsTwitterOptionsParam(authOptions.UserFields) ? new(UserFields, JoinStrings(authOptions.UserFields)): default,
    ];

  static string JoinStrings(IEnumerable<string> strings, char separator = Separator) =>
    string.Join(separator, strings);

}