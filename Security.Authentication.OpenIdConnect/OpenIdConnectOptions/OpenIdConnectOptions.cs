// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

public record OpenIdConnectOptions : OAuthOptions
{
  public required OpenIdConnectRedirectBehaviour AuthenticationMethod { get; init; }

  public required PathString ChallengeSignOutPath { get; init; }
  public required PathString CallbackSignOutPath { get; init; }

  public string? CheckSessionIframe { get; init; }
  public bool GetClaimsFromUserInfoEndpoint { get; init; }

  public bool DisableTelemetry { get; init; }
  public required string Issuer { get; init; }
  public IEnumerable<SecurityKey> SigningKeys { get; init; } = [];
  public TimeSpan? MaxAge { get; init; }
  public required string Prompt { get; init; }

  public required string ResponseMode { get; init; }
  public bool RequireStateValidation { get; init; }
  public string? Resource { get; init; }

  public bool UseTokenLifetime { get; init; }
}