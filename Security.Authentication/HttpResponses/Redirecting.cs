
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? SetResponseRedirect (HttpResponse response, string? redirectUri) {
    if(redirectUri is not null)
      response.Redirect(redirectUri);
    return redirectUri;
  }
}