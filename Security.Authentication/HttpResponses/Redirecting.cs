
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class Funcs {

  public static string SetResponseRedirect (HttpResponse response, string redirectUri) {
    response.Redirect(redirectUri!);
    return redirectUri;
  }

}