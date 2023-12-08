
namespace Security.Authentication.OAuth;

partial class Funcs {

  static string? GetAccessToken (TokenResult tokenResult) =>
    tokenResult.TokenInfo?.AccessToken;

}