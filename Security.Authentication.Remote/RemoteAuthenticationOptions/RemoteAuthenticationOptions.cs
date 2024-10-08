// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

public record RemoteAuthenticationOptions: AuthenticationOptions
{
  public required PathString CallbackPath { get; init; }
  public required PathString ChallengePath { get; init; }

  public string? ClaimsIssuer { get; init; }
  public ClaimActionCollection ClaimActions { get; init; } = [];

  public TimeSpan RemoteAuthenticationTimeout { get; init; } = TimeSpan.FromMinutes(15);

  public bool SaveTokens { get; init; }
  public bool UsePkce { get; init; }
}