
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool IsFailedPostAuthorizationResult(PostAuthorizationResult authResult) =>
    authResult.Failure is not null;

  static bool IsSucceededPostAuthorizationResult(PostAuthorizationResult authResult) =>
    authResult.Success is not null;
}