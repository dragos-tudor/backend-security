
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static AuthenticationProperties CreateAuthenticationProperties(DateTimeOffset? currentUtc = default) =>
    new() {
      AllowRefresh = true,
      IsPersistent = true,
      ExpiresUtc = currentUtc?.AddDays(15)
    };
}