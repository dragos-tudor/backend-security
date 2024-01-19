
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? SetResponseLocation (HttpResponse response, string location) =>
    response.Headers.Location = location;

  public static string? SetResponseRedirect (HttpResponse response, string? redirectUri) {
    if(redirectUri is not null)
      response.Redirect(redirectUri);
    return redirectUri;
  }

  public static string? SetResponseSetCookie (HttpResponse response, string cookie) =>
    response.Headers.SetCookie = cookie;
}