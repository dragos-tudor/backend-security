
using System.Security.Cryptography;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static void GenerateRandomBytes (byte[] bytes) =>
    RandomNumberGenerator.Fill(bytes);

}