
namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  static KeyValuePair<string, string?>[] BuildSpecificUserInfoParams (
    FacebookOptions facebookOptions,
    string accessToken) =>
      [
        CreateAccessTokenParam(accessToken),
        ShouldSendAppSecretProof(facebookOptions)?
          CreateAppSecretProofParam(GenerateAppSecretProof(facebookOptions.ClientSecret, accessToken)):
          default,
        HasExtraFields(facebookOptions) ?
          CreateFieldsParam(string.Join(",", facebookOptions.Fields)):
          default
      ];

}