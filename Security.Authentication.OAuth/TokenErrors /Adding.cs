
using System.Text;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static StringBuilder AddTokenErrorDetail (this StringBuilder builder, string name, string? message) =>
    !IsEmptyString(message)?
      builder.Append($";{name}=").Append(message):
      builder;

}