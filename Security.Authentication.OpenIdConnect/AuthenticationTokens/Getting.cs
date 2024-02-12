// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static IEnumerable<AuthenticationToken> GetAuthenticationTokens(
    OpenIdConnectMessage oidcMessage,
    DateTimeOffset currentUtc)
  {
    if (IsNotEmptyString(oidcMessage.AccessToken))
      yield return CreateAuthenticationAccessToken(oidcMessage.AccessToken);

    if (IsNotEmptyString(oidcMessage.IdToken))
      yield return CreateAuthenticationIdToken(oidcMessage.IdToken);

    if (IsNotEmptyString(oidcMessage.RefreshToken))
      yield return CreateAuthenticationRefreshToken(oidcMessage.RefreshToken);

    if (IsNotEmptyString(oidcMessage.TokenType))
      yield return CreateAuthenticationTokenType(oidcMessage.TokenType);

    if (IsEmptyString(oidcMessage.ExpiresIn))
      yield break;

    if(ToAuthenticationTokenExpiresInTimeSpan(oidcMessage.ExpiresIn) is TimeSpan expiresIn)
      yield return CreateAuthenticationTokenExpiresAt(
        ToAuthenticationTokenExpiresAtString(currentUtc + expiresIn));
  }
}