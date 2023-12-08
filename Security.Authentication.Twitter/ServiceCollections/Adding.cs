
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using static Security.Authentication.Funcs;

namespace Security.Authentication.Twitter;

partial class Funcs {

  public static IServiceCollection AddTwitter(
    this IServiceCollection services,
    ConfigFunc<TwitterOptions>? configOptionsFunc = default,
    string? schemeName = TwitterDefaults.AuthenticationScheme) =>
      services
        .AddSingleton((services) => (configOptionsFunc ?? Identity)(
          CreateTwitterOptions(ResolveService<IDataProtectionProvider>(services), schemeName)))
        .TryAddSingleton(TimeProvider.System);

}