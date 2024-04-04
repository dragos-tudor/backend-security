// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Security.Authentication.OpenIdConnect;

public static class OpenIdConnectDefaults
{
  public const string AuthenticationPropertiesKey = "OpenIdConnect.AuthenticationProperties";
  public const string AuthenticationScheme = "OpenIdConnect";

  public const string DisplayName = "OpenIdConnect";
  public const string CookieNoncePrefix = ".AspNetCore.OpenIdConnect.Nonce.";

  public const string RedirectUriForCodePropertiesKey = "OpenIdConnect.Code.RedirectUri";
  public const string UserStatePropertiesKey = "OpenIdConnect.Userstate";
}