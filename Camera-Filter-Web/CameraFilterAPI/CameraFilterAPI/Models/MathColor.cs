using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public struct MathColor
	{
		public int A { get; }
		public int R { get; }
		public int G { get; }
		public int B { get; }

		public MathColor(int a, int r, int g, int b)
		{
			A = a;
			R = r;
			G = g;
			B = b;
		}
		public MathColor(int r, int g, int b)
		{
			A = byte.MaxValue;
			R = r;
			G = g;
			B = b;
		}

		public static explicit operator Color(MathColor mathColor)
		{
			return Color.FromArgb(mathColor.A, mathColor.R, mathColor.G, mathColor.B);
		}

		public static explicit operator MathColor(Color color)
		{
			return new MathColor(color.A, color.R, color.G, color.B);
		}
	}
}
