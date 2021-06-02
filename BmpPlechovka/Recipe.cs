using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpPlechovka
{
    public record Recipe
    {
		public string InputFilePath { get; set; }
		public string OutputFilePath { get; set; }

		public int StartX { get; set; }
		public int StartY { get; set; }

		public int TargetR { get; set; }
		public int TargetG { get; set; }
		public int TargetB { get; set; }

		public int Sensitivity { get; set; }
	}
}
