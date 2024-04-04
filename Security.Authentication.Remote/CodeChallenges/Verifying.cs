namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static bool ShouldCleanCodeChallenge(RemoteAuthenticationOptions remoteOptions) =>
    remoteOptions.UsePkce;

  public static bool ShouldUseCodeChallenge(RemoteAuthenticationOptions remoteOptions) =>
    remoteOptions.UsePkce;
}