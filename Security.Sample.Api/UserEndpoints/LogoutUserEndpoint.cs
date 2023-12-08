
namespace Security.Samples;

partial class Funcs {

  const string LogoutUserEndpointName = "/logout";

  readonly static Func<HttpContext, string?> LogoutUserEndpoint = (context) =>
    SignOutCookie(
      context,
      CreateAuthenticationProperties(),
      ResolveService<CookieAuthenticationOptions>(context),
      ResolveService<CookieBuilder>(context)
    );

}