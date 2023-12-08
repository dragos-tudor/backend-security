
namespace Security.Samples;

partial class Funcs {

  static AuthenticationProperties CreateAuthenticationProperties(DateTimeOffset? currentUtc = default) =>
    new() {
      AllowRefresh = true,
      IsPersistent = true,
      ExpiresUtc = currentUtc?.AddDays(15)
    };

}