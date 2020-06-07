using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public class Named<T>
	{
		public string Name { get; }
		public T Value { get; set; }

		public Named(string name, T value)
		{
			Name = name;
			Value = value;
		}

		public static implicit operator Named<object>(Named<T> named)
		{
			return new Named<object>(named.Name, named.Value);
		}
	}
}
