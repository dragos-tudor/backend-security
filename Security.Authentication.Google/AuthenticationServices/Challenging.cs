
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class Funcs {

  public static string? ChallengeGoogle (
    HttpContext context,
    AuthenticationProperties authProperties,
    GoogleOptions googleOptions,
    DateTimeOffset currentUtc) =>
      ChallengeOAuth(
        context,
        authProperties,
        googleOptions,
        currentUtc);

}