
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using static Security.Authentication.Funcs;

namespace Security.Authentication.Google;

partial class Funcs {

  public static IServiceCollection AddGoogle(
    this IServiceCollection services,
    SetFunc<GoogleOptions>? setOptions = default,
    string? schemeName = GoogleDefaults.AuthenticationScheme) =>
      services
        .AddSingleton((services) => (setOptions ?? Identity)(
          CreateGoogleOptions(ResolveService<IDataProtectionProvider>(services), schemeName!)))
        .TryAddSingleton(TimeProvider.System);

}