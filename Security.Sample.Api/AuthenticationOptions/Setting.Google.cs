
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static GoogleOptions SetGoogleOptions(WebApplicationBuilder app) =>
    CreateGoogleOptions(app.Configuration["Secrets:google:clientid"]!, app.Configuration["Secrets:google:clientsecret"]!) with {
      CallbackPath = $"/api{GoogleDefaults.CallbackPath}",
      ChallengePath = $"/api{GoogleDefaults.ChallengePath}"
    };
}