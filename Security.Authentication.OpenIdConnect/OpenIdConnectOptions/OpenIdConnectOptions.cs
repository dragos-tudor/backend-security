// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Security.Authentication.Remote;

namespace Security.Authentication.OpenIdConnect;

public record OpenIdConnectOptions : RemoteAuthenticationOptions
{
  public string? Authority { get; init; }
  public required OpenIdConnectRedirectBehaviour AuthenticationMethod { get; init; }
  public required string ClientId { get; init; }
  public required string ClientSecret { get; init; }

  public string? CheckSessionIframe { get; init; }
  public bool GetClaimsFromUserInfoEndpoint { get; init; }

  public bool DisableTelemetry { get; init; }
  public TimeSpan? MaxAge { get; init; }
  public required TimeSpan NonceLifetime { get; init; }

  public string? Prompt { get; init; }
  public bool RequireStateValidation { get; init; }
  public required string ResponseMode { get; init; }
  public required string ResponseType { get; init; }
  public bool RequireNonce { get; init; }
  public string? Resource { get; init; }
  public required ICollection<string> Scope { get; init; }

  public required PathString SignOutChallengePath { get; init; }
  public required PathString SignOutCallbackPath { get; init; }
  public required string SignOutRedirectUri { get; init; }
  public string? SignOutScheme { get; init; }

  public bool UseTokenLifetime { get; init; }
}