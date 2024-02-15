using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static AuthenticationProperties CreateAuthenticationProperties() =>
    new ();
}