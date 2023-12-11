
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class Funcs {

  public static string? ChallengeFacebook (
    HttpContext context,
    AuthenticationProperties authProperties,
    FacebookOptions facebookOptions,
    DateTimeOffset currentUtc) =>
      ChallengeOAuth(
        context,
        authProperties,
        facebookOptions,
        currentUtc);

}