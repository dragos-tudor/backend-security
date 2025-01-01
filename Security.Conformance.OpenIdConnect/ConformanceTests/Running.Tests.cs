
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Security.Authentication.OpenIdConnect;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdConnectTests
{
  static readonly TimeSpan ConformanceTimeout = TimeSpan.FromMinutes(1);

  [TestMethod]
  [Ignore]
  public async Task Run_conformance_tests()
  {
    // iterate test plans
    // iterate test modules
    // create test
    // get tets oidc configuration [OpenIdConnectConfigurationRetriever]
    // start test
    // create openidconnect options
    // redirect challenge oidc response to authorization endpoint
    // redirect authorization response to callback oidc
    // get test details [check for errors]?
    // cancel test [finally]

    await Task.CompletedTask;
  }

  [TestMethod]
  public async Task Run_conformance_template_test()
  {
    using var cancellationTokenSource = new CancellationTokenSource(ConformanceTimeout);
    var cancellationToken = cancellationTokenSource.Token;

    var testPlansPage = new PaginationRequest() { Length = 10, Start = 0 };
    var testPlans = await GetTestPlansForCurrentUserAsync(ApiClient, testPlansPage, false, cancellationToken);
    var testPlan = testPlans.Plans.First();
    var testModule = testPlan.TestModules.First();
    var testInstance = await CreateTestInstanceAsync(ApiClient, testPlan.PlanId, testModule.ModuleName, testModule.ModuleVariant, cancellationToken);
    var testConfiguration = await OpenIdConnectConfigurationRetriever.GetAsync(testInstance.Url, cancellationToken);

    try
    {
      await StartTestInstanceAsync(ApiClient, testInstance.InstanceId, cancellationToken);
      var challengeContext = CreateHttpContext();
      var oidcOptions = CreateOpenIdConnectOptions(testConfiguration, default!, default!);
      var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(challengeContext));
      var authorizationPath = ChallengeOidc(challengeContext, CreateAuthProps(), oidcOptions, DateTimeOffset.Now, authPropsProtector, NullLogger.Instance);
      var authorizationUrl = GetHttpResponseLocation(challengeContext.Response); // TODO: location or body form based on oidcOptions.AuthenticationMethod


      var callbackUrl = await HttpClient.GetAsync(authorizationUrl);
      var callbackRequest = GetResponseMessageLocation(callbackUrl);
      var callbackContext = CreateHttpContext();
      await CallbackOidc<OpenIdConnectOptions>(callbackContext, (_) => default!, (_, _, _) => default);
      var testInfo = await GetTestInstanceInfoAsync(ApiClient, testInstance.InstanceId, false, cancellationToken);
      Assert.IsNull(testInfo.Error);
    }
    finally
    {
      await CancelTestInstanceAsync(ApiClient, testInstance.InstanceId, cancellationToken);
    }
  }
}