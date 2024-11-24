
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static IServiceCollection AddTwitterServices(
    this IServiceCollection services,
    TwitterOptions twitterOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default) =>
      services.AddOAuthServices(twitterOptions, httpClient, dataProtectionProvider, timeProvider);
}