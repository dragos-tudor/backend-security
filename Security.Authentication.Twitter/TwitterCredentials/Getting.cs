
using System.Text;
using static System.Convert;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  internal static string GetTwitterCredentials(string clientId, string clientSecret) =>
    ToBase64String(Encoding.ASCII.GetBytes(FormatClientCredentials(clientId, clientSecret)));
}