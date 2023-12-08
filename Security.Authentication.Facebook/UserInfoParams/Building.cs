
namespace Security.Authentication.Facebook;

partial class Funcs {

  static IEnumerable<KeyValuePair<string, string?>> BuildSpecificUserInfoParams (
    FacebookOptions facebookOptions,
    string accessToken) =>
      new KeyValuePair<string, string?>[] {
        CreateAccessTokenParam(accessToken),
        ShouldSendAppSecretProof(facebookOptions)? CreateAppSecretProofParam(GenerateAppSecretProof(facebookOptions.ClientSecret, accessToken)): default,
        HasExtraFields(facebookOptions) ? CreateFieldsParam(string.Join(",", facebookOptions.Fields)): default
      };

}