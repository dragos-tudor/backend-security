
namespace Security.Samples;

partial class SampleFuncs
{
  const string LogoutUserEndpointName = "/logout";

  static string? LogoutUserEndpoint(HttpContext context) =>
    SignOutCookie(context, CreateAuthenticationProperties());
}