// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

public sealed class NonceCookieBuilder(OpenIdConnectOptions oidcOptions): RequestPathBaseCookieBuilder
{
  protected override string AdditionalPath => oidcOptions.CallbackPath;

  public override CookieOptions Build(HttpContext context, DateTimeOffset expiresFrom)
  {
    var cookieOptions = base.Build(context, expiresFrom);

    if (Expiration is null || cookieOptions.Expires is null)
      SetNonceCookieOptionsExpires(cookieOptions, expiresFrom.Add(oidcOptions.NonceLifetime));

    return cookieOptions;
  }
}