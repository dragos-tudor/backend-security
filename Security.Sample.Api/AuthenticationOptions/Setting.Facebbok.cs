
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static FacebookOptions SetFacebookOptions(WebApplicationBuilder app) =>
    CreateFacebookOptions(app.Configuration["Secrets:facebook:appid"]!, app.Configuration["Secrets:facebook:appsecret"]!) with {
      CallbackPath = $"/api{FacebookDefaults.CallbackPath}",
      ChallengePath = $"/api{FacebookDefaults.ChallengePath}"
    };
}