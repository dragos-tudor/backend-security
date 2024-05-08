global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Builder;
global using System;
global using Security.Authentication.Cookies;
global using Security.Authentication.Google;
global using Security.Authentication.Facebook;
global using Security.Authentication.Twitter;
global using Security.Authentication;

global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.Cookies.CookiesFuncs;
global using static Security.Authentication.Google.GoogleFuncs;
global using static Security.Authentication.Facebook.FacebookFuncs;
global using static Security.Authentication.Twitter.TwitterFuncs;
global using static Security.Authorization.AuthorizationFuncs;
global using static Security.Sample.Endpoints.EndpointsFuncs;

namespace Security.Sample.Api;

public static partial class ApiFuncs { }