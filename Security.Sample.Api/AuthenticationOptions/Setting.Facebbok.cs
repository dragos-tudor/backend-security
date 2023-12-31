
namespace Security.Samples;

partial class SampleFuncs {

  static FacebookOptions SetFacebookOptions(WebApplicationBuilder app) =>
    CreateFacebookOptions(app.Configuration["facebook:appid"]!, app.Configuration["facebook:appsecret"]!) with {
      CallbackPath = "/api" + FacebookDefaults.SigninPath,
      ChallengePath = "/api" + FacebookDefaults.ChallengePath
    };

}