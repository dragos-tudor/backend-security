using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

sealed class StringSerializer : IDataSerializer<string>
{
  public string Deserialize(byte[] data) => Encoding.UTF8.GetString(data);

  public byte[] Serialize(string model) => Encoding.UTF8.GetBytes(model);

}
