using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public abstract class ImageFilter
	{
		public static string FilterName { get; } = "Image filter";
		public abstract Bitmap Filter(Bitmap originalBitmap);
	}
}
