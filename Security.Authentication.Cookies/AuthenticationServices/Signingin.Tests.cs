
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class Tests {

  [Fact]
  public static async Task Login_request__signin__authentication_cookie()
  {
    using var server = CreateHttpServer(services => services.AddCookies(options => options with { SchemeName = "CookiesScheme" }));
    server.MapPost("/account/login", (HttpContext context) => SignIn(context, CreateNamedClaimsPrincipal("CookiesScheme", "user")).ToString());
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
    using var server = CreateHttpServer(services => services.AddCookies(options => options with { SchemeName = "CookiesScheme" }));
    server.MapPost("/account/login", (HttpContext context) =>
      SignIn(context, CreateNamedClaimsPrincipal("CookiesScheme", "user"), new AuthenticationProperties() { RedirectUri  = context.Request.Form["redirect_url"] }).ToString());
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
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("CookiesScheme", new [] { CreateNameClaim("user") })));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/account/login", default);

    Assert.True(response.IsSuccessStatusCode);
    Assert.Contains("CookiesScheme", GetResponseMessageCookie(response));
  }

  static AuthenticationTicket SignIn(
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties? authProperties = default) =>
      SignInCookie(context,
        principal,
        authProperties ?? CreateAuthenticationProperties(),
        ResolveRequiredService<CookieAuthenticationOptions>(context),
        ResolveRequiredService<CookieBuilder>(context)!,
        ResolveRequiredService<TimeProvider>(context)!.GetUtcNow()
      );

}
