using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public class Brightener : ImageFilter
	{
		public static string FilterName => "Brightener";
		public static List<Named<Type>> NamedParameterTypes { get; } = new List<Named<Type>>();
		public double BrightnessStrength { get; }
		public Color LightColor { get; }

		static Brightener()
		{
			AddNamedParameterType<double>("Brightness strength");
			AddNamedParameterType<Color>("Light color");
		}

		private static void AddNamedParameterType<T>(string name)
		{
			NamedParameterTypes.Add(new Named<Type>(name, typeof(T)));
		}

		public Brightener(double brightnessStrength, Color lightColor)
		{
			BrightnessStrength = brightnessStrength;
			LightColor = lightColor;
		}

		public Brightener(params JsonElement[] parameters)
		{
			BrightnessStrength = Convert.ToDouble(parameters[0].GetString());
			LightColor = ColorTranslator.FromHtml(parameters[1].GetString());
		}

		public override Bitmap Filter(Bitmap originalBitmap)
		{
			var editedBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, originalBitmap.PixelFormat);
			var brightenerFunction = (Func<byte, byte, byte>)((channel, lightChannel) => (byte)(channel + (lightChannel - channel) * BrightnessStrength));
			for (var y = 0; y < originalBitmap.Height; y++)
			{
				for (var x = 0; x < originalBitmap.Width; x++)
				{
					editedBitmap.SetPixel(x, y, originalBitmap.GetPixel(x, y).ForEachChannelRgb(LightColor, brightenerFunction));
				}
			}
			return editedBitmap;
		}
	}
}
