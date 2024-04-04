using System.Net.Http;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public record class OAuthDeps<TOptions>(HttpClient HttpClient, PropertiesDataFormat PropertiesDataFormat, TimeProvider TimeProvider)
  where TOptions : OAuthOptions;