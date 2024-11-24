
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static bool UnsetAuthPropsCodeVerifier(AuthenticationProperties authProps) => UnsetAuthPropsItem(authProps, OAuthParamNames.CodeVerifier);

  public static bool UnsetAuthPropsCorrelationId(AuthenticationProperties authProps) => UnsetAuthPropsItem(authProps, CorrelationId);
}