global using System;
global using System.Threading.Tasks;
global using static Security.Authentication.AuthenticationFuncs;

namespace Security.Authorization;

public static partial class AuthorizationFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif