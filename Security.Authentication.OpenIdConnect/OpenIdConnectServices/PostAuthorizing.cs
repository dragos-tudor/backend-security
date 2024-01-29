
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string NoMessage = "No message.";
  const string NoMessageState = "No message state.";
  const string InvalidMessageState = "Invalid message state.";
  const string UnexpectedParams = "An OpenID Connect response cannot contain an identity token or an access token when using query response mode";

  public static async Task<(AuthenticationProperties?, string?)> PostAuthorize<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat propertiesDataFormat)
  where TOptions : OpenIdConnectOptions
  {
    var authParams = await GetRequestParams(context.Request, context.RequestAborted);
    if (authParams is null) return (default, NoMessage);

    var authResponse = CreateCallbackOpenIdConnectMessage(authParams);
    if (IsGetRequest(context.Request) && !IsSafeAuthorizationResponse(authResponse)) return (default, UnexpectedParams);
    if (IsEmptyString(authResponse.State)) return (default, NoMessageState);

    var authError = ValidateAuthorizationResponse(authResponse);
    if (authError is not null) return (default, authError);

    var authProperties = UnprotectAuthenticationProperties(authResponse.State, propertiesDataFormat);
    if (authProperties is null) return (default, InvalidMessageState);

    ResetOpenIdConnectMessageState(authResponse, GetAuthenticationPropertiesUserState(authProperties)!);

    var validationCorrelationResult = ValidateCorrelationCookie(context.Request, authProperties);
    if (validationCorrelationResult is not null) return (default, validationCorrelationResult);

    SetAuthenticationPropertiesSession(authProperties, oidcOptions, authResponse);
    return (authProperties, default);

    // var idToken = authMessage.IdToken;
    // var (securityToken, validationException) = await ValidateIdToken(idToken, oidcConfiguration, tokenHandler, validationParameters);
    // if (validationException is not null)
    //   return Fail(validationException.Message);


    // if (oidcOptions.UseTokenLifetime)
    // {
    //   var issued = validatedToken.ValidFrom;
    //   if (issued != DateTime.MinValue)
    //     properties.IssuedUtc = issued;

    //   var expires = validatedToken.ValidTo;
    //   if (expires != DateTime.MinValue)
    //     properties.ExpiresUtc = expires;
    // }
  }

  public static Task<(AuthenticationProperties?, string?)> PostAuthorize<TOptions>(
    HttpContext context)
  where TOptions : OpenIdConnectOptions =>
      PostAuthorize(
        context,
        ResolveService<TOptions>(context),
        ResolveService<PropertiesDataFormat>(context)
      );
}