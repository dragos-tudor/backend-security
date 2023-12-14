
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests {

  [Fact]
  public async Task Logout_request__signout__expired_cookie()
  {
    using var server = CreateHttpServer(services => services.AddCookies() );
    server.MapPost("/account/logout", (HttpContext context) => SignOutCookie(context) ?? string.Empty);
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/account/logout");

    Assert.True(response.IsSuccessStatusCode);
    Assert.Contains(".AspNetCore.Cookies=;", GetResponseMessageCookie(response));
    Assert.Contains("expires=Thu, 01 Jan 1970", GetResponseMessageCookie(response));
  }

  [Fact]
  public async Task Logout_request_with_return_url__signin__response_redirected_to_return_url()
  {
    using var server = CreateHttpServer(services => services.AddCookies() );
    var GetAuthProperties = (HttpContext context) => new AuthenticationProperties() { RedirectUri = context.Request.Form["redirect_url"] };
    server.MapPost("/account/logout", (HttpContext context) => SignOutCookie(context, GetAuthProperties(context)) ?? string.Empty);
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/account/logout", new FormUrlEncodedContent(new Dictionary<string, string> { { "redirect_url", "/logged-out" } }));

    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("/logged-out", GetResponseMessageLocation(response));
  }

  [Fact]
  public async Task Logout_request__signout__expired_cookie_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.UseAuthentication();
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("Cookies", new [] { CreateNameClaim("user") })));
    server.MapPost("/account/logout", (HttpContext context) => context.SignOutAsync());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login");
    using var response = await client.PostAsync("/Account/Logout", GetRequestMessageCookieHeader(loginResponse));

    Assert.True(response.IsSuccessStatusCode);
    Assert.Contains(".AspNetCore.Cookies=;", GetResponseMessageCookie(response));
    Assert.Contains("expires=Thu, 01 Jan 1970", GetResponseMessageCookie(response));
  }

}