
using System.Globalization;
using System.Text;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  static byte[] ToBytes (string text) => Encoding.ASCII.GetBytes(text);

  static string ToHexString (byte @byte) => @byte.ToString("x2", CultureInfo.InvariantCulture);

}