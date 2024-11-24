
using System.Text;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  static StringBuilder AppendHexaStringByte(StringBuilder builder, byte @byte) => builder.Append(ToHexString(@byte));
}