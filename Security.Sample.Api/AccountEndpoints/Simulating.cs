using System.Threading.Tasks;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static Task SimulateLongProcess (int delay) => Task.Delay(delay);
}