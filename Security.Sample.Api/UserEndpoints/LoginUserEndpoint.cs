
namespace Security.Samples;

partial class Funcs {

  const string LoginUserEndpointName = "/login";

  readonly static Func<HttpContext, string?> LoginUserEndpoint = (context) =>
    ValidateUserCredentials(context.Request) ?
      SignInCookie(
        context,
        CreateClaimsPrincipal(
          ResolveService<CookieAuthenticationOptions>(context).SchemeName,
          new [] { CreateNameClaim(GetUserName(context.Request)!) }),
        CreateAuthenticationProperties(),
        ResolveService<CookieAuthenticationOptions>(context),
        ResolveService<CookieBuilder>(context),
        ResolveService<TimeProvider>(context).GetUtcNow()
      ).AuthenticationScheme:
      default;

}