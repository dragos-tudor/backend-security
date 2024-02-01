using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string BuildChallengePath<TOptions> (
    TOptions authOptions,
    string returnUri)
  where TOptions: RemoteAuthenticationOptions =>
    $"{authOptions.ChallengePath}{QueryString.Create(authOptions.ReturnUrlParameter, returnUri)}";
}