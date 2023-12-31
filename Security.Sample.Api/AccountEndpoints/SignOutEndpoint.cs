
namespace Security.Samples;

partial class SampleFuncs
{
  static string? SignOutEndpoint(HttpContext context) =>
    SignOutCookie(context, CreateAuthenticationProperties());
}