global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Builder;
global using System;
global using Security.Authentication.Cookies;
global using Security.Authentication.Google;
global using Security.Authentication.Facebook;
global using Security.Authentication.Twitter;
global using Security.Authentication;

global using static Security.Authentication.Funcs;
global using static Security.Authentication.Cookies.Funcs;
global using static Security.Authentication.Google.Funcs;
global using static Security.Authentication.Facebook.Funcs;
global using static Security.Authentication.Twitter.Funcs;
global using static Security.Authentication.OAuth.Funcs;
global using static Security.Authorization.Funcs;
global using static Security.Samples.Funcs;


namespace Security.Samples;

public static partial class Funcs { }