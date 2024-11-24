// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? ValidateAuthenticationResponse(OpenIdConnectValidationOptions validationOptions, string? code, string? state) =>
    IsEmptyString(code) ? ValidationMessages.IDX21305 : ValidateState(validationOptions, state);

  /// <summary>
  /// Validates that an OpenID Connect response from "token_endpoint" is valid as per <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="validationContext">the <see cref="OpenIdConnectProtocolValidationContext"/> that contains expected values.</param>
  /// <exception cref="ArgumentNullException">Thrown if <paramref name="validationContext"/> is null.</exception>
  /// <exception cref="OpenIdConnectProtocolException">Thrown if the response is not spec compliant.</exception>
  /// <remarks>It is assumed that the IdToken had ('aud', 'iss', 'signature', 'lifetime') validated.</remarks>
  static void ValidateTokenResponse(OpenIdConnectProtocolValidationContext validationContext)
  {
    if (validationContext == null)
      throw LogHelper.LogArgumentNullException(nameof(validationContext));

    // no 'response' is recieved
    if (validationContext.ProtocolMessage == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21333));

    // both 'id_token' and 'access_token' are required
    if (string.IsNullOrEmpty(validationContext.ProtocolMessage.IdToken) || string.IsNullOrEmpty(validationContext.ProtocolMessage.AccessToken))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21336));

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21332));

    ValidateIdToken(validationContext);

    // only if 'at_hash' claim exist. 'at_hash' is not required in token response.
    object atHashClaim;
    if (validationContext.ValidatedIdToken.Payload.TryGetValue(JwtRegisteredClaimNames.AtHash, out atHashClaim))
    {
      ValidateAtHash(validationContext);
    }

  }

  /// <summary>
  /// Validates that an OpenIdConnect response from "useinfo_endpoint" is valid as per <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="validationContext">the <see cref="OpenIdConnectProtocolValidationContext"/> that contains expected values.</param>
  /// <exception cref="ArgumentNullException">Thrown if <paramref name="validationContext"/> is null.</exception>
  /// <exception cref="OpenIdConnectProtocolException">Thrown if the response is not spec compliant.</exception>
  static void ValidateUserInfoResponse(OpenIdConnectProtocolValidationContext validationContext)
  {
    if (validationContext == null)
      throw LogHelper.LogArgumentNullException(nameof(validationContext));

    if (string.IsNullOrEmpty(validationContext.UserInfoEndpointResponse))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21337));

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21332));

    string sub = string.Empty;
    try
    {
      // if user info response is a jwt token
      var handler = new JwtSecurityTokenHandler();
      if (handler.CanReadToken(validationContext.UserInfoEndpointResponse))
      {
        var token = handler.ReadToken(validationContext.UserInfoEndpointResponse) as JwtSecurityToken;
        sub = token.Payload.Sub;
      }
      else
      {
        // if the response is not a jwt, it should be json
        var payload = JwtPayload.Deserialize(validationContext.UserInfoEndpointResponse);
        sub = payload.Sub;
      }
    }
    catch (Exception ex)
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21343, validationContext.UserInfoEndpointResponse), ex));
    }

    if (string.IsNullOrEmpty(sub))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21345));

    if (string.IsNullOrEmpty(validationContext.ValidatedIdToken.Payload.Sub))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21346));

    if (!string.Equals(validationContext.ValidatedIdToken.Payload.Sub, sub))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21338, validationContext.ValidatedIdToken.Payload.Sub, sub)));
  }

  /// <summary>
  /// Validates the claims in the 'id_token' as per <see href="https://openid.net/specs/openid-connect-core-1_0.html#IDTokenValidation"/>.
  /// </summary>
  /// <param name="validationContext">the <see cref="OpenIdConnectProtocolValidationContext"/> that contains expected values.</param>
  static void ValidateIdToken(OpenIdConnectProtocolValidationContext validationContext)
  {
    if (validationContext == null)
      throw LogHelper.LogArgumentNullException("validationContext");

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogArgumentNullException("validationContext.ValidatedIdToken");

    // if user sets the custom validator, we call the delegate. The default checks for multiple audiences and azp are not executed.
    if (this.IdTokenValidator != null)
    {
      try
      {
        this.IdTokenValidator(validationContext.ValidatedIdToken, validationContext);
      }
      catch (Exception ex)
      {
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21313, validationContext.ValidatedIdToken), ex));
      }
      return;
    }
    else
    {
      JwtSecurityToken idToken = validationContext.ValidatedIdToken;

      // required claims
      if (idToken.Payload.Aud.Count == 0)
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Aud.ToLowerInvariant()), idToken)));

      if (!idToken.Payload.Expiration.HasValue)
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Exp.ToLowerInvariant()), idToken)));

      if (idToken.Payload.IssuedAt.Equals(DateTime.MinValue))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Iat.ToLowerInvariant()), idToken)));

      if (idToken.Payload.Iss == null)
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Iss.ToLowerInvariant()), idToken)));

      // sub is required in OpenID spec; but we don't want to block valid idTokens provided by some identity providers
      if (RequireSub && (string.IsNullOrWhiteSpace(idToken.Payload.Sub)))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Sub.ToLowerInvariant()), idToken)));

      // optional claims
      if (RequireAcr && string.IsNullOrWhiteSpace(idToken.Payload.Acr))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21315, idToken)));

      if (RequireAmr && idToken.Payload.Amr.Count == 0)
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21316, idToken)));

      if (RequireAuthTime && !(idToken.Payload.AuthTime.HasValue))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21317, idToken)));

      if (RequireAzp && string.IsNullOrWhiteSpace(idToken.Payload.Azp))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21318, idToken)));

      // if multiple audiences are present in the id_token, 'azp' claim should be present
      if (idToken.Payload.Aud.Count > 1 && string.IsNullOrEmpty(idToken.Payload.Azp))
      {
        LogHelper.LogWarning(ValidationMessages.IDX21339);
      }

      // if 'azp' claim exist, it should be equal to 'client_id' of the application
      if (!string.IsNullOrEmpty(idToken.Payload.Azp))
      {
        if (string.IsNullOrEmpty(validationContext.ClientId))
        {
          throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21308));
        }
        else if (!string.Equals(idToken.Payload.Azp, validationContext.ClientId))
        {
          throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21340, idToken.Payload.Azp, validationContext.ClientId)));
        }
      }
    }
  }

  /// <summary>
  /// Returns a <see cref="HashAlgorithm"/> corresponding to string 'algorithm' after translation using <see cref="HashAlgorithmMap"/>.
  /// </summary>
  /// <param name="algorithm">string representing the hash algorithm</param>
  /// <returns>A <see cref="HashAlgorithm"/>.</returns>
  static HashAlgorithm GetHashAlgorithm(string algorithm)
  {
    if (string.IsNullOrEmpty(algorithm))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21350));

    try
    {
      if (!HashAlgorithmMap.TryGetValue(algorithm, out string hashAlgorithm))
        hashAlgorithm = algorithm;

      return CryptoProviderFactory.CreateHashAlgorithm(hashAlgorithm);
    }
    catch (Exception ex)
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21301, LogHelper.MarkAsNonPII(algorithm), LogHelper.MarkAsNonPII(typeof(HashAlgorithm))), ex));
    }

    throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21302, LogHelper.MarkAsNonPII(algorithm))));
  }

  /// <summary>
  /// Validates the 'token' or 'code'. See: <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="expectedValue">The expected value of the hash. normally the c_hash or at_hash claim.</param>
  /// <param name="hashItem">Item to be hashed per oidc spec.</param>
  /// <param name="algorithm">Algorithm for computing hash over hashItem.</param>
  /// <exception cref="OpenIdConnectProtocolException">Thrown if the expected value does not equal the hashed value.</exception>
  static void ValidateHash(string expectedValue, string hashItem, string algorithm)
  {
    if (LogHelper.IsEnabled(EventLogLevel.Informational))
      LogHelper.LogInformation(ValidationMessages.IDX21303, expectedValue);

    HashAlgorithm hashAlgorithm = null;
    try
    {
      hashAlgorithm = GetHashAlgorithm(algorithm);
      CheckHash(hashAlgorithm, expectedValue, hashItem, algorithm);
    }
    finally
    {
      CryptoProviderFactory.ReleaseHashAlgorithm(hashAlgorithm);
    }
  }

  static void CheckHash(HashAlgorithm hashAlgorithm, string expectedValue, string hashItem, string algorithm)
  {
    var hashBytes = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(hashItem));
    var hashString = Base64UrlEncoder.Encode(hashBytes, 0, hashBytes.Length / 2);
    if (!string.Equals(expectedValue, hashString))
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21300, expectedValue, hashItem, LogHelper.MarkAsNonPII(algorithm))));
    }
  }

  /// <summary>
  /// Validates the 'code' according to <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="validationContext">A <see cref="OpenIdConnectProtocolValidationContext"/> that contains the protocol message to validate.</param>
  /// <exception cref="ArgumentNullException">Thrown if <paramref name="validationContext"/> is null.</exception>
  /// <exception cref="ArgumentNullException">Thrown if <see cref="OpenIdConnectProtocolValidationContext.ValidatedIdToken"/> is null.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidCHashException">Thrown if <paramref name="validationContext"/> contains a 'code' and there is no 'c_hash' claim in the 'id_token'.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidCHashException">Thrown if <paramref name="validationContext"/> contains a 'code' and the 'c_hash' claim is not a string in the 'id_token'.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidCHashException">Thrown if the 'c_hash' claim in the 'id_token' does not correspond to the 'code' in the <see cref="OpenIdConnectMessage"/> response.</exception>
  static void ValidateCHash(OpenIdConnectProtocolValidationContext validationContext)
  {
    LogHelper.LogVerbose(ValidationMessages.IDX21304);

    if (validationContext == null)
      throw LogHelper.LogArgumentNullException(nameof(validationContext));

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogArgumentNullException(nameof(validationContext.ValidatedIdToken));

    if (validationContext.ProtocolMessage == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21333));

    if (string.IsNullOrEmpty(validationContext.ProtocolMessage.Code))
    {
      LogHelper.LogInformation(ValidationMessages.IDX21305);
      return;
    }

    object cHashClaim;
    if (!validationContext.ValidatedIdToken.Payload.TryGetValue(JwtRegisteredClaimNames.CHash, out cHashClaim))
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidCHashException(LogHelper.FormatInvariant(ValidationMessages.IDX21307, validationContext.ValidatedIdToken)));
    }

    var chash = cHashClaim as string;
    if (chash == null)
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidCHashException(LogHelper.FormatInvariant(ValidationMessages.IDX21306, validationContext.ValidatedIdToken)));
    }

    var idToken = validationContext.ValidatedIdToken;

    var alg = idToken.InnerToken != null ? idToken.InnerToken.Header.Alg : idToken.Header.Alg;

    try
    {
      ValidateHash(chash, validationContext.ProtocolMessage.Code, alg);
    }
    catch (OpenIdConnectProtocolException ex)
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidCHashException(ValidationMessages.IDX21347, ex));
    }
  }

  /// <summary>
  /// Validates the 'token' according to <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="validationContext">A <see cref="OpenIdConnectProtocolValidationContext"/> that contains the protocol message to validate.</param>
  /// <exception cref="ArgumentNullException">Thrown if <paramref name="validationContext"/> is null.</exception>
  /// <exception cref="ArgumentNullException">Thrown if <see cref="OpenIdConnectProtocolValidationContext.ValidatedIdToken"/> is null.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidAtHashException">Thrown if the <paramref name="validationContext"/> contains a 'token' and there is no 'at_hash' claim in the id_token.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidAtHashException">Thrown if the <paramref name="validationContext"/> contains a 'token' and the 'at_hash' claim is not a string in the 'id_token'.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidAtHashException">Thrown if the 'at_hash' claim in the 'id_token' does not correspond to the 'access_token' in the <see cref="OpenIdConnectMessage"/> response.</exception>
  static void ValidateAtHash(OpenIdConnectProtocolValidationContext validationContext)
  {
    LogHelper.LogVerbos  public virtual string? ValidateAuthenticationResponse(OpenIdConnectValidationOptions validator, string? code, string? state) =>
    IsEmptyString(code) ? ValidationMessages.IDX21305 : ValidateState(valOptions, state);

  /// <summary>
  /// Validates that an OpenID Connect response from "token_endpoint" is valid as per <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="validationContext">the <see cref="OpenIdConnectProtocolValidationContext"/> that contains expected values.</param>
  /// <exception cref="ArgumentNullException">Thrown if <paramref name="validationContext"/> is null.</exception>
  /// <exception cref="OpenIdConnectProtocolException">Thrown if the response is not spec compliant.</exception>
  /// <remarks>It is assumed that the IdToken had ('aud', 'iss', 'signature', 'lifetime') validated.</remarks>
  static void ValidateTokenResponse(OpenIdConnectProtocolValidationContext validationContext)
  {
    if (validationContext == null)
      throw LogHelper.LogArgumentNullException(nameof(validationContext));

    // no 'response' is recieved
    if (validationContext.ProtocolMessage == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21333));

    // both 'id_token' and 'access_token' are required
    if (string.IsNullOrEmpty(validationContext.ProtocolMessage.IdToken) || string.IsNullOrEmpty(validationContext.ProtocolMessage.AccessToken))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21336));

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21332));

    ValidateIdToken(validationContext);
    ValidateNonce(validationContext);

    // only if 'at_hash' claim exist. 'at_hash' is not required in token response.
    object atHashClaim;
    if (validationContext.ValidatedIdToken.Payload.TryGetValue(JwtRegisteredClaimNames.AtHash, out atHashClaim))
    {
      ValidateAtHash(validationContext);
    }

  }

  /// <summary>
  /// Validates that an OpenIdConnect response from "useinfo_endpoint" is valid as per <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="validationContext">the <see cref="OpenIdConnectProtocolValidationContext"/> that contains expected values.</param>
  /// <exception cref="ArgumentNullException">Thrown if <paramref name="validationContext"/> is null.</exception>
  /// <exception cref="OpenIdConnectProtocolException">Thrown if the response is not spec compliant.</exception>
  static void ValidateUserInfoResponse(OpenIdConnectProtocolValidationContext validationContext)
  {
    if (validationContext == null)
      throw LogHelper.LogArgumentNullException(nameof(validationContext));

    if (string.IsNullOrEmpty(validationContext.UserInfoEndpointResponse))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21337));

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21332));

    string sub = string.Empty;
    try
    {
      // if user info response is a jwt token
      var handler = new JwtSecurityTokenHandler();
      if (handler.CanReadToken(validationContext.UserInfoEndpointResponse))
      {
        var token = handler.ReadToken(validationContext.UserInfoEndpointResponse) as JwtSecurityToken;
        sub = token.Payload.Sub;
      }
      else
      {
        // if the response is not a jwt, it should be json
        var payload = JwtPayload.Deserialize(validationContext.UserInfoEndpointResponse);
        sub = payload.Sub;
      }
    }
    catch (Exception ex)
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21343, validationContext.UserInfoEndpointResponse), ex));
    }

    if (string.IsNullOrEmpty(sub))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21345));

    if (string.IsNullOrEmpty(validationContext.ValidatedIdToken.Payload.Sub))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21346));

    if (!string.Equals(validationContext.ValidatedIdToken.Payload.Sub, sub))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21338, validationContext.ValidatedIdToken.Payload.Sub, sub)));
  }

  /// <summary>
  /// Validates the claims in the 'id_token' as per <see href="https://openid.net/specs/openid-connect-core-1_0.html#IDTokenValidation"/>.
  /// </summary>
  /// <param name="validationContext">the <see cref="OpenIdConnectProtocolValidationContext"/> that contains expected values.</param>
  static void ValidateIdToken(OpenIdConnectProtocolValidationContext validationContext)
  {
    if (validationContext == null)
      throw LogHelper.LogArgumentNullException("validationContext");

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogArgumentNullException("validationContext.ValidatedIdToken");

    // if user sets the custom validator, we call the delegate. The default checks for multiple audiences and azp are not executed.
    if (this.IdTokenValidator != null)
    {
      try
      {
        this.IdTokenValidator(validationContext.ValidatedIdToken, validationContext);
      }
      catch (Exception ex)
      {
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21313, validationContext.ValidatedIdToken), ex));
      }
      return;
    }
    else
    {
      JwtSecurityToken idToken = validationContext.ValidatedIdToken;

      // required claims
      if (idToken.Payload.Aud.Count == 0)
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Aud.ToLowerInvariant()), idToken)));

      if (!idToken.Payload.Expiration.HasValue)
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Exp.ToLowerInvariant()), idToken)));

      if (idToken.Payload.IssuedAt.Equals(DateTime.MinValue))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Iat.ToLowerInvariant()), idToken)));

      if (idToken.Payload.Iss == null)
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Iss.ToLowerInvariant()), idToken)));

      // sub is required in OpenID spec; but we don't want to block valid idTokens provided by some identity providers
      if (RequireSub && (string.IsNullOrWhiteSpace(idToken.Payload.Sub)))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21314, LogHelper.MarkAsNonPII(JwtRegisteredClaimNames.Sub.ToLowerInvariant()), idToken)));

      // optional claims
      if (RequireAcr && string.IsNullOrWhiteSpace(idToken.Payload.Acr))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21315, idToken)));

      if (RequireAmr && idToken.Payload.Amr.Count == 0)
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21316, idToken)));

      if (RequireAuthTime && !(idToken.Payload.AuthTime.HasValue))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21317, idToken)));

      if (RequireAzp && string.IsNullOrWhiteSpace(idToken.Payload.Azp))
        throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21318, idToken)));

      // if multiple audiences are present in the id_token, 'azp' claim should be present
      if (idToken.Payload.Aud.Count > 1 && string.IsNullOrEmpty(idToken.Payload.Azp))
      {
        LogHelper.LogWarning(ValidationMessages.IDX21339);
      }

      // if 'azp' claim exist, it should be equal to 'client_id' of the application
      if (!string.IsNullOrEmpty(idToken.Payload.Azp))
      {
        if (string.IsNullOrEmpty(validationContext.ClientId))
        {
          throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21308));
        }
        else if (!string.Equals(idToken.Payload.Azp, validationContext.ClientId))
        {
          throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21340, idToken.Payload.Azp, validationContext.ClientId)));
        }
      }
    }
  }

  /// <summary>
  /// Returns a <see cref="HashAlgorithm"/> corresponding to string 'algorithm' after translation using <see cref="HashAlgorithmMap"/>.
  /// </summary>
  /// <param name="algorithm">string representing the hash algorithm</param>
  /// <returns>A <see cref="HashAlgorithm"/>.</returns>
  static HashAlgorithm GetHashAlgorithm(string algorithm)
  {
    if (string.IsNullOrEmpty(algorithm))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21350));

    try
    {
      if (!HashAlgorithmMap.TryGetValue(algorithm, out string hashAlgorithm))
        hashAlgorithm = algorithm;

      return CryptoProviderFactory.CreateHashAlgorithm(hashAlgorithm);
    }
    catch (Exception ex)
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21301, LogHelper.MarkAsNonPII(algorithm), LogHelper.MarkAsNonPII(typeof(HashAlgorithm))), ex));
    }

    throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21302, LogHelper.MarkAsNonPII(algorithm))));
  }

  /// <summary>
  /// Validates the 'token' or 'code'. See: <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="expectedValue">The expected value of the hash. normally the c_hash or at_hash claim.</param>
  /// <param name="hashItem">Item to be hashed per oidc spec.</param>
  /// <param name="algorithm">Algorithm for computing hash over hashItem.</param>
  /// <exception cref="OpenIdConnectProtocolException">Thrown if the expected value does not equal the hashed value.</exception>
  static void ValidateHash(string expectedValue, string hashItem, string algorithm)
  {
    if (LogHelper.IsEnabled(EventLogLevel.Informational))
      LogHelper.LogInformation(ValidationMessages.IDX21303, expectedValue);

    HashAlgorithm hashAlgorithm = null;
    try
    {
      hashAlgorithm = GetHashAlgorithm(algorithm);
      CheckHash(hashAlgorithm, expectedValue, hashItem, algorithm);
    }
    finally
    {
      CryptoProviderFactory.ReleaseHashAlgorithm(hashAlgorithm);
    }
  }

  static void CheckHash(HashAlgorithm hashAlgorithm, string expectedValue, string hashItem, string algorithm)
  {
    var hashBytes = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(hashItem));
    var hashString = Base64UrlEncoder.Encode(hashBytes, 0, hashBytes.Length / 2);
    if (!string.Equals(expectedValue, hashString))
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant(ValidationMessages.IDX21300, expectedValue, hashItem, LogHelper.MarkAsNonPII(algorithm))));
    }
  }

  /// <summary>
  /// Validates the 'code' according to <see href="https://openid.net/specs/openid-connect-core-1_0.html"/>.
  /// </summary>
  /// <param name="validationContext">A <see cref="OpenIdConnectProtocolValidationContext"/> that contains the protocol message to validate.</param>
  /// <exception cref="ArgumentNullException">Thrown if <paramref name="validationContext"/> is null.</exception>
  /// <exception cref="ArgumentNullException">Thrown if <see cref="OpenIdConnectProtocolValidationContext.ValidatedIdToken"/> is null.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidCHashException">Thrown if <paramref name="validationContext"/> contains a 'code' and there is no 'c_hash' claim in the 'id_token'.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidCHashException">Thrown if <paramref name="validationContext"/> contains a 'code' and the 'c_hash' claim is not a string in the 'id_token'.</exception>
  /// <exception cref="OpenIdConnectProtocolInvalidCHashException">Thrown if the 'c_hash' claim in the 'id_token' does not correspond to the 'code' in the <see cref="OpenIdConnectMessage"/> response.</exception>
  static void ValidateCHash(OpenIdConnectProtocolValidationContext validationContext)
  {
    LogHelper.LogVerbose(ValidationMessages.IDX21304);

    if (validationContext == null)
      throw LogHelper.LogArgumentNullException(nameof(validationContext));

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogArgumentNullException(nameof(validationContext.ValidatedIdToken));

    if (validationContext.ProtocolMessage == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21333));

    if (string.IsNullOrEmpty(validationContext.ProtocolMessage.Code))
    {
      LogHelper.LogInformation(ValidationMessages.IDX21305);
      return;
    }

    object cHashClaim;
    if (!validationContext.ValidatedIdToken.Payload.TryGetValue(JwtRegisteredClaimNames.CHash, out cHashClaim))
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidCHashException(LogHelper.FormatInvariant(ValidationMessages.IDX21307, validationContext.ValidatedIdToken)));
    }

    var chash = cHashCle(ValidationMessages.IDX21309);

    if (validationContext == null)
      throw LogHelper.LogArgumentNullException("validationContext");

    if (validationContext.ValidatedIdToken == null)
      throw LogHelper.LogArgumentNullException("validationContext.ValidatedIdToken");

    if (validationContext.ProtocolMessage == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(ValidationMessages.IDX21333));

    if (string.IsNullOrEmpty(validationContext.ProtocolMessage.AccessToken))
    {
      LogHelper.LogInformation(ValidationMessages.IDX21310);
      return;
    }

    object atHashClaim;
    if (!validationContext.ValidatedIdToken.Payload.TryGetValue(JwtRegisteredClaimNames.AtHash, out atHashClaim))
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidAtHashException(LogHelper.FormatInvariant(ValidationMessages.IDX21312, validationContext.ValidatedIdToken)));

    var atHash = atHashClaim as string;
    if (atHash == null)
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidAtHashException(LogHelper.FormatInvariant(ValidationMessages.IDX21311, validationContext.ValidatedIdToken)));

    var idToken = validationContext.ValidatedIdToken;

    var alg = idToken.InnerToken != null ? idToken.InnerToken.Header.Alg : idToken.Header.Alg;

    try
    {
      ValidateHash(atHash, validationContext.ProtocolMessage.AccessToken, alg);
    }
    catch (OpenIdConnectProtocolException ex)
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidAtHashException(ValidationMessages.IDX21348, ex));
    }
  }

  public static string? ValidateState(OpenIdConnectValidationOptions validator, string? state)
  {
    if (!validator.RequireStateValidation) return default;
    if (!validator.RequireState && IsEmptyString(state) && IsEmptyString(state)) return default;

    if (string.IsNullOrEmpty(validationContext.State))
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidStateException(LogHelper.FormatInvariant(ValidationMessages.IDX21329, LogHelper.MarkAsNonPII(RequireState))));
    }
    if (string.IsNullOrEmpty(validationContext.ProtocolMessage.State))
    {
      // 'state' was sent, but message does not contain 'state'
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidStateException(LogHelper.FormatInvariant(ValidationMessages.IDX21330, LogHelper.MarkAsNonPII(RequireState))));
    }

    if (!string.Equals(validationContext.State, validationContext.ProtocolMessage.State))
    {
      throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidStateException(LogHelper.FormatInvariant(ValidationMessages.IDX21331, validationContext.State, validationContext.ProtocolMessage.State)));
    }
  }
}

