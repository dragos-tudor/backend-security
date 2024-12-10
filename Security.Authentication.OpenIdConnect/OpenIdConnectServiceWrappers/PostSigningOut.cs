
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static PostSignOutResult PostSignOut<TOptions>(
    HttpContext context)
  where TOptions : OpenIdConnectOptions =>
    PostSignOut(
      context,
      ResolvePropertiesDataFormat(context));
}