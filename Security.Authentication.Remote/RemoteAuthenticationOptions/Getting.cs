
namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string GetClaimsIssuer (RemoteAuthenticationOptions remoteOptions) =>
    remoteOptions.ClaimsIssuer ?? remoteOptions.SchemeName;
}