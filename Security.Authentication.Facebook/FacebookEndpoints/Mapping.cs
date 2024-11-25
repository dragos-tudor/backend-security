using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  static RouteHandlerBuilder MapCallbackFacebook(this IEndpointRouteBuilder endpoints, PathString pattern, SignInFunc signIn) =>
    endpoints.MapGet(pattern, (Delegate)((HttpContext context) => CallbackFacebookEndpoint(context, signIn)));

  static RouteHandlerBuilder MapChallengeFacebook(this IEndpointRouteBuilder endpoints, PathString pattern) =>
    endpoints.MapGet(pattern, ChallengeFacebookEndpoint);

  public static(RouteHandlerBuilder CallbackHandler, RouteHandlerBuilder ChallengeHandler) MapFacebook(
    this IEndpointRouteBuilder endpoints,
    FacebookOptions facebookOptions,
    SignInFunc signIn) =>
      (endpoints.MapCallbackFacebook(facebookOptions.CallbackPath, signIn),
      endpoints.MapChallengeFacebook(facebookOptions.ChallengePath));
}