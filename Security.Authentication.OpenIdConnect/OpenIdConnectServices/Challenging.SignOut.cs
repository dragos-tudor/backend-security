using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<bool> ChallengeSignOut<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var challangeRequest = await GetHttpRequestParams(context.Request, context.RequestAborted);
    if (challangeRequest is null) return false;

    var principal = GetContextUser(context);
    var challengeData = ToOpenIdConnectData(challangeRequest);
    var validationMsg = ValidateSignoutRequest(challengeData, oidcOptions, principal);
    if (validationMsg is not null) {
      LogSkipSignOutChallenge(logger, oidcOptions.SchemeName, validationMsg, context.TraceIdentifier);
      return false;
    }

    SetChallengeSignoutAuthProps(authProps, oidcOptions.CallbackSignOutPath);
    var state = ProtectAuthProps(authProps, authPropsProtector);
    var idTokenHint = await GetOidcParamIdTokenHint(context, oidcOptions);
    var oidcParams = SetChallengeSignoutParams(CreateOidcParams(), context, oidcOptions, state, idTokenHint!);

    if (IsRedirectGetAuthMethod(oidcOptions))
      SetHttpResponseRedirect(context.Response, BuildHttpRequestUri(oidcOptions.SignOutPath!, oidcParams!));

    if (IsFormPostAuthMethod(oidcOptions))
    {
      ResetHttpResponseCacheHeaders(context.Response);
      await WriteHttpResponseTextContent(context.Response, BuildHttpRequestFormPost(oidcOptions.SignOutPath!, oidcParams!), context.RequestAborted);
    }

    LogSignOutChallenge(logger, oidcOptions.SchemeName, GetHttpResponseLocation(context.Response)!, context.TraceIdentifier);
    return true;
  }
}