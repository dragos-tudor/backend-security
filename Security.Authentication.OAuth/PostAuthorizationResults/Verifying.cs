
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool IsFailurePostAuthorizationResult(PostAuthorizationResult authResult) =>
    authResult.Failure is not null;

  static bool IsSucceessPostAuthorizationResult(PostAuthorizationResult authResult) =>
    authResult.Success is not null;
}