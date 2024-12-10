
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string Format(this string format, object arg) => String.Format(format, arg);

  public static string Format(this string format, object arg1, object arg2) => String.Format(format, arg1, arg2);

  public static string Format(this string format, object arg1, object arg2, object arg3) => String.Format(format, arg1, arg2, arg3);
}