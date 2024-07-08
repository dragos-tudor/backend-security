
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  static IReadOnlyList<T>? GetAllEndpointMetadata<T> (Endpoint? endpoint) where T: class => endpoint?.Metadata.GetOrderedMetadata<T>();

  static T? GetEndpointMetadata<T> (Endpoint? endpoint) where T: class => endpoint?.Metadata.GetMetadata<T>();
}