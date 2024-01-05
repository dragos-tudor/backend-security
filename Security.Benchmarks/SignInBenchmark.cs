
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

[DryJob]
[SimpleJob(invocationCount: 128)]
[SimpleJob(invocationCount: 512)]
[SimpleJob(invocationCount: 1024)]
[MemoryDiagnoser]
public class SignInBenchmarks
{
  static readonly IServiceProvider msServices = new ServiceCollection().AddLogging().AddAuthentication("Cookies").AddCookie().Services.BuildServiceProvider()!;
  static readonly IServiceProvider services = new ServiceCollection().AddLogging().AddCookies().AddDataProtection().Services.BuildServiceProvider();
  static readonly IIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
	static readonly IAuthenticationService authenticationService = msServices.GetRequiredService<IAuthenticationService>();

  [Benchmark(Baseline = true)]
  public async Task FPBenchmark ()
  {
    var context = new DefaultHttpContext(){RequestServices = services};
    await SignInCookie(
      context,
      new ClaimsPrincipal(identity),
      new AuthenticationProperties());
  }

  [Benchmark]
  public async Task OOPBenchmark ()
  {
    var context = new DefaultHttpContext(){RequestServices = msServices};
    // var authenticationService = msServices.GetRequiredService<IAuthenticationService>();
    await authenticationService.SignInAsync(
      context,
      CookieAuthenticationDefaults.AuthenticationScheme,
      new ClaimsPrincipal(identity),
      new AuthenticationProperties());
  }

}
