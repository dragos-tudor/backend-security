using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static AuthenticationProperties? UnprotectAuthenticationProperties(
    string state,
    PropertiesDataFormat propertiesDataFormat) =>
      propertiesDataFormat.Unprotect(state);
}