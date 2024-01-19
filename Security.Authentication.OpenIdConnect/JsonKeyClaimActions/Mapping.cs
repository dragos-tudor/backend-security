// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static void MapUniqueJsonKey(
    this ClaimActionCollection collection,
    string claimType,
    string jsonKey) =>
      collection.MapUniqueJsonKey(claimType, jsonKey, ClaimValueTypes.String);

  public static void MapUniqueJsonKey(
    this ClaimActionCollection collection,
    string claimType,
    string jsonKey,
    string valueType) =>
      collection.Add(new UniqueJsonKeyClaimAction(claimType, valueType, jsonKey));
}