using System.Net.Http;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string WellKnownPath = ".well-known/openid-configuration";

  static ConfigurationManager<OpenIdConnectConfiguration> CreateConfigurationManager(ConfigurationManagerOptions options, HttpClient remoteClient) =>
    new (
      $"{options.MetadataAddress}{WellKnownPath}",
      new OpenIdConnectConfigurationRetriever(),
      new HttpDocumentRetriever(remoteClient) { RequireHttps = true })
    {
      RefreshInterval = options.RefreshInterval,
      AutomaticRefreshInterval = options.AutomaticRefreshInterval,
    };
}