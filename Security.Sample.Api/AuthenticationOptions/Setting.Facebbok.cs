
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static FacebookOptions SetFacebookOptions(WebApplicationBuilder app) =>
    CreateFacebookOptions(app.Configuration["facebook:appid"]!, app.Configuration["facebook:appsecret"]!) with {
      CallbackPath = $"/api{FacebookDefaults.CallbackPath}",
      ChallengePath = $"/api{FacebookDefaults.ChallengePath}"
    };
}