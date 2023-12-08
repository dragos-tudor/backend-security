
using System.Linq;

namespace Security.Authentication.Twitter;

partial class Funcs {

  static bool ExistsTwitterOptionsParam (IEnumerable<string>? param) =>
    param?.Any() ?? false;

}