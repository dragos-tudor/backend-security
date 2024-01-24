using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async ValueTask<string?> ChallengeRemoteOidc(
    HttpContext context,
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    NonceCookieBuilder nonceCookieBuilder,
    PropertiesDataFormat propertiesDataFormat,
    StringDataFormat stringDataFormat,
    OpenIdConnectProtocolValidator protocolValidator,
    DateTimeOffset currentUtc)
  {
    var authRequest = CreateChallengeOpenIdConnectMessage(context, authProperties, oidcOptions, oidcConfiguration);

    UseCorrelationCookie(context, GenerateCorrelationId(), oidcOptions, currentUtc);

    if (ShouldUseNonceCookie(oidcOptions))
      UseNonceCookie(context, nonceCookieBuilder, stringDataFormat, protocolValidator, currentUtc);

    SetChallengeAuthenticationProperties(authProperties, authRequest, GetRequestUrl(context.Request));
    if (ShouldUseCodeChallenge(oidcOptions))
      SetAuthenticationPropertiesCodeVerifier(authProperties, GenerateCodeVerifier());

    SetChallengeOpenIdConnectMessage(authRequest, authProperties, oidcOptions, propertiesDataFormat);
    if (ShouldUseCodeChallenge(oidcOptions))
      SetAuthorizationParamsCodeChallenge(authRequest.Parameters, authProperties);

    if (IsRedirectGetOpenIdConnectAuthenticationMethod(oidcOptions))
      SetResponseRedirect(context.Response, authRequest.CreateAuthenticationRequestUrl());

    if (IsFormPostOpenIdConnectAuthenticationMethod(oidcOptions))
    {
      ResetResponseCacheHeaders(context.Response);
      await WriteResponseHtmlContent(context.Response, authRequest.BuildFormPost());
    }

    SanitizeChallengeResponse(context.Response);
    var authUri = GetAuthorizationUri(context.Response, oidcConfiguration);

    LogChallengedRemote(Logger, oidcOptions.SchemeName, authUri, context.TraceIdentifier);
    return authUri;
  }

  public static ValueTask<string?> ChallengeRemoteOidc(
    HttpContext context,
    AuthenticationProperties authProperties) =>
      ChallengeRemoteOidc(
        context,
        authProperties,
        ResolveService<OpenIdConnectOptions>(context),
        ResolveService<OpenIdConnectConfiguration>(context),
        ResolveService<NonceCookieBuilder>(context) ?? new NonceCookieBuilder(ResolveService<OpenIdConnectOptions>(context)),
        ResolveService<PropertiesDataFormat>(context),
        ResolveService<StringDataFormat>(context),
        ResolveService<OpenIdConnectProtocolValidator>(context) ?? new OpenIdConnectProtocolValidator(),
        ResolveService<TimeProvider>(context).GetUtcNow()
      );
}