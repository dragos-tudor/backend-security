using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> ChallengeSignOutOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    SetSignoutChallengeAuthProps(authProps, oidcOptions.CallbackSignOutPath);

    var oidcParams = CreateOidcParams();
    var state = ProtectAuthProps(authProps, authPropsProtector);
    var idTokenHint = await GetOidcParamIdTokenHint(context, oidcOptions);
    SetSignoutChallengeParams(oidcParams, context, oidcOptions, state, idTokenHint!);

    if (IsRedirectGetAuthMethod(oidcOptions))
      SetHttpResponseRedirect(context.Response, BuildHttpRequestUri(oidcOptions.ChallengeSignOutPath, oidcParams!));

    if (IsFormPostAuthMethod(oidcOptions))
    {
      ResetHttpResponseCacheHeaders(context.Response);
      await WriteHttpResponseTextContent(context.Response, BuildHttpRequestFormPost(oidcOptions.ChallengeSignOutPath, oidcParams!), context.RequestAborted);
    }

    LogSignOutChallenge(logger, oidcOptions.SchemeName, GetHttpResponseLocation(context.Response)!, context.TraceIdentifier);
    return oidcOptions.ChallengeSignOutPath;
  }
}