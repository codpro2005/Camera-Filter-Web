using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public static class ColorExtensions
	{
		public static Color ForEachChannelRgb(this Color originalColor, Func<byte, byte> channelConverter)
		{
			return Color.FromArgb(channelConverter(originalColor.R), channelConverter(originalColor.G), channelConverter(originalColor.B));
		}

		public static Color ForEachChannel(this Color originalColor, Func<byte, byte> channelConverter)
		{
			return Color.FromArgb(channelConverter(originalColor.A), channelConverter(originalColor.R), channelConverter(originalColor.G), channelConverter(originalColor.B));
		}

		public static Color ForEachChannelRgb(this Color originalColor, Color referenceChannels, Func<byte, byte, byte> channelConverter)
		{
			return Color.FromArgb(channelConverter(originalColor.R, referenceChannels.R), channelConverter(originalColor.G, referenceChannels.G), channelConverter(originalColor.B, referenceChannels.B));
		}

		public static Color ForEachChannel(this Color originalColor, Color referenceChannels, Func<byte, byte, byte> channelConverter)
		{
			return Color.FromArgb(channelConverter(originalColor.A, referenceChannels.A), channelConverter(originalColor.R, referenceChannels.R), channelConverter(originalColor.G, referenceChannels.G), channelConverter(originalColor.B, referenceChannels.B));
		}

		public static Color ForEachChannelRgb(this Color originalColor, MathColor referenceChannels, Func<byte, int, int> channelConverter)
		{
			return Color.FromArgb(channelConverter(originalColor.R, referenceChannels.R), channelConverter(originalColor.G, referenceChannels.G), channelConverter(originalColor.B, referenceChannels.B));
		}

		public static Color ForEachChannel(this Color originalColor, MathColor referenceChannels, Func<byte, int, int> channelConverter)
		{
			return Color.FromArgb(channelConverter(originalColor.A, referenceChannels.A), channelConverter(originalColor.R, referenceChannels.R), channelConverter(originalColor.G, referenceChannels.G), channelConverter(originalColor.B, referenceChannels.B));
		}

		public static MathColor ForEachChannelRgb(this MathColor originalColor, Func<int, int> channelConverter)
		{
			return new MathColor(channelConverter(originalColor.R), channelConverter(originalColor.G), channelConverter(originalColor.B));
		}

		public static MathColor ForEachChannel(this MathColor originalColor, Func<int, int> channelConverter)
		{
			return new MathColor(channelConverter(originalColor.A), channelConverter(originalColor.R), channelConverter(originalColor.G), channelConverter(originalColor.B));
		}

		public static MathColor ForEachChannelRgb(this MathColor originalColor, Color referenceChannels, Func<int, byte, int> channelConverter)
		{
			return new MathColor(channelConverter(originalColor.R, referenceChannels.R), channelConverter(originalColor.G, referenceChannels.G), channelConverter(originalColor.B, referenceChannels.B));
		}

		public static MathColor ForEachChannel(this MathColor originalColor, Color referenceChannels, Func<int, byte, int> channelConverter)
		{
			return new MathColor(channelConverter(originalColor.A, referenceChannels.A), channelConverter(originalColor.R, referenceChannels.R), channelConverter(originalColor.G, referenceChannels.G), channelConverter(originalColor.B, referenceChannels.B));
		}

		public static MathColor ForEachChannelRgb(this MathColor originalColor, MathColor referenceChannels, Func<int, int, int> channelConverter)
		{
			return new MathColor(channelConverter(originalColor.R, referenceChannels.R), channelConverter(originalColor.G, referenceChannels.G), channelConverter(originalColor.B, referenceChannels.B));
		}

		public static MathColor ForEachChannel(this MathColor originalColor, MathColor referenceChannels, Func<int, int, int> channelConverter)
		{
			return new MathColor(channelConverter(originalColor.A, referenceChannels.A), channelConverter(originalColor.R, referenceChannels.R), channelConverter(originalColor.G, referenceChannels.G), channelConverter(originalColor.B, referenceChannels.B));
		}
	}
}
