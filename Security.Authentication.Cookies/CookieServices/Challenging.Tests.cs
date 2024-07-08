
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests {

  [TestMethod]
  public async Task Unautenticated_user_resource_request__challenge__login_redirection()
  {
    using var server = CreateHttpServer(services => services.AddCookiesServices());
    var authProperties = CreateAuthenticationProperties();
    server.MapGet("/resource", (HttpContext context) => ChallengeCookie(context, authProperties));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
    Assert.AreEqual("/Account/Login?ReturnUrl=/resource", GetResponseMessageLocation(response));
  }

  [TestMethod]
  public async Task Unautenticated_user_resource_request__challenge__login_redirection_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.MapGet("/resource", (HttpContext context) => context.ChallengeAsync());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
    Assert.AreEqual("http://localhost/Account/Login?ReturnUrl=/resource", GetResponseMessageLocation(response));
  }

}