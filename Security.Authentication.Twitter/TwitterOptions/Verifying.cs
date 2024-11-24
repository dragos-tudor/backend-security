
using System.Linq;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  static bool ExistsTwitterOptionsParam(IEnumerable<string>? param) => param?.Any() ?? false;
}