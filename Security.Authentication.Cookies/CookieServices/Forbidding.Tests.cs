
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests {

  [TestMethod]
  public async Task Unautenticated_user_resource_request__forbid__forbidden_and_access_denied_path()
  {
    using var server = CreateHttpServer(services => services.AddCookiesServices());
    var authProperties = CreateAuthenticationProperties();
    server.MapGet("/resource",(HttpContext context) => ForbidCookie(context, authProperties));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    Assert.AreEqual("/Account/AccessDenied?ReturnUrl=%2Fresource", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Unautenticated_user_resource_request__forbid__access_denied_redirection_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.MapGet("/resource",(HttpContext context) => context.ForbidAsync());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
    Assert.AreEqual("http://localhost/Account/AccessDenied?ReturnUrl=/resource", GetResponseMessageLocation(response));
  }

}