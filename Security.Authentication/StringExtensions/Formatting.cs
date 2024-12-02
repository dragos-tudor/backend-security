
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string FormatString(string format, object arg) => string.Format(format, arg);

  public static string FormatString(string format, object arg1, object arg2) => string.Format(format, arg1, arg2);

  public static string FormatString(string format, object arg1, object arg2, object arg3) => string.Format(format, arg1, arg2, arg3);
}