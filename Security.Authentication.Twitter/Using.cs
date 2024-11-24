global using System;
global using System.Collections.Generic;
global using System.Security.Claims;
global using Security.Authentication.OAuth;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.OAuth.OAuthFuncs;
global using static Security.Authentication.OAuth.OAuthBaseFuncs;

namespace Security.Authentication.Twitter;

public static partial class TwitterFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif