
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<AuthenticateResult> AuthenticateOidc<TOptions>(
    HttpContext context,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    LogAuthentication(
      logger,
      await AuthenticateOidc(
        context,
        ResolveRequiredService<TOptions>(context),
        ResolveRequiredService<OpenIdConnectValidationOptions>(context),
        ResolveHttpClient(context),
        ResolvePropertiesDataFormat(context),
        PostAuthorize,
        ExchangeCodeForTokens,
        AccessUserInfo,
        logger
      ),
      ResolveRequiredService<TOptions>(context).SchemeName,
      context.TraceIdentifier
    );
}