
using static System.Net.WebUtility;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  const string FormTitle = "Working...";
  const string Script = "<script language=\"javascript\">window.setTimeout(function() {document.forms[0].submit();}, 0);</script>";
  const string ScriptButtonText = "Submit";
  const string ScriptDisabledText = "Script is disabled. Click Submit to continue.";

  public static string BuildHttpRequestUri(
    string uri,
    IEnumerable<KeyValuePair<string, string?>> queryParams)
  {
    var queryBuilder = new StringBuilder();
    var hasQuerySeparator = uri.Contains("?");

    foreach (var queryParam in queryParams)
    {
      if (queryParam.Value is null) continue;
      queryBuilder.Append(hasQuerySeparator? "&": "?");
      queryBuilder.Append(Uri.EscapeDataString(queryParam.Key));
      queryBuilder.Append('=');
      queryBuilder.Append(Uri.EscapeDataString(queryParam.Value));
      hasQuerySeparator = true;
    }

    return $"{uri}{queryBuilder.ToString()}";
  }

  public static string BuildHttpRequestFormPost(
    string uri,
    IEnumerable<KeyValuePair<string, string>> formParams,
    string formTitle = FormTitle,
    string script = Script,
    string scriptButtonText = ScriptButtonText,
    string scriptDisabledText = ScriptDisabledText)
  {
      var formBuilder = new StringBuilder();
      formBuilder.Append("<html><head><title>");
      formBuilder.Append(HtmlEncode(formTitle));
      formBuilder.Append("</title></head><body><form method=\"POST\" name=\"hiddenform\" action=\"");
      formBuilder.Append(HtmlEncode(uri));
      formBuilder.Append("\">");

      foreach(var formParam in formParams)
      {
          formBuilder.Append("<input type=\"hidden\" name=\"");
          formBuilder.Append(HtmlEncode(formParam.Key));
          formBuilder.Append("\" value=\"");
          formBuilder.Append(HtmlEncode(formParam.Value));
          formBuilder.Append("\" />");
      }

      formBuilder.Append("<noscript><p>");
      formBuilder.Append(HtmlEncode(scriptDisabledText));
      formBuilder.Append("</p><input type=\"submit\" value=\"");
      formBuilder.Append(HtmlEncode(scriptButtonText));
      formBuilder.Append("\" /></noscript>");
      formBuilder.Append("</form>");
      formBuilder.Append(script);
      formBuilder.Append("</body></html>");
      return formBuilder.ToString();
  }
}