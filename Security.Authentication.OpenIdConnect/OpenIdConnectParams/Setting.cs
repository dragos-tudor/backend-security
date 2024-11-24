
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  // static readonly string? Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

  // static void SetTelemetryOIDCParams(OidcParams authParams)
  // {
  //   SetOAuthParam(authParams, SkuTelemetry, IdentityModelTelemetryUtil.ClientSku);
  //   SetOAuthParam(authParams, VersionTelemetry, Version);
  // }

  // static OidcParams SetAuthorizationOAuthParams(
  //   OidcParams authParams,
  //   HttpContext context,
  //   AuthenticationProperties authProps,
  //   OpenIdConnectOptions oidcOptions,
  //   string state)
  // {
  //   SetOAuthParam(authParams, ClientId, oidcOptions.ClientId);
  //   SetOAuthParam(authParams, MaxAge, GetOidcParamMaxAge(authProps, oidcOptions));
  //   SetOAuthParam(authParams, RedirectUri, GetOidcParamRedirectUri(context, oidcOptions));

  //   SetOAuthParam(authParams, ResponseType, oidcOptions.ResponseType);
  //   SetOAuthParam(authParams, Prompt, GetOidcParamPrompt(authProps, oidcOptions));
  //   SetOAuthParam(authParams, Scope, GetOidcParamScope(authProps, oidcOptions));
  //   SetOAuthParam(authParams, State, state);
  //   SetOAuthParam(authParams, Resource, oidcOptions.Resource!);

  //   if(!oidcOptions.DisableTelemetry) SetTelemetryOIDCParams(authParams);
  //   if(IsResponseModeSettable(oidcOptions)) SetOAuthParam(authParams, ResponseMode, oidcOptions.ResponseMode);

  //   return authParams;
  // }

  // static OidcParams SetPostAuthorizationOAuthParams(
  //   OidcParams oidcParams,
  //   AuthenticationProperties authProps)
  // {
  //   SetOAuthParam(oidcParams, State, GetAuthPropsUserState(authProps)!);
  //   return oidcParams;
  // }

  // static async Task<OidcParams> SetSignoutChallengeOAuthParams(
  //   OidcParams oidcParams,
  //   HttpContext context,
  //   OpenIdConnectOptions oidcOptions,
  //   OpenIdConnectConfiguration oidcConfiguration,
  //   string state)
  // {
  //   SetOAuthParam(oidcParams, IdTokenHint, await GetOidcParamIdTokenHint(context, oidcOptions));
  //   SetOAuthParam(oidcParams, PostLogoutRedirectUri, GetAbsoluteUrl(context.Request, oidcOptions.CallbackSignOutPath));
  //   SetOAuthParam(oidcParams, State, state);

  //   if(!oidcOptions.DisableTelemetry) SetTelemetryOIDCParams(oidcParams);
  //   return oidcParams;
  // }
}