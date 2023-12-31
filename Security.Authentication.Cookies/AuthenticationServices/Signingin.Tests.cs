
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests {

  [Fact]
  public static async Task Login_request__signin__authentication_cookie()
  {
    using var server = CreateHttpServer(services => services.AddCookies((CreateCookieAuthenticationOptions()) with { SchemeName = "CookiesScheme" }));
    var authProperties = CreateAuthenticationProperties();
    server.MapPost("/account/login", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("CookiesScheme", "user"), authProperties).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/account/login");

    Assert.True(response.IsSuccessStatusCode);
    Assert.Contains("CookiesScheme", GetResponseMessageCookie(response));
    Assert.DoesNotContain("expires=Thu, 01 Jan 1970", GetResponseMessageCookie(response));
  }

  [Fact]
  public static async Task Login_request_with_return_url__signin__response_redirected_to_return_url()
  {
    using var server = CreateHttpServer(services => services.AddCookies((CreateCookieAuthenticationOptions()) with { SchemeName = "CookiesScheme" }));
    var principal = CreateNamedClaimsPrincipal("CookiesScheme", "user");
    var GetAuthProperties = (HttpContext context) => new AuthenticationProperties() { RedirectUri  = context.Request.Form["redirect_url"] };
    server.MapPost("/account/login", (HttpContext context) => SignInCookie(context, principal, GetAuthProperties(context)).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/account/login", new FormUrlEncodedContent(new Dictionary<string, string> { { "redirect_url", "/logged-in" } }));

    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("/logged-in", GetResponseMessageLocation(response));
  }

  [Fact]
  public async Task Login_request__signin__authentication_cookie_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie(o => o.Cookie.Name = "CookiesScheme"));
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreatePrincipal("CookiesScheme", new [] { CreateNameClaim("user") })));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/account/login", default);

    Assert.True(response.IsSuccessStatusCode);
    Assert.Contains("CookiesScheme", GetResponseMessageCookie(response));
  }

}
