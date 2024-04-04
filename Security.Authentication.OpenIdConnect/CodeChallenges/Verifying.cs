namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool ShouldUseCodeChallenge(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.UsePkce && IsCodeOpenIdConnectResponseType(oidcOptions);
}