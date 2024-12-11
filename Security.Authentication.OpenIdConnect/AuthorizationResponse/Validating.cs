// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal const string MissingAuthorizationCode = "Missing authorization code from Authorization Endpoint.";
  internal const string MissingAuthorizationState = "Missing state from Authorization Endpoint [RequireState is '{0}'].";

  public static string? ValidateAuthorizationResponse(
    OpenIdConnectValidationOptions validationOptions,
    string? code,
    string? state)
  {
    if (ValidateCode(code) is string codeError)
      return codeError;

    if (ValidateState(validationOptions, state) is string stateError)
      return stateError;

    return default;
  }

  static string? ValidateCode(string? code) => IsEmptyString(code) ? MissingAuthorizationCode : default;

  static string? ValidateState(
    OpenIdConnectValidationOptions validationOptions,
    string? state)
  {
    if (!validationOptions.RequireStateValidation)
      return default;

    if (validationOptions.RequireState && IsEmptyString(state))
      return MissingAuthorizationState.Format(validationOptions.RequireState);

    // TODO: investigate RFC for state validation [request/response state comparison]
    // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/dev/src/Microsoft.IdentityModel.Protocols.OpenIdConnect/OpenIdConnectProtocolValidator.cs#L705

    return default;
  }
}