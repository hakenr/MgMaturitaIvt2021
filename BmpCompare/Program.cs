using System;
using System.Drawing;

namespace BmpCompare
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using var image1 = new Bitmap(args[0]);
			using var image2 = new Bitmap(args[1]);

			if ((image1.Width != image2.Width) || (image1.Height != image2.Height))
			{
				Environment.Exit(-1);
			}

			for (int x = 0; x < image1.Width; x++)
			{
				for (int y = 0; y < image1.Height; y++)
				{
					if (image1.GetPixel(x, y) != image2.GetPixel(x, y))
					{
						Environment.Exit(-2);
					}
				}
			}
		}
	}
}
