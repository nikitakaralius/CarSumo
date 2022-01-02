#if UNITY_INCLUDE_TESTS

using System.Reflection;
using Sources.Core.Exceptions;
using UnityEngine;

namespace Sources.Core.Tests.Extensions
{
	public static class ComponentBehaviourExtensions
	{
		public static TField TestField<TField>(this Component component, string id) where TField : class
		{
			FieldInfo[] fields = component.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (FieldInfo field in fields)
			{
				var attribute = field.GetCustomAttribute<TestField>();
				if (attribute is null)
				{
					continue;
				}
				if (attribute.Id != id)
				{
					continue;
				}
				if (field.FieldType == typeof(TField))
				{
					return field.GetValue(component) as TField;
				}
			}
			throw new TestFieldNotFoundException(id);
		}
	}
}

#endif