## ASPNET Core security [wip].

### Schemes
- schemes options: cookie, jwt, facebook, google, twitter, identity server, oauth, oidc.
- authentication api: signin, signout, (remote) authenticate, challenge, forbid.
- scheme paths: login, logout, acces denied, callback url.

### Services
- cookie services: cookie manager, data provider, ticket date format.
- remote services: backend channel, data provider, state date format.

### Schemes handlers
- manual signin, remote handler.
- manual signout, openid handler.
- auth middleware -> authenticate request, authenticate.
- authorization middleware -> challenge.
- authorization middleware -> forbid.
