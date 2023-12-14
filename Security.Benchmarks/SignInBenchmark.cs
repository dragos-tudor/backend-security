
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

// [SimpleJob(invocationCount: 2048)] // took to long
[MemoryDiagnoser]
public class SignInBenchmarks
{
  static readonly IServiceProvider msServices = new ServiceCollection().AddLogging().AddAuthentication("Cookies").AddCookie().Services.BuildServiceProvider()!;
  static readonly IServiceProvider services = new ServiceCollection().AddLogging().AddCookies().AddDataProtection().Services.BuildServiceProvider();
  static readonly IIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
	// static readonly IAuthenticationService authenticationService = MsServices.GetRequiredService<IAuthenticationService>();

  [Benchmark(Baseline = true)]
  public void FPBenchmark ()
  {
    var context = new DefaultHttpContext(){RequestServices = services};
    SignInCookie(
      context,
      new ClaimsPrincipal(identity),
      new AuthenticationProperties());
  }

  [Benchmark]
  public Task OOPBenchmark ()
  {
    var context = new DefaultHttpContext(){RequestServices = msServices};
    var authenticationService = msServices.GetRequiredService<IAuthenticationService>();
    return authenticationService.SignInAsync(
      context,
      CookieAuthenticationDefaults.AuthenticationScheme,
      new ClaimsPrincipal(identity),
      new AuthenticationProperties());
  }

}
