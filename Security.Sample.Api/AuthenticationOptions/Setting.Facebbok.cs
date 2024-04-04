
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static FacebookOptions SetFacebookOptions(WebApplicationBuilder app) =>
    CreateFacebookOptions(app.Configuration["Secrets:Facebook:AppId"]!, app.Configuration["Secrets:Facebook:AppSecret"]!) with {
      CallbackPath = $"/accounts{FacebookDefaults.CallbackPath}",
      ChallengePath = $"/accounts{FacebookDefaults.ChallengePath}"
    };
}