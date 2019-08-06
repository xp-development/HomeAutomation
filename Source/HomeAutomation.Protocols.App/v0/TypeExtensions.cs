using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HomeAutomation.Protocols.App.v0
{
  public static class TypeExtensions
  {
    public static IEnumerable<PropertyInfo> GetAllProperties(this Type requestType)
    {
      if (requestType == typeof(object))
        yield break;

      foreach (var propertyInfo in GetAllProperties(requestType.BaseType))
        yield return propertyInfo;

      foreach (var propertyInfo in requestType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(x => x.SetMethod.IsPublic))
        yield return propertyInfo;
    }
  }
}