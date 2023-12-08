
namespace Security.Samples;

partial class Funcs {

  static readonly ConfigFunc<CookieAuthenticationOptions> ConfigureCookieOptions = options => options with
  {
    LoginPath = "/login",
    LogoutPath = "/logout",
    AccessDeniedPath = "/accessdenied",
    ReturnUrlParameter = "returnUrl"
  };

}