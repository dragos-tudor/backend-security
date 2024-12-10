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
  const string MissingAccessToken = "AccessToken is null. There is no 'access_token' to validate against.";
  const string DifferentIdTokenAzpAndClientId = "The 'id_token' contains 'azp' claim but its value is not equal to Client Id. 'azp': '{0}'. clientId: '{1}'.";
  const string CHashValidationFailure = "Validating the 'c_hash' failed, error {0}";
  const string AtHashValidationFailure = "Validating the 'at_hash' failed, error {0}";
  const string MissingJwtTokenHeaderAlgorithm = "The algorithm specified in the jwt header is null or empty.";

  static string? ValidateTokenResponse(HttpResponseMessage response)
  {
    var contentType = GetHttpResponseContentType(response);
    var statusCode = GetHttpResponseStatusCode(response);

    if (IsEmptyString(contentType))
      return MissingContentTypeHeader.Format(statusCode);

    if (!IsJsonContentTypeHttpResponse(contentType!))
      return UnexpectedTokenResponseContentType.Format(contentType!, statusCode);

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

  public static string? ValidateOidcTokens(OidcTokens oidcTokens)
  {
    if (IsEmptyString(oidcTokens.IdToken)) return MissingIdToken;
    if (IsEmptyString(oidcTokens.AccessToken)) return MissingAccessToken;
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
        return InvalidIdToken.Format(idToken, idTokenError);

      return default;
    }

    // required claims
    if (idToken.Payload.Aud.Count == 0)
      return MissingIdTokenClaim.Format(JwtRegisteredClaimNames.Aud, idToken);

    if (!idToken.Payload.Expiration.HasValue)
      return MissingIdTokenClaim.Format(JwtRegisteredClaimNames.Exp, idToken);

    if (idToken.Payload.IssuedAt.Equals(DateTime.MinValue))
      return MissingIdTokenClaim.Format(JwtRegisteredClaimNames.Iat, idToken);

    if (idToken.Payload.Iss == null)
      return MissingIdTokenClaim.Format(JwtRegisteredClaimNames.Iss, idToken);

    if (validationOptions.RequireSub && string.IsNullOrWhiteSpace(idToken.Payload.Sub))
      return MissingIdTokenClaim.Format(JwtRegisteredClaimNames.Sub, idToken);

    // optional claims
    if (validationOptions.RequireAcr && string.IsNullOrWhiteSpace(idToken.Payload.Acr))
      return MissingIdTokenAcr.Format(idToken);

    if (validationOptions.RequireAmr && idToken.Payload.Amr.Count == 0)
      return MissingIdTokenAmr.Format(idToken);

    if (validationOptions.RequireAuthTime && !idToken.Payload.AuthTime.HasValue)
      return MissingIdTokenAuthTime.Format(idToken);

    if (validationOptions.RequireAzp && string.IsNullOrWhiteSpace(idToken.Payload.Azp))
      return MissingIdTokenAzp.Format(idToken);

    // if 'azp' claim exist, it should be equal to 'client_id' of the application
    if (!string.IsNullOrEmpty(idToken.Payload.Azp))
    {
      if (!EqualsStringOrdinal(idToken.Payload.Azp, clientId))
        return DifferentIdTokenAzpAndClientId.Format(idToken.Payload.Azp, clientId);
    }
    return default;
  }

  static string? ValidateCHash(
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    string code)
  {
    if (!idToken.Payload.TryGetValue(JwtRegisteredClaimNames.CHash, out object? cHashClaim))
      return MissingIdTokenCHash.Format(idToken);

    if (cHashClaim is not string cHash)
      return NotStringIdTokenCHash.Format(idToken);

    var algorithm = GetJwtTokenAlgorithm(idToken);
    if (IsEmptyString(algorithm))
      return MissingJwtTokenHeaderAlgorithm;

    if (ValidateHash(validationOptions, cHash!, code, algorithm) is string hashError)
      return CHashValidationFailure.Format(hashError);

    return default;
  }

  static string? ValidateAtHash(
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    string accessToken)
  {
    if (!idToken.Payload.TryGetValue(JwtRegisteredClaimNames.AtHash, out object? atHashClaim))
      return MissingIdTokenAtHash.Format(idToken);

    if (atHashClaim is not string atHash)
      return NotStringIdTokenAtHash.Format(idToken);

    var algorithm = GetJwtTokenAlgorithm(idToken);
    if (IsEmptyString(algorithm))
      return MissingJwtTokenHeaderAlgorithm;

    if (ValidateHash(validationOptions, atHash!, accessToken, algorithm) is string hashError)
      return AtHashValidationFailure.Format(hashError);

    return default;
  }
}