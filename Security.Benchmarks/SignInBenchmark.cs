
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Security.Authentication.Cookies;
using static Security.Authentication.Cookies.CookiesFuncs;
#pragma warning disable CA1822

namespace Security.Benchmarks;

[SimpleJob(invocationCount: 128)]
[SimpleJob(invocationCount: 512)]
[SimpleJob(invocationCount: 1024)]
[MemoryDiagnoser]
public class SignInBenchmarks
{
  const string cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  static readonly IServiceProvider msServices = new ServiceCollection().AddLogging().AddAuthentication(cookieScheme).AddCookie(cookieScheme).Services.BuildServiceProvider()!;
  static readonly IServiceProvider myServices = new ServiceCollection().AddCookies().AddDataProtection().Services.BuildServiceProvider();
  static readonly IIdentity identity = new ClaimsIdentity(cookieScheme);
	static readonly IAuthenticationService authenticationService = msServices.GetRequiredService<IAuthenticationService>();

  [Benchmark(Baseline = true)]
  public async Task FPSignin ()
  {
    var context = new DefaultHttpContext(){RequestServices = myServices};
    await SignInCookie(
      context,
      new ClaimsPrincipal(identity),
      new AuthenticationProperties());
  }

  [Benchmark]
  public async Task OOPSignin ()
  {
    var context = new DefaultHttpContext(){RequestServices = msServices};
    // var authenticationService = msServices.GetRequiredService<IAuthenticationService>();
    await authenticationService.SignInAsync(
      context,
      cookieScheme,
      new ClaimsPrincipal(identity),
      new AuthenticationProperties());
  }

}
