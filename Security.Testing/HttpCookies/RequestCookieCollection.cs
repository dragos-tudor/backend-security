
using Microsoft.AspNetCore.Http;

namespace Security.Testing;

public sealed class RequestCookieCollection : Dictionary<string, string>, IRequestCookieCollection {

  ICollection<string> IRequestCookieCollection.Keys => base.Keys;

  public RequestCookieCollection AddCookie (string key, string value)
  { base.Add(key, value); return this; }

}

