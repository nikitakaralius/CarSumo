using System.Reflection;
using Sources.Core.Exceptions;
using UnityEngine;

namespace Sources.Core.Tests.Extensions
{
	public static class MonoBehaviourExtensions
	{
		public static T TestField<T>(this MonoBehaviour monoBehaviour, string id) where T : class
		{
			FieldInfo[] fields = monoBehaviour.GetType().GetFields();
			foreach (FieldInfo field in fields)
			{
				var attribute = field.GetCustomAttribute<FindInTestAttribute>();
				if (attribute is null)
				{
					continue;
				}
				if (attribute.Id != id)
				{
					continue;
				}
				if (field is T value)
				{
					return value;
				}
			}
			throw new TestFieldNotFoundException(id);
		}
	}
}