using System.Net.Http;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class OpenIdConnectDeps<TOptions>(
  HttpClient HttpClient,
  PropertiesDataFormat PropertiesDataFormat,
  StringDataFormat StringDataFormat,
  TimeProvider TimeProvider)
    where TOptions : OpenIdConnectOptions;