// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Security.Authentication.Facebook;

public static class FacebookDefaults
{
  public const string AuthenticationScheme = "Facebook";
  public const string ChallengePath = "/challenge-facebook";
  public const string CallbackPath = "/callback-facebook";

  internal const string AuthorizationEndpoint = "https://www.facebook.com/v11.0/dialog/oauth";
  internal const string TokenEndpoint = "https://graph.facebook.com/v11.0/oauth/access_token";
  internal const string UserInformationEndpoint = "https://graph.facebook.com/v11.0/me";
}
