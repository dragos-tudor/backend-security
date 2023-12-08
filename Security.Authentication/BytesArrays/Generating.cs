
using System.Security.Cryptography;

namespace Security.Authentication;

partial class Funcs {

  public static void GenerateRandomBytes (byte[] bytes) =>
    RandomNumberGenerator.Fill(bytes);

}