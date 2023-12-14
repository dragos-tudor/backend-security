
using System.Linq;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  static bool HasExtraFields (FacebookOptions facebookOptions) =>
    facebookOptions.Fields.Any();

  static bool ShouldSendAppSecretProof (FacebookOptions facebookOptions) =>
    facebookOptions.SendAppSecretProof;

}