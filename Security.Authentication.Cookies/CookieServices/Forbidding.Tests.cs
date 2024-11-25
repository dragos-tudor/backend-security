
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{
  [TestMethod]
  public async Task Unautenticated_user_resource_request__forbid__forbidden()
  {
    using var server = CreateHttpServer(services => services.AddCookiesServices());
    var authProps = CreateAuthProps();
    server.MapGet("/resource", (HttpContext context) => ForbidCookie(context));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
  }

  [TestMethod]
  public async Task Unautenticated_user_resource_request__forbid__access_denied_redirection_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.MapGet("/resource", (HttpContext context) => context.ForbidAsync());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
    Assert.AreEqual("http://localhost/Account/AccessDenied?ReturnUrl=/resource", GetResponseMessageLocation(response));
  }

}