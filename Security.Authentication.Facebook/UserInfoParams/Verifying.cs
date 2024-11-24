
namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  static bool HasExtraFields(FacebookOptions facebookOptions) => facebookOptions.Fields.Count != 0;

  static bool ShouldSendAppSecretProof(FacebookOptions facebookOptions) => facebookOptions.SendAppSecretProof;
}