
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using static Security.Authentication.Funcs;

namespace Security.Authentication.Google;

partial class Funcs {

  public static IServiceCollection AddGoogle(
    this IServiceCollection services,
    ConfigFunc<GoogleOptions>? configOptionsFunc = default,
    string? schemeName = GoogleDefaults.AuthenticationScheme) =>
      services
        .AddSingleton((services) => (configOptionsFunc ?? Identity)(
          CreateGoogleOptions(ResolveService<IDataProtectionProvider>(services), schemeName!)))
        .TryAddSingleton(TimeProvider.System);

}