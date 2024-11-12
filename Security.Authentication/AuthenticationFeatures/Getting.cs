using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static TFeature? GetAuthenticationFeature<TFeature>(HttpContext context) => context.Features.Get<TFeature>();
}