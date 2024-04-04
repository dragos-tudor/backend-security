
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static IServiceCollection AddGoogle(
    this IServiceCollection services,
    GoogleOptions googleOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default) =>
      services.AddOAuth(googleOptions, httpClient, dataProtectionProvider, timeProvider);
}