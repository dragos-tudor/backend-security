
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace Security.Sample.App;

partial class AppFuncs
{
  static IFileInfo GetFileInfo(string fileName) =>  new PhysicalFileInfo(new FileInfo(fileName));
}