#nullable enable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Text.RegularExpressions;
using CameraFilterAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Accord.Video;
using Newtonsoft.Json;

namespace CameraFilterAPI.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class CameraFilterController : ControllerBase
	{
		private IEnumerable<Type> GetImageFilterSubclasses()
		{
			return typeof(ImageFilter).Assembly.GetTypes()
				.Where(type => type.IsSubclassOf(typeof(ImageFilter)));
		}

		[HttpGet]
		public Named<IEnumerable<Named<IEnumerable<Named<string>>>>> GetFormData()
		{
			return new Named<IEnumerable<Named<IEnumerable<Named<string>>>>>(ImageFilter.FilterName, GetImageFilterSubclasses()
				.Select(type => new Named<IEnumerable<Named<string>>>((string)type.GetTypeInfo().GetDeclaredProperty("FilterName").GetValue(null), ((List<Named<Type>>)type.GetTypeInfo().GetDeclaredProperty("NamedParameterTypes").GetValue(null))
					.Select(namedParameterType => new Named<string>(namedParameterType.Name, namedParameterType.Value.ToString())))));
		}

		[HttpPost]
		public ControlReference<string> PostFilteredMedia(int filterIndex, FilterResult filterResult)
		{
			var imageFilter = Activator.CreateInstance(GetImageFilterSubclasses().ToList()[filterIndex], filterResult.Parameters) as ImageFilter;
			T MediaBase64To<T>(Func<MemoryStream, T> action)
			{
				using var ms = new MemoryStream(Convert.FromBase64String(filterResult.MediaBase64Raw));
				return action(ms);
			}
			var mediaIsImage = true;
			try
			{
				MediaBase64To<object?>(ms =>
				{
					new Bitmap(ms);
					return null;
				});
			}
			catch
			{
				mediaIsImage = false;
			}
			if (mediaIsImage)
			{
				return MediaBase64To(ms =>
				{
					using var filteredMs = new MemoryStream();
					imageFilter.Filter(new Bitmap(ms)).Save(filteredMs, ImageFormat.Jpeg);
					return $"{filterResult.Data}{Convert.ToBase64String(filteredMs.ToArray())}";
				});
			}
			throw new NotImplementedException();
			//Accord.Math.Rational rational;
			//var originalBitmaps = new List<Bitmap>();
			//using (var videoFileReader = new VideoFileReader())
			//{
			//	videoFileReader.Open(mediaInputPath);
			//	rational = videoFileReader.FrameRate;
			//	for (var frameIndex = 0; frameIndex < videoFileReader.FrameCount; frameIndex++)
			//	{
			//		originalBitmaps.Add(videoFileReader.ReadVideoFrame());
			//	}
			//	videoFileReader.Close();
			//}
			//var firstBitmap = originalBitmaps[0];
			//var width = firstBitmap.Width;
			//var height = firstBitmap.Height;
			//using (var videoFileWriter = new VideoFileWriter())
			//{
			//	videoFileWriter.Open(mediaOutputPath, width, height, rational, VideoCodec.H264);
			//	VideoService.FilterImageSequence(originalBitmaps, imageFilter).ForEach(videoFileWriter.WriteVideoFrame);
			//	videoFileWriter.Close();
			//}
			}
		public class FilterResult
		{
			private static Regex _base64Regex { get; } = new Regex("^(?<data>data:(?<mimeType>image/[a-z]+);base64,)(?<base64raw>[a-zA-Z0-9+/]+={0,2})$");
			public JsonElement[] Parameters { get; set; }
			public string MediaBase64 { get; set; }
			public string Data => GetValueFromBase64RegexGroup("data");
			public string MimeType => GetValueFromBase64RegexGroup("mimeType");
			public string MediaBase64Raw => GetValueFromBase64RegexGroup("base64raw");

			public string GetValueFromBase64RegexGroup(string groupName)
			{
				return _base64Regex.Match(MediaBase64).Groups[groupName].Value;
			}
		}
	}
	public class Reference<T>
	{
		private T Value { get; }

		private Reference(T value)
		{
			Value = value;
		}

		public static implicit operator Reference<T>(T value)
		{
			return new Reference<T>(value);
		}

		public static implicit operator T(Reference<T> reference)
		{
			return reference.Value;
		}
	}

	public class ControlReference<T>
	{
		public T Value { get; }

		private ControlReference(T value)
		{
			Value = value;
		}

		public static implicit operator ControlReference<T>(T value)
		{
			return new ControlReference<T>(value);
		}

		public static implicit operator T(ControlReference<T> reference)
		{
			return reference.Value;
		}
	}
}
