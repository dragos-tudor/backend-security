
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static TwitterOptions SetTwitterOptions(WebApplicationBuilder app) =>
    CreateTwitterOptions(app.Configuration["Secrets:twitter:consumerkey"]!, app.Configuration["Secrets:twitter:consumersecret"]!) with {
      CallbackPath = $"/api{TwitterDefaults.CallbackPath}",
      ChallengePath = $"/api{TwitterDefaults.ChallengePath}"
    };
}