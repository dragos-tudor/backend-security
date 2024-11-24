global using System;
global using System.Collections.Generic;
global using System.Security.Claims;
global using Security.Authentication.OAuth;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.OAuth.OAuthFuncs;
global using static Security.Authentication.OAuth.OAuthBaseFuncs;

namespace Security.Authentication.Facebook;

public static partial class FacebookFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif