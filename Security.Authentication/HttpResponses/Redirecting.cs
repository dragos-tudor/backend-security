
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static string SetResponseRedirect (HttpResponse response, string redirectUri) {
    response.Redirect(redirectUri!);
    return redirectUri;
  }

}