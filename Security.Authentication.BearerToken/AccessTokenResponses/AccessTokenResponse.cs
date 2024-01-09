// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Security.Authentication.BearerToken;

public sealed class AccessTokenResponse
{
    public string TokenType { get; } = "Bearer";
    public required string AccessToken { get; init; }
    public required long ExpiresIn { get; init; }
    public required string RefreshToken { get; init; }
}