using System.Threading.Tasks;

namespace Security.Sample.Endpoints;

partial class EndpointsFuncs
{
  static Task SimulateLongProcess (int delay) => Task.Delay(delay);
}