
namespace Security.Authentication;

public delegate T IdentityFunc<T> (T options);

partial class AuthenticationFuncs {

  public static T Identity<T> (T value) => value;

}