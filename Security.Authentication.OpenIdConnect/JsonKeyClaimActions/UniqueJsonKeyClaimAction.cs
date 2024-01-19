// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.OpenIdConnect;

public class UniqueJsonKeyClaimAction(string claimType, string valueType, string jsonKey) :
  JsonKeyClaimAction(claimType, valueType, jsonKey)
{
  public override void Run(JsonElement userData, ClaimsIdentity identity, string issuer)
  {
    var value = userData.GetString(JsonKey);
    if (IsEmptyString(value)) return;

    var claim = identity.FindFirst(claim => IsClaimWithType(claim, ClaimType));
    if (IsDuplicateClaim(claim, value!)) return;

    claim = identity.FindFirst(claim =>
      claim.Properties.TryGetValue(JwtSecurityTokenHandler.ShortClaimTypeProperty, out var shortType) &&
      IsClaimTypesEquality(shortType, ClaimType)
    );
    if (IsDuplicateClaim(claim, value!)) return;

    AddIdentityClaim(identity, CreateClaim(ClaimType, value!, ValueType, issuer));
  }
}