// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Security.Authentication.Google;

public static class GoogleDefaults
{
  public const string AuthenticationScheme = "Google";
  public const string ChallengePath = "/challenge-google";
  public const string CallbackPath = "/callback-google";

  internal const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
  internal const string TokenEndpoint = "https://oauth2.googleapis.com/token";
  internal const string UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
}