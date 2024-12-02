
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

public delegate string? ChallengeOAuthFunc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps)
  where TOptions : OAuthOptions;
