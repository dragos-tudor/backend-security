using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string MissingContentTypeHeader = "Unexpected token response format. Content-Type header is missing. Status Code: {0}";
  const string UnexpectedTokenResponseContentType = "Unexpected token response format. Content-Type {0}. Status Code: {1}";
  const string NotStringIdTokenCHash = "The 'c_hash' claim was not a string in the 'id_token', but a 'code' was in the response, 'id_token': '{0}'.";
  const string MissingIdTokenCHash = "The 'c_hash' claim was not found in the id_token, but a 'code' was in the response, id_token: '{0}'";
  const string NotStringIdTokenAtHash = "The 'at_hash' claim was not a string in the 'id_token', but an 'access_token' was in the response, 'id_token': '{0}'.";
  const string MissingIdTokenAtHash = "The 'at_hash' claim was not found in the 'id_token', but a 'access_token' was in the response, 'id_token': '{0}'.";
  const string InvalidIdToken = "The id_token: '{0}' is not valid. error {1}";
  const string MissingIdTokenClaim = "OpenIdConnect protocol validator requires the jwt token to have an '{0}' claim. The jwt did not contain an '{0}' claim, jwt: '{1}'.";
  const string MissingIdTokenAcr = "RequireAcr is 'true' (default is 'false') but jwt.PayLoad.Acr is 'null or whitespace', jwt: '{0}'.";
  const string MissingIdTokenAmr = "RequireAmr is 'true' (default is 'false') but jwt.PayLoad.Amr is 'null or whitespace', jwt: '{0}'.";
  const string MissingIdTokenAuthTime = "RequireAuthTime is 'true' (default is 'false') but jwt.PayLoad.AuthTime is 'null or whitespace', jwt: '{0}'.";
  const string MissingIdTokenAzp = "RequireAzp is 'true' (default is 'false') but jwt.PayLoad.Azp is 'null or whitespace', jwt: '{0}'.";
  const string MissingIdToken = "IdToken is null. There is no 'id_token' to validate against.";
  const string MissingIdTokenAndAccessToken = "Both 'id_token' and 'access_token' should be present in the response received from Token Endpoint";
  const string DifferentIdTokenAzpAndClientId = "The 'id_token' contains 'azp' claim but its value is not equal to Client Id. 'azp': '{0}'. clientId: '{1}'.";
  const string CHashValidationFailure = "Validating the 'c_hash' failed, error {0}";
  const string AtHashValidationFailure = "Validating the 'at_hash' failed, error {0}";
  const string MissingJwtTokenHeaderAlgorithm = "The algorithm specified in the jwt header is null or empty.";

  static string? ValidateTokenResponse(HttpResponseMessage response)
  {
    var contentType = GetHttpResponseContentType(response);
    var statusCode = GetHttpResponseStatusCode(response);

    if (IsEmptyString(contentType))
      return FormatString(MissingContentTypeHeader, statusCode);

    if (!IsJsonContentTypeHttpResponse(contentType!))
      return FormatString(UnexpectedTokenResponseContentType, contentType!, statusCode);

    return default;
  }

  public static string? ValidateIdToken(
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    string accessToken,
    string clientId,
    string code)
  {
    if (ValidateIdToken(validationOptions, idToken, clientId) is string idTokenError)
      return idTokenError;

    if (ValidateAtHash(validationOptions, idToken, accessToken!) is string atHashError)
      return atHashError;

    if (ValidateCHash(validationOptions, idToken, code!) is string cHashError)
      return cHashError;

    return default;
  }

  public static Task<TokenValidationResult> ValidateSecurityIdToken<TOptions>(TOptions oidcOptions, string? idToken) where TOptions : OpenIdConnectOptions =>
    oidcOptions.TokenHandler.ValidateTokenAsync(idToken, oidcOptions.TokenValidationParameters);

  static string? ValidateIdToken(
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    string clientId)
  {
    // TODO: skip logging PII
    if (validationOptions.IdTokenValidator != null)
    {
      if (validationOptions.IdTokenValidator(validationOptions, idToken) is string idTokenError)
        return FormatString(InvalidIdToken, idToken, idTokenError);

      return default;
    }

    // required claims
    if (idToken.Payload.Aud.Count == 0)
      return FormatString(MissingIdTokenClaim, JwtRegisteredClaimNames.Aud, idToken);

    if (!idToken.Payload.Expiration.HasValue)
      return FormatString(MissingIdTokenClaim, JwtRegisteredClaimNames.Exp, idToken);

    if (idToken.Payload.IssuedAt.Equals(DateTime.MinValue))
      return FormatString(MissingIdTokenClaim, JwtRegisteredClaimNames.Iat, idToken);

    if (idToken.Payload.Iss == null)
      return FormatString(MissingIdTokenClaim, JwtRegisteredClaimNames.Iss, idToken);

    if (validationOptions.RequireSub && string.IsNullOrWhiteSpace(idToken.Payload.Sub))
      return FormatString(MissingIdTokenClaim, JwtRegisteredClaimNames.Sub, idToken);

    // optional claims
    if (validationOptions.RequireAcr && string.IsNullOrWhiteSpace(idToken.Payload.Acr))
      return FormatString(MissingIdTokenAcr, idToken);

    if (validationOptions.RequireAmr && idToken.Payload.Amr.Count == 0)
      return FormatString(MissingIdTokenAmr, idToken);

    if (validationOptions.RequireAuthTime && !idToken.Payload.AuthTime.HasValue)
      return FormatString(MissingIdTokenAuthTime, idToken);

    if (validationOptions.RequireAzp && string.IsNullOrWhiteSpace(idToken.Payload.Azp))
      return FormatString(MissingIdTokenAzp, idToken);

    // if 'azp' claim exist, it should be equal to 'client_id' of the application
    if (!string.IsNullOrEmpty(idToken.Payload.Azp))
    {
      if (!EqualsStringOrdinal(idToken.Payload.Azp, clientId))
        return FormatString(DifferentIdTokenAzpAndClientId, idToken.Payload.Azp, clientId);
    }
    return default;
  }

  static string? ValidateCHash(
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    string code)
  {
    if (!idToken.Payload.TryGetValue(JwtRegisteredClaimNames.CHash, out object? cHashClaim))
      return FormatString(MissingIdTokenCHash, idToken);

    if (cHashClaim is not string cHash)
      return FormatString(NotStringIdTokenCHash, idToken);

    var algorithm = GetJwtTokenAlgorithm(idToken);
    if (IsEmptyString(algorithm))
      return MissingJwtTokenHeaderAlgorithm;

    if (ValidateHash(validationOptions, cHash!, code, algorithm) is string hashError)
      return FormatString(CHashValidationFailure, hashError);

    return default;
  }

  static string? ValidateAtHash(
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    string accessToken)
  {
    if (!idToken.Payload.TryGetValue(JwtRegisteredClaimNames.AtHash, out object? atHashClaim))
      return FormatString(MissingIdTokenAtHash, idToken);

    if (atHashClaim is not string atHash)
      return FormatString(NotStringIdTokenAtHash, idToken);

    var algorithm = GetJwtTokenAlgorithm(idToken);
    if (IsEmptyString(algorithm))
      return MissingJwtTokenHeaderAlgorithm;

    if (ValidateHash(validationOptions, atHash!, accessToken, algorithm) is string hashError)
      return FormatString(AtHashValidationFailure, hashError);

    return default;
  }
}