
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static GoogleOptions SetGoogleOptions(WebApplicationBuilder app) =>
    CreateGoogleOptions(app.Configuration["Secrets:Google:ClientId"]!, app.Configuration["Secrets:Google:ClientSecret"]!) with {
      CallbackPath = $"/accounts{GoogleDefaults.CallbackPath}",
      ChallengePath = $"/accounts{GoogleDefaults.ChallengePath}"
    };
}