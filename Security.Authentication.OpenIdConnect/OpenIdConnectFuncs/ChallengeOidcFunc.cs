
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public delegate string? ChallengeOidcFunc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps)
  where TOptions : OpenIdConnectOptions;
}