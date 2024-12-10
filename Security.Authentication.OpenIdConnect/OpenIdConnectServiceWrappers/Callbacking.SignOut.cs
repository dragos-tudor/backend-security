
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<string?> CallbackSignOut<TOptions>(
    HttpContext context,
    PostSignoutFunc<TOptions> postSignOut,
    SignOutFunc signOut,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    CallbackSignOut(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat(context),
      postSignOut,
      signOut,
      logger
    );
}