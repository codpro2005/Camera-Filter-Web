using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CameraFilterAPI.Models
{
	public struct Matrix
	{
		public int X { get; }
		public int Y { get; }

		public Matrix(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
