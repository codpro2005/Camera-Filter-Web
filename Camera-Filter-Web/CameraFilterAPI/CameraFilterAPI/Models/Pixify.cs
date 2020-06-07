using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public class Pixify : ImageFilter
	{
		public static string FilterName => "Pixify";
		public static List<Named<Type>> NamedParameterTypes { get; } = new List<Named<Type>>();
		public int AmountOfPixels { get; }

		static Pixify()
		{
			AddNamedParameterType<int>("Amount of pixels");
		}

		private static void AddNamedParameterType<T>(string name)
		{
			NamedParameterTypes.Add(new Named<Type>(name, typeof(T)));
		}

		public Pixify(int amountOfPixels)
		{
			AmountOfPixels = amountOfPixels;
		}

		public Pixify(params JsonElement[] parameters)
		{
			AmountOfPixels = Convert.ToInt32(parameters[0].GetString());
		}

		public override Bitmap Filter(Bitmap originalBitmap)
		{
			var pixelGroups = new Dictionary<Matrix, List<Color>>();
			for (var y = 0; y < originalBitmap.Height; y++)
			{
				for (var x = 0; x < originalBitmap.Width; x++)
				{
					var pixelGroupsIndex = new Matrix(x / AmountOfPixels, y / AmountOfPixels);
					if (x % AmountOfPixels == 0 && y % AmountOfPixels == 0)
					{
						pixelGroups.Add(pixelGroupsIndex, new List<Color>());
					}
					pixelGroups[pixelGroupsIndex].Add(originalBitmap.GetPixel(x, y));
				}
			}
			var pixelGroupsEvaluated = new Dictionary<Matrix, Color>();
			foreach (var (pixelGroupKey, pixelGroupValue) in pixelGroups)
			{
				var summedUpGroupColor = (MathColor)Color.Black;
				pixelGroupValue.ForEach(pixel => summedUpGroupColor = summedUpGroupColor.ForEachChannelRgb(pixel, (summedUpGroupChannel, channel) => summedUpGroupChannel + channel));
				pixelGroupsEvaluated.Add(pixelGroupKey, (Color)summedUpGroupColor.ForEachChannelRgb(channel => channel / pixelGroupValue.Count));
			}
			var editedBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, originalBitmap.PixelFormat);
			for (var y = 0; y < originalBitmap.Height; y++)
			{
				for (var x = 0; x < originalBitmap.Width; x++)
				{
					editedBitmap.SetPixel(x, y, pixelGroupsEvaluated[new Matrix(x / AmountOfPixels, y / AmountOfPixels)]);
				}
			}
			return editedBitmap;
		}
	}
}
