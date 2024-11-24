using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static IServiceCollection AddFacebookServices(
    this IServiceCollection services,
    FacebookOptions facebookOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default) =>
      services.AddOAuthServices(facebookOptions, httpClient, dataProtectionProvider, timeProvider);
}