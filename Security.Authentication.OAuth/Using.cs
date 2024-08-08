global using System;
global using System.Linq;
global using System.Security.Claims;
global using System.Threading.Tasks;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.Remote.RemoteFuncs;
global using static Security.Authentication.OAuth.OAuthFuncs;

namespace Security.Authentication.OAuth;

public static partial class OAuthFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif
