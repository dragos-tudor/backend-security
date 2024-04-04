// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace Security.Authentication.BearerToken;

/// <summary>
/// Contains the options used to authenticate using opaque bearer tokens.
/// </summary>
public sealed record BearerTokenOptions : AuthenticationOptions
{
    /// <summary>
    /// Controls how much time the bearer token will remain valid from the point it is created.
    /// The expiration information is stored in the protected token. Because of that, an expired token will be rejected
    /// even if it is passed to the server after the client should have purged it.
    /// </summary>
    /// <remarks>
    /// Defaults to 1 hour.
    /// </remarks>
    public TimeSpan BearerTokenExpiration { get; init; }
    /// <summary>
    /// Controls how much time the refresh token will remain valid from the point it is created.
    /// The expiration information is stored in the protected token.
    /// </summary>
    /// <remarks>
    /// Defaults to 14 days.
    /// </remarks>
    public TimeSpan RefreshTokenExpiration { get; init; }
}