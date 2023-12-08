
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

public delegate T IdentityFunc<T> (T options);

partial class Funcs {

  public static T Identity<T> (T value) => value;

}