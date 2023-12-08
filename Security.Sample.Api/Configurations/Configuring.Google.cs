
namespace Security.Samples;

partial class Funcs {

  static ConfigFunc<GoogleOptions> ConfigureGoogleOptions(WebApplicationBuilder app) => options => options with
  {
    ClientId = app.Configuration["google:clientid"]!,
    ClientSecret = app.Configuration["google:clientsecret"]!,
    AccessDeniedPath = "/accessdenied",
    ReturnUrlParameter = "returnUrl"
  };

}