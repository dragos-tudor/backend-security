using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  static RouteHandlerBuilder MapCallbackTwitter(this IEndpointRouteBuilder endpoints, PathString pattern, SignInFunc signIn) =>
    endpoints.MapGet(pattern, (Delegate)((HttpContext context) => CallbackTwitterEndpoint(context, signIn)));

  static RouteHandlerBuilder MapChallengeTwitter(this IEndpointRouteBuilder endpoints, PathString pattern) =>
    endpoints.MapGet(pattern, ChallengeTwitterEndpoint);

  public static(RouteHandlerBuilder CallbackHandler, RouteHandlerBuilder ChallengeHandler) MapTwitter(
    this IEndpointRouteBuilder endpoints,
    TwitterOptions twitterOptions,
    SignInFunc signIn) =>
     (endpoints.MapCallbackTwitter(twitterOptions.CallbackPath, signIn),
      endpoints.MapChallengeTwitter(twitterOptions.ChallengePath));
}