//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace CameraFilterAPI.Models
//{
//	public class Custom : ImageFilter
//	{
//		public static string FilterName => "Custom";
//		public static List<Named<Type>> NamedParameterTypes { get; } = new List<Named<Type>>();
//		public Func<Bitmap, Bitmap> OwnFilter { get; }

//		static Custom()
//		{
//			AddNamedParameterType<Func<Bitmap, Bitmap>>("Own filter");
//		}

//		private static void AddNamedParameterType<T>(string name)
//		{
//			NamedParameterTypes.Add(new Named<Type>(name, typeof(T)));
//		}

//		public Custom(Func<Bitmap, Bitmap> ownFilter)
//		{
//			OwnFilter = ownFilter;
//		}

//		public Custom(params JsonElement[] parameters)
//		{
//			throw new NotImplementedException();
//		}

//		public override Bitmap Filter(Bitmap originalBitmap)
//		{
//			return OwnFilter(originalBitmap);
//		}
//	}
//}
