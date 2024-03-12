
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static TwitterOptions SetTwitterOptions(WebApplicationBuilder app) =>
    CreateTwitterOptions(app.Configuration["Secrets:Twitter:ConsumerKey"]!, app.Configuration["Secrets:Twitter:ConsumerSecret"]!) with {
      CallbackPath = $"/accounts{TwitterDefaults.CallbackPath}",
      ChallengePath = $"/accounts{TwitterDefaults.ChallengePath}"
    };
}