
namespace Security.Sample.Api;

partial class ApiFuncs
{
  static AuthenticationProperties CreateAuthenticationProperties(DateTimeOffset? currentUtc = default) =>
    new() {
      AllowRefresh = true,
      IsPersistent = true,
      ExpiresUtc = currentUtc?.AddDays(15)
    };
}