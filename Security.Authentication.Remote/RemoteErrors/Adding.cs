
using System.Globalization;
using System.Text;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static StringBuilder AddErrorDetail (this StringBuilder builder, string name, string? message) =>
    !IsEmptyString(message)?
      builder.Append(CultureInfo.InvariantCulture, $";{name}={message}"):
      builder;
}