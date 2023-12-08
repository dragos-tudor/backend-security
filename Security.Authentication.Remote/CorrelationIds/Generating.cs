
namespace Security.Authentication.Remote;

partial class Funcs {

  public static string GenerateCorrelationId ()
  {
    var bytes = RentBytes(32);
    GenerateRandomBytes(bytes);

    var correlationId = EncodeBytes(bytes);
    ReturnBytes(bytes);

    return correlationId;
  }

}