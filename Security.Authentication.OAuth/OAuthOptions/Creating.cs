
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static TOptions CreateOAuthOptions<TOptions> (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = "OAuth") where TOptions: OAuthOptions, new() =>
      new () {
        ReturnUrlParameter = "ReturnUrl",
        RemoteAuthenticationTimeout = TimeSpan.FromMinutes(15),
        RemoteClient = ConfigureRemoteClient(CreateRemoteClient(), schemeName!),
        SaveTokens = false,
        SchemeName = schemeName!,
        ScopeSeparator = ' ',
        StateDataFormat = CreateStateDataFormat(dataProtectionProvider, schemeName),
        UsePkce = false
      };

}
