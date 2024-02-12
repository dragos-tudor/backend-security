using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class PostAuthorizeInfo(AuthenticationProperties AuthenticationProperties, string? AuthorizationCode, string? IdToken, ClaimsIdentity? Identity);