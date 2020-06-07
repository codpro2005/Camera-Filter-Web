using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public static class VideoService
	{
		public static List<Bitmap> FilterImageSequence(List<Bitmap> originalBitmaps, ImageFilter imageFilter)
		{
			return originalBitmaps.Select(imageFilter.Filter).ToList();
		}
	}
}
