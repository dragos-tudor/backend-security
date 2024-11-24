
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace Security.Authorization;
#pragma warning disable CA1812

partial class AuthorizationTests {

  static readonly ChallengeFunc challegeFunc = (context) => (context.Response.StatusCode = StatusCodes.Status401Unauthorized).ToString(CultureInfo.InvariantCulture);
  static readonly ForbidFunc forbidFunc = (context) => (context.Response.StatusCode = StatusCodes.Status403Forbidden).ToString(CultureInfo.InvariantCulture);

  [TestMethod]
  public async Task Authenticated_user__access_private_resource__user_authorized()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization().AddAuthentication().AddCookie() );
    server.UseAuthentication().UseAuthorization(challegeFunc, forbidFunc);
    server.MapPost("/account/login",(HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("user")));
    server.MapGet("/resource",(HttpContext context) => "private" ).RequireAuthorization();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual("private", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Unauthenticated_user__access_public_resource__user_authorized()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization() );
    server.UseAuthorization(challegeFunc, forbidFunc);
    server.MapGet("/resource",(HttpContext context) => "public" ).AllowAnonymous();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual("public", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Unauthenticated_user__access_private_resource__unauthorized_access()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization() );
    server.UseAuthorization(challegeFunc, forbidFunc);
    server.MapGet("/resource",(HttpContext context) => "not accesible" ).RequireAuthorization();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    Assert.AreEqual(string.Empty, await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Unauthenticated_user__challenge_user_access_authentication_properties__dont_throw_error()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization() );
    server.UseAuthorization((context) => {}, forbidFunc);
    server.MapGet("/resource",(HttpContext context) => "not accesible" ).RequireAuthorization();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.GetAsync("/resource");

    Assert.AreEqual(string.Empty, await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_without_role__access_role_policy_private_resource__forbidden_access()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization(options => options.AddPolicy("role policy", policy => policy.RequireRole("admin"))).AddAuthentication().AddCookie());
    server.UseAuthentication().UseAuthorization(challegeFunc, forbidFunc);
    server.MapPost("/account/login",(HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("user")) );
    server.MapGet("/resource",(HttpContext context) => "not accesible").RequireAuthorization("role policy");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    Assert.AreEqual(string.Empty, await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_with_role__access_role_policy_private_resource__authorized_access()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization(options => options.AddPolicy("role policy", policy => policy.RequireRole("admin"))).AddAuthentication().AddCookie());
    server.UseAuthentication().UseAuthorization(challegeFunc, forbidFunc);
    server.MapPost("/account/login",(HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("user", "admin")) );
    server.MapGet("/resource",(HttpContext context) => "private").RequireAuthorization("role policy");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual("private", await ReadResponseMessageContent(response));
  }


  [TestMethod]
  public async Task Authenticated_user_with_claims__access_role_policy_private_resource__authorized_access()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization(options => options.AddPolicy("claim policy", policy => policy.RequireClaim("custom claim", "value"))).AddAuthentication().AddCookie());
    server.UseAuthentication().UseAuthorization(challegeFunc, forbidFunc);
    server.MapPost("/account/login",(HttpContext context) => context.SignInAsync(CreateClaimsPrincipalWithClaim("custom claim", "value")) );
    server.MapGet("/resource",(HttpContext context) => "private").RequireAuthorization("claim policy");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual("private", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_with_requirement__access_role_policy_private_resource__authorized_access()
  {
    using var server = CreateHttpServer(services => services
      .AddSingleton<IAuthorizationHandler, MinimumAgeHandler>()
      .AddAuthorization(options => options.AddPolicy("req policy", policy => policy.AddRequirements([new MinimumAgeRequirement(21)])))
      .AddAuthentication().AddCookie());
    server.UseAuthentication().UseAuthorization(challegeFunc, forbidFunc);
    server.MapPost("/account/login",(HttpContext context) => context.SignInAsync(CreateClaimsPrincipalWithClaim(ClaimTypes.DateOfBirth, DateTime.Now.AddYears(-23).ToString(CultureInfo.InvariantCulture))) );
    server.MapGet("/resource",(HttpContext context) => "private").RequireAuthorization("req policy");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual("private", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user__access_private_resource__user_authorized_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthorization().AddAuthentication().AddCookie());
    server.UseAuthentication().UseAuthorization();
    server.MapPost("/account/login",(HttpContext context) => context.SignInAsync(CreateClaimsPrincipal("user", "admin")));
    server.MapGet("/resource",(HttpContext context) => "private").RequireAuthorization();
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.AreEqual("private", await ReadResponseMessageContent(response));
  }

  static ClaimsPrincipal CreateClaimsPrincipal(string userName, string? roleName = "role") =>
    new(new ClaimsIdentity(new List<Claim>{ new(ClaimTypes.Name, userName), new(ClaimTypes.Role, roleName!)}, "Cookies"));

  static ClaimsPrincipal CreateClaimsPrincipalWithClaim(string claimType, string claimValue) =>
    new(new ClaimsIdentity(new List<Claim>{ new(claimType, claimValue!) }, "Cookies"));

  internal sealed class MinimumAgeRequirement : IAuthorizationRequirement
  {
      public MinimumAgeRequirement(int minimumAge) =>
          MinimumAge = minimumAge;

      public int MinimumAge { get; }
  }

  internal sealed class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
  {
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var dateOfBirthClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);
        if(dateOfBirthClaim is null) return Task.CompletedTask;

        var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value, CultureInfo.InvariantCulture);
        int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;

        if(dateOfBirth > DateTime.Today.AddYears(-calculatedAge)) calculatedAge--;
        if(calculatedAge >= requirement.MinimumAge) context.Succeed(requirement);

        return Task.CompletedTask;
    }
  }

}
