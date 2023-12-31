
namespace Security.Samples;

partial class SampleFuncs {

  static TwitterOptions SetTwitterOptions(WebApplicationBuilder app) =>
    CreateTwitterOptions(app.Configuration["twitter:consumerkey"]!, app.Configuration["twitter:consumersecret"]!) with {
      CallbackPath = "/api" + TwitterDefaults.SigninPath,
      ChallengePath = "/api" + TwitterDefaults.ChallengePath
    };

}