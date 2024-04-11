namespace Security.Authentication.OpenIdConnect;

partial record class TokenResult
{
  public static implicit operator TokenResult(string failure) =>
    new (default, failure);

  public static implicit operator TokenResult(TokenInfo tokenInfo) =>
    new (tokenInfo);

  public static TokenResult ToTokenResult(TokenInfo tokenInfo) => tokenInfo;

  public static TokenResult ToTokenResult(string failure) => failure;
}