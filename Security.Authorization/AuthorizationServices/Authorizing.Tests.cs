
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using Security.Authentication;

namespace Security.Authorization;

partial class AuthorizationTests {

  static readonly ChallengeFunc challegeFunc = (context, schemeName) => (context.Response.StatusCode = StatusCodes.Status401Unauthorized).ToString();
  static readonly ForbidFunc forbidFunc = (context, schemeName) => (context.Response.StatusCode = StatusCodes.Status403Forbidden).ToString();

  [Fact]
  public async Task Authenticated_user__access_private_resource__user_authorized()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization().AddAuthentication().AddCookie() );
    server.UseAuthentication().UseAuthorization(challegeFunc, forbidFunc);
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("user")));
    server.MapGet("/resource", (HttpContext context) => "private" ).RequireAuthorization();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.Equal("private", await ReadResponseMessageContent(response));
  }

  [Fact]
  public async Task Unauthenticated_user__access_public_resource__user_authorized()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization() );
    server.UseAuthorization(challegeFunc, forbidFunc);
    server.MapGet("/resource", (HttpContext context) => "public" ).AllowAnonymous();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.Equal("public", await ReadResponseMessageContent(response));
  }

  [Fact]
  public async Task Unauthenticated_user__access_private_resource__unauthenticated_access()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization() );
    server.UseAuthorization(challegeFunc, forbidFunc);
    server.MapGet("/resource", (HttpContext context) => "not accesible" ).RequireAuthorization();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    Assert.Equal(string.Empty, await ReadResponseMessageContent(response));
  }

  [Fact]
  public async Task Authenticated_user_without_role__access_role_policy_private_resource__unauthorized_access()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization(options => options.AddPolicy("role policy", policy => policy.RequireRole("admin"))).AddAuthentication().AddCookie());
    server.UseAuthentication().UseAuthorization(challegeFunc, forbidFunc);
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("user")) );
    server.MapGet("/resource", (HttpContext context) => "not accesible").RequireAuthorization("role policy");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    Assert.Equal(string.Empty, await ReadResponseMessageContent(response));
  }

  [Fact]
  public async Task Authenticated_user_with_role__access_role_policy_private_resource__authorized_access()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization(options => options.AddPolicy("role policy", policy => policy.RequireRole("admin"))).AddAuthentication().AddCookie());
    server.UseAuthentication().UseAuthorization(challegeFunc, forbidFunc);
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("user", "admin")) );
    server.MapGet("/resource", (HttpContext context) => "private").RequireAuthorization("role policy");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.Equal("private", await ReadResponseMessageContent(response));
  }

  internal static ClaimsPrincipal CreateClaimsPrincipal (string userName, string? roleName = "role") =>
    new (new ClaimsIdentity(new List<Claim>{ new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.Role, roleName!)}, "Cookies"));

}

partial class MicrosoftTests {

  [Fact]
  public async Task Authenticated_user__access_private_resource__user_authorized()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization().AddAuthentication().AddCookie());
    server.UseAuthentication().UseAuthorization();
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(AuthorizationTests.CreateClaimsPrincipal("user", "admin")));
    server.MapGet("/resource", (HttpContext context) => "private").RequireAuthorization();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal("private", await ReadResponseMessageContent(response));
  }

}
