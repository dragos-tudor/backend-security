
using System.Text.Json.Serialization;

namespace Security.Conformance.OpenIdConnect;

sealed class OpenIdBrowser
{
  public string[] BrowserApiRequests { get; init; } = [];
  public string[] Runners { get; init; } = [];
  [JsonPropertyName("show_qr_code")]
  public bool ShowQrCode { get; init; }
  public string[] Visited { get; init; } = [];
  public string[] VisitedUrlsWithMethod { get; init; } = [];
  public string[] Urls { get; init; } = [];
  public string[] UrlsWithMethod { get; init; } = [];
}