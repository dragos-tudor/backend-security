
using System.Threading.Tasks;

namespace Security.Samples;

partial class Funcs {

  static async Task<bool> AuthenticateRemoteAsync (HttpContext context)
  {
    if (await AuthenticateAndSignInOAuthAsync(context, ResolveService<GoogleOptions>(context), AuthenticateGoogleAsync, SignInRemote)) return true;
    if (await AuthenticateAndSignInOAuthAsync(context, ResolveService<FacebookOptions>(context), AuthenticateFacebookAsync, SignInRemote)) return true;
    if (await AuthenticateAndSignInOAuthAsync(context, ResolveService<TwitterOptions>(context), AuthenticateTwitterAsync, SignInRemote)) return true;
    return false;
  }

}