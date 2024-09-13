global using System;
global using System.Security.Claims;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.Cookies.CookiesFuncs;

namespace Security.Authentication.Cookies;

public static partial class CookiesFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif
