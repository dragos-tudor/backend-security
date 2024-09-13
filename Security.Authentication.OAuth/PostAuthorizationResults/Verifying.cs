
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool IsPostAuthorizationResultFailure (PostAuthorizationResult authResult) => authResult.Failure is not null;

  static bool IsPostAuthorizationResultSucceess (PostAuthorizationResult authResult) => authResult.Success is not null;
}