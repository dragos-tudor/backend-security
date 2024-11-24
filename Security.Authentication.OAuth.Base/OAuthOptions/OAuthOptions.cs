// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

public record OAuthOptions : AuthenticationOptions
{
  static readonly TimeSpan DefaultAuthenticationTimeout = TimeSpan.FromMinutes(15);

  public required PathString CallbackPath { get; init; }
  public required PathString ChallengePath { get; init; }

  public required string AuthorizationEndpoint { get; init; }
  public required string TokenEndpoint { get; init; }
  public required string UserInfoEndpoint { get; init; }

  public required string ClientId { get; init; }
  public required string ClientSecret { get; init; }

  public string? ClaimsIssuer { get; init; }
  public IEnumerable<ClaimAction> ClaimActions { get; init; } = [];
  public IEnumerable<ClaimMapper> ClaimMappers { get; init; } = [];

  public TimeSpan AuthenticationTimeout { get; init; } = DefaultAuthenticationTimeout;
  public OAuthParams AdditionalAuthorizationParameters { get; } = [];
  public required string ResponseType { get; init; }

  public bool SaveTokens { get; init; }
  public IEnumerable<string> Scope { get; init; } = [];
  public char ScopeSeparator { get; init; } = ' ';
  public bool UsePkce { get; init; }
}