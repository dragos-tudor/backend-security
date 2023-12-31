
using System.Text;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static StringBuilder AddTokenErrorDetail (this StringBuilder builder, string name, string? message) =>
    !IsEmptyString(message)?
      builder.Append($";{name}=").Append(message):
      builder;

}