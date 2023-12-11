
namespace Security.Samples;

partial class Funcs {

  static SetFunc<GoogleOptions> SetGoogleOptions(WebApplicationBuilder app) => options => options with
  {
    ClientId = app.Configuration["google:clientid"]!,
    ClientSecret = app.Configuration["google:clientsecret"]!,
    AccessDeniedPath = "/accessdenied",
    ReturnUrlParameter = "returnUrl"
  };

}