
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs {

  public static IServiceCollection AddBearerTokenServices (this IServiceCollection services) =>
      AddBearerTokenServices(services, CreateBearerTokenOptions());

  public static IServiceCollection AddBearerTokenServices (
    this IServiceCollection services,
    BearerTokenOptions tokenOptions,
    IDataProtectionProvider? dataProtectionProvider = default) =>
      services
        .AddSingleton((services) => tokenOptions)
        .AddSingleton((services) =>
          CreateBearerTokenDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), tokenOptions.SchemeName))
        .AddSingleton((services) =>
          CreateRefreshTokenDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), tokenOptions.SchemeName))
        .AddSingleton(TimeProvider.System)
        .AddKeyedSingleton(CategoryNameLogger, (services, serviceKey) => CreateLogger(services, (string)serviceKey));
}