using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class PostAuthorizationInfo(AuthenticationProperties AuthenticationProperties, string? AuthorizationCode, string? IdToken, ClaimsIdentity? Identity);