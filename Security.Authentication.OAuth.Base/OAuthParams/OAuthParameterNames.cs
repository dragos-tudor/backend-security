
namespace Security.Authentication.OAuth;

public static class OAuthParameterNames
{
  public const string ClientId = "client_id";
  public const string ResponseType = "response_type";
  public const string RedirectUri = "redirect_uri";
  public const string Scope = "scope";
  public const string State = "state";

  public const string AuthorizationCode = "code";
  public const string ClientSecret = "client_secret";
  public const string GrantType = "grant_type";

  public const string AccessToken = "access_token";
  public const string ExpiresIn = "expires_in";
  public const string RefreshToken = "refresh_token";
  public const string TokenType = "token_type";

  public const string AccessType = "access_type";
  public const string ApprovalPrompt = "approval_prompt";
  public const string IncludeGrantedScopes = "include_granted_scopes";
  public const string LoginHint = "login_hint";
  public const string OnlineAccessType = "online";
  public const string Prompt = "prompt";

  public const string CodeVerifier = "code_verifier";
  public const string CodeChallenge = "code_challenge";
  public const string CodeChallengeMethod = "code_challenge_method";
  public const string CodeChallengeMethodS256 = "S256";
}