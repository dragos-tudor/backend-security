
using Microsoft.IdentityModel.Logging;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static void SetTelemetryOidcParams(OidcParams oidcParams)
  {
    SetOAuthParam(oidcParams, OidcParamNames.SkuTelemetry, IdentityModelTelemetryUtil.ClientSku);
    SetOAuthParam(oidcParams, OidcParamNames.VersionTelemetry, IdentityModelTelemetryUtil.ClientVer);
  }
}