
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static CookieAuthenticationOptions SetCookieOptions() =>
    CreateCookieAuthenticationOptions();

  static CookieBuilder SetCookieBuilderSameSite (CookieBuilder cookieBuilder, SameSiteMode sameSiteMode) {
    cookieBuilder.SameSite = sameSiteMode;
    return cookieBuilder;
  }
}