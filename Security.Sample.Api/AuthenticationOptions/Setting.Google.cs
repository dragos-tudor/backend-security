
namespace Security.Samples;

partial class SampleFuncs {

  static GoogleOptions SetGoogleOptions(WebApplicationBuilder app) =>
    CreateGoogleOptions(app.Configuration["google:clientid"]!, app.Configuration["google:clientsecret"]!) with {
      CallbackPath = "/api" + GoogleDefaults.SigninPath,
      ChallengePath = "/api" + GoogleDefaults.ChallengePath
    };

}