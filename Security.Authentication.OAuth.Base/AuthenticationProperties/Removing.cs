
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static bool RemoveAuthPropsCodeVerifier(AuthenticationProperties authProps) => RemoveAuthPropsItem(authProps, OAuthParamNames.CodeVerifier);

  public static bool RemoveAuthPropsCorrelationId(AuthenticationProperties authProps) => RemoveAuthPropsItem(authProps, CorrelationId);
}