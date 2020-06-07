using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public class BlackWhite : ImageFilter
	{
		public static string FilterName => "Black white";
		public static List<Named<Type>> NamedParameterTypes { get; } = new List<Named<Type>>();

		public BlackWhite()
		{

		}

		public BlackWhite(params JsonElement[] parameters)
		{

		}

		public override Bitmap Filter(Bitmap originalBitmap)
		{
			var editedBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, originalBitmap.PixelFormat);
			for (var y = 0; y < originalBitmap.Height; y++)
			{
				for (var x = 0; x < originalBitmap.Width; x++)
				{
					var originalPixel = originalBitmap.GetPixel(x, y);
					var brightnessColor = (byte)(originalPixel.GetBrightness() * byte.MaxValue); // shortcut for (pixel.R + pixel.G + pixel.B) / 3 + slightly more refined for human eye
					editedBitmap.SetPixel(x, y, originalPixel.ForEachChannelRgb(channel => brightnessColor));
				}
			}
			return editedBitmap;
		}
	}
}
