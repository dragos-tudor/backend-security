// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

public class OpenIdConnectChallengeProperties(IDictionary<string, string?>? items = default, IDictionary<string, object?>? parameters = default):
  OAuthChallengeProperties(items, parameters)
{
  static readonly string MaxAgeKey = OpenIdConnectParameterNames.MaxAge;
  static readonly string PromptKey = OpenIdConnectParameterNames.Prompt;

  public TimeSpan? MaxAge
  {
    get => GetParameter<TimeSpan?>(MaxAgeKey);
    set => SetParameter(MaxAgeKey, value);
  }

  public string? Prompt
  {
    get => GetParameter<string>(PromptKey);
    set => SetParameter(PromptKey, value);
  }
}