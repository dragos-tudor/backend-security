// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Security.Authentication.Twitter;

public static class TwitterDefaults
{
  public const string AuthenticationScheme = "Twitter";
  public const string ChallengePath = "/challenge-twitter";
  public const string CallbackPath = "/callback-twitter";

  internal const string AuthorizationEndpoint = "https://twitter.com/i/oauth2/authorize";
  internal const string TokenEndpoint = "https://api.twitter.com/2/oauth2/token";
  internal const string UserInformationEndpoint = "https://api.twitter.com/2/users/me";
}