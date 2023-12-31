using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static void SetAuthenticationFeature<TFeature>(HttpContext context, TFeature feature) =>
    context.Features.Set(feature);
}