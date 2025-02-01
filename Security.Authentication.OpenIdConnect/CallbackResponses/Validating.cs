// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal const string MissingAuthorizationCode = "missing authorization code from Authorization Endpoint.";
  internal const string MissingState = "missing state from Authorization Endpoint [RequireState is '{0}'].";

  public static string? ValidateCallbackResponse( OpenIdConnectValidationOptions validationOptions, string? code, string? state)
  {
    if (ValidateAuthorizationCode(code) is string codeError) return codeError;
    if (ValidateState(validationOptions, state) is string stateError) return stateError;
    return default;
  }

  static string? ValidateAuthorizationCode(string? code) => IsEmptyString(code) ? MissingAuthorizationCode : default;

  static string? ValidateState(OpenIdConnectValidationOptions validationOptions, string? state)
  {
    if (!validationOptions.RequireStateValidation) return default;
    if (validationOptions.RequireState && IsEmptyString(state)) return MissingState.Format(validationOptions.RequireState);

    // TODO: investigate RFC for state validation [request/response state comparison]
    // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/dev/src/Microsoft.IdentityModel.Protocols.OpenIdConnect/OpenIdConnectProtocolValidator.cs#L705

    return default;
  }
}