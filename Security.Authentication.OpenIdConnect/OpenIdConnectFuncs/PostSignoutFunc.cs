
namespace Security.Authentication.OpenIdConnect;

public delegate PostSignOutResult PostSignoutFunc<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions: OpenIdConnectOptions;