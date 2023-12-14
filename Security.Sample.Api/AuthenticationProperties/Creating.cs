
namespace Security.Samples;

partial class SampleFuncs {

  static AuthenticationProperties CreateAuthenticationProperties(DateTimeOffset? currentUtc = default) =>
    new() {
      AllowRefresh = true,
      IsPersistent = true,
      ExpiresUtc = currentUtc?.AddDays(15)
    };

}