
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string NoCallbackRedirect = string.Empty;

  static string? GetCallbackAuthorizationState(HttpRequest request) => request.Query[OAuthParamNames.State];
}