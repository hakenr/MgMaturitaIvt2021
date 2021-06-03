using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpPlechovka
{
    public record Pixel
    {
		public int X { get; init; }
		public int Y { get; init; }
		public Color OriginalColor { get; init; }

		public Pixel(int x, int y, Color originalColor)
		{
			this.X = x;
			this.Y = y;
			this.OriginalColor = originalColor;
		}
	}
}
