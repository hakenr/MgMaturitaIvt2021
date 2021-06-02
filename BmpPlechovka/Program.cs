using System;

namespace BmpPlechovka
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var recipe = ReadRecipe();

			Console.Write(recipe);
		}

		private static Recipe ReadRecipe()
		{
			var recipe = new Recipe();
			recipe.InputFilePath = Console.ReadLine();
			recipe.OutputFilePath = Console.ReadLine();

			var startLine = Console.ReadLine();
			var startLineItems = startLine.Split(' ');
			recipe.StartX = int.Parse(startLineItems[0]);
			recipe.StartY = int.Parse(startLineItems[1]);

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
