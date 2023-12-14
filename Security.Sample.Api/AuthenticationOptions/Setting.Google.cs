
namespace Security.Samples;

partial class SampleFuncs {

  static GoogleOptions SetGoogleOptions(WebApplicationBuilder app) =>
    CreateGoogleOptions(app.Configuration["google:clientid"]!, app.Configuration["google:clientsecret"]!);

}