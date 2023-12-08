
using System.Text;

namespace Security.Authentication.Facebook;

partial class Funcs {

  static StringBuilder AppendHexaStringByte (StringBuilder builder, byte @byte) =>
    builder.Append(ToHexString(@byte));

}