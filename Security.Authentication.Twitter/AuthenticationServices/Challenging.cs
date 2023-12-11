
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class Funcs {

  public static string ChallengeTwitter (
    HttpContext context,
    AuthenticationProperties authProperties,
    TwitterOptions twitterOptions,
    DateTimeOffset currentUtc) =>
      ChallengeOAuth(
        context,
        authProperties,
        twitterOptions,
        currentUtc
      );

}