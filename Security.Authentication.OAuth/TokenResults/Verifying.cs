
namespace Security.Authentication.OAuth;

partial class Funcs {

  static bool ExistsAccessToken (TokenResult tokenResult) =>
    tokenResult.TokenInfo?.AccessToken is not null;

}