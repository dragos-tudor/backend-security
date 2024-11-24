using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  static RouteHandlerBuilder MapCallbackGoogle(this IEndpointRouteBuilder endpoints, PathString pattern, SignInFunc signIn) =>
    endpoints.MapGet(pattern,(Delegate)((HttpContext context) => CallbackGoogleEndpoint(context, signIn)));

  static RouteHandlerBuilder MapChallengeGoogle(this IEndpointRouteBuilder endpoints, PathString pattern) =>
    endpoints.MapGet(pattern, ChallengeGoogleEndpoint);

  public static(RouteHandlerBuilder CallbackHandler, RouteHandlerBuilder ChallengeHandler) MapGoogle(
    this IEndpointRouteBuilder endpoints,
    GoogleOptions googleOptions,
    SignInFunc signIn) =>
     (endpoints.MapCallbackGoogle(googleOptions.CallbackPath, signIn),
      endpoints.MapChallengeGoogle(googleOptions.ChallengePath));
}