
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string ProtectAuthProps(AuthenticationProperties authProps, PropertiesDataFormat propDataProtector) => propDataProtector.Protect(authProps);
}