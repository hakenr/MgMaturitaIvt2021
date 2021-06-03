using System;
using System.Collections.Generic;
using System.Drawing;

namespace BmpPlechovka
{
	public class Program
	{
		private static void Main(string[] args)
		{
			var recipe = ReadRecipe();

			//var recipe = new Recipe()
			//{
			//	InputFilePath = "src/apple.bmp",
			//	OutputFilePath = "out/apple-sensitivity_010.bmp",
			//	StartX = 100,
			//	StartY = 100,
			//	TargetR = 255,
			//	TargetG = 0,
			//	TargetB = 0,
			//	Sensitivity = 10
			//};

			Console.Write(recipe);

			using var image = new Bitmap(recipe.InputFilePath);
			Fill(image, recipe.StartX, recipe.StartY, Color.FromArgb(recipe.TargetR, recipe.TargetG, recipe.TargetB), recipe.Sensitivity);
			image.Save(recipe.OutputFilePath);
		}

		private const int AlphaMark = 1;
		private static void Fill(Bitmap image, int startX, int startY, Color targetColor, int sensitivity)
		{
			var visited = new bool[image.Width, image.Height];
			var pixels = new Stack<Pixel>();

			pixels.Push(new Pixel(startX, startY, image.GetPixel(startX, startY)));
			image.SetPixel(startX, startY, targetColor);
			visited[startX, startY] = true;

			while (pixels.Count > 0)
			{
				var currentPixel = pixels.Pop();
				CheckAndPushPixel(currentPixel.X - 1, currentPixel.Y);
				CheckAndPushPixel(currentPixel.X + 1, currentPixel.Y);
				CheckAndPushPixel(currentPixel.X, currentPixel.Y - 1);
				CheckAndPushPixel(currentPixel.X, currentPixel.Y + 1);

				void CheckAndPushPixel(int x, int y)
				{
					if ((x < image.Width) && (x > 0) && (y < image.Height) && (y > 0) && !visited[x, y])
					{
						if (Transition(currentPixel.OriginalColor, image.GetPixel(x, y)) <= sensitivity)
						{
							pixels.Push(new Pixel(x, y, image.GetPixel(x, y)));
							image.SetPixel(x, y, targetColor);
							visited[x, y] = true;
						}
					}
				}
			}
		}

		private static int Transition(Color a, Color b)
		{
			return Math.Abs(a.R - b.R) + Math.Abs(a.G - b.G) + Math.Abs(a.B - b.B);
		}

		private static Recipe ReadRecipe()
		{
			var recipe = new Recipe();
			recipe.InputFilePath = Console.ReadLine();
			recipe.OutputFilePath = Console.ReadLine();

			var startLine = Console.ReadLine();
			var startLineItems = startLine.Split(' ');
			recipe.StartX = int.Parse(startLineItems[1]);
			recipe.StartY = int.Parse(startLineItems[0]);

			var targetLine = Console.ReadLine();
			var targetLineItems = targetLine.Split(' ');
			recipe.TargetR = int.Parse(targetLineItems[0]);
			recipe.TargetG = int.Parse(targetLineItems[1]);
			recipe.TargetB = int.Parse(targetLineItems[2]);

			var sensitivityLine = Console.ReadLine();
			recipe.Sensitivity = int.Parse(sensitivityLine);

			return recipe;
		}
	}
}
