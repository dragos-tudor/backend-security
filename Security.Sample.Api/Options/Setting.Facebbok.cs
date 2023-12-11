
namespace Security.Samples;

partial class Funcs {

  static SetFunc<FacebookOptions> SetFacebookOptions(WebApplicationBuilder app) => options => options with
  {
    ClientId = app.Configuration["facebook:appid"]!,
    ClientSecret = app.Configuration["facebook:appsecret"]!,
    AccessDeniedPath = "/accessdenied",
    ReturnUrlParameter = "returnUrl"
  };

}