using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static TokenValidationParameters CreateTokenValidationParameters(OpenIdConnectOptions oidcOptions) =>
    new () { ValidAudience = oidcOptions.ClientId };
}