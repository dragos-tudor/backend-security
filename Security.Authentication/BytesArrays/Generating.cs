
using System.Security.Cryptography;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static void GenerateRandomBytes (byte[] data) =>
    RandomNumberGenerator.Fill(data);

}