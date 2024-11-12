
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  const char BackSlash = '\\';
  const char Slash = '/';

  public static bool ExistsUri(string? uri) => IsNotEmptyString(uri);

  public static bool IsEmptyUri(string? uri) => IsEmptyString(uri);

  public static bool IsNotEmptyUri(string? uri) => IsNotEmptyString(uri);

  public static bool IsRelativeUri(string? uri) =>
   (uri ?? string.Empty).Length switch {
      0 => false,
      1 => uri![0] == Slash,
      _ => uri![0] == Slash && uri![1] != Slash && uri[1] != BackSlash
    };
}