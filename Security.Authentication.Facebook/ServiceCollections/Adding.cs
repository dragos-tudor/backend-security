
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using static Security.Authentication.Funcs;

namespace Security.Authentication.Facebook;

partial class Funcs {

  public static IServiceCollection AddFacebook(
    this IServiceCollection services,
    SetFunc<FacebookOptions>? setOptions = default,
    string? schemeName = FacebookDefaults.AuthenticationScheme) =>
      services
        .AddSingleton((services) => (setOptions ?? Identity)(
          CreateFacebookOptions(ResolveService<IDataProtectionProvider>(services), schemeName)))
        .TryAddSingleton(TimeProvider.System);

}