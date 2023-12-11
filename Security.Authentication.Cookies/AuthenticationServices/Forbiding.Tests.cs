
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class Tests {

  [Fact]
  public async Task Unautenticated_user_resource_request__forbid__access_denied_redirection()
  {
    using var server = CreateHttpServer(services => services.AddCookies());
    server.MapGet("/resource", (HttpContext context) => Forbid(context));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("/Account/AccessDenied?ReturnUrl=/resource", GetResponseMessageLocation(response));
  }

  [Fact]
  public async Task Unautenticated_user_resource_request__forbid__access_denied_redirection_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.MapGet("/resource", (HttpContext context) => context.ForbidAsync());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("http://localhost/Account/AccessDenied?ReturnUrl=/resource", GetResponseMessageLocation(response));
  }

  static string? Forbid (HttpContext context) =>
    ForbidCookie(
      context,
      CreateAuthenticationProperties(),
      ResolveRequiredService<CookieAuthenticationOptions>(context));

}