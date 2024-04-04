// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Security.Authentication.Twitter;

public static class TwitterDefaults
{
  public const string AuthenticationScheme = "Twitter";
  public static readonly string ChallengePath = "/challenge-twitter";
  public static readonly string CallbackPath = "/callback-twitter";

  internal static readonly string AuthorizationEndpoint = "https://twitter.com/i/oauth2/authorize";
  internal static readonly string TokenEndpoint = "https://api.twitter.com/2/oauth2/token";
  internal static readonly string UserInformationEndpoint = "https://api.twitter.com/2/users/me";
}