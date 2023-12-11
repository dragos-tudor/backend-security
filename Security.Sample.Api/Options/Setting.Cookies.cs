
namespace Security.Samples;

partial class Funcs {

  static readonly SetFunc<CookieAuthenticationOptions> SetCookieOptions = options => options with
  {
    LoginPath = "/login",
    LogoutPath = "/logout",
    AccessDeniedPath = "/accessdenied",
    ReturnUrlParameter = "returnUrl"
  };

}