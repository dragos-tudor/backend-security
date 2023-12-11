
namespace Security.Samples;

partial class Funcs {

  static SetFunc<TwitterOptions> SetTwitterOptions(WebApplicationBuilder app) => options => options with
  {
    ClientId = app.Configuration["twitter:consumerkey"]!,
    ClientSecret = app.Configuration["twitter:consumersecret"]!,
    AccessDeniedPath = "/accessdenied",
    ReturnUrlParameter = "returnUrl"
  };

}