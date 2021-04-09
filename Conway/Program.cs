using System;
using System.IO;

namespace Conway
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Conway's Game of Life v1.0");
			Console.WriteLine();

			// Time it.
			var start = DateTime.Now;

			if (args.Length != 2)
			{
				Console.WriteLine("Usage: Conway.exe [file] [ticks]");
				return;
			}

			// Get tick count.
			bool res = int.TryParse(args[1], out int ticks);
			if (!res)
			{
				Console.WriteLine($"Could not parse \"{args[1]}\" for number of ticks.");
				return;
			}

			// Load the specified file.
			PointSet currentState = Load(args[0]);

			// Print out the starting layout.
			Console.WriteLine("Start:");
			Console.WriteLine(currentState);

			for (int i = 0; i < ticks; i++)
			{
				currentState = currentState.Tick();

				// Here is where you would save or display the current state.
			}

			Console.WriteLine();
			// Print out the ending layout.
			Console.WriteLine("End:");
			Console.WriteLine(currentState);
			Console.WriteLine();
			Console.WriteLine($"{ticks} tick(s) calculated, elapsed time: {DateTime.Now - start}");
		}

		// Load a layout from the specified filename.
		// File content should be a set of points in X, Y format.
		public static PointSet Load(string fileName)
		{
			var state = new PointSet();

			string[] lines = File.ReadAllLines(fileName);
			foreach (string line in lines)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					string[] vals = line.Split(',');
					if (vals.Length == 2)
					{
						bool resX = int.TryParse(vals[0].Trim(), out int x);
						if (resX)
						{
							bool resY = int.TryParse(vals[1].Trim(), out int y);
							if (resY)
								state.Add(x, y);
							else
								Console.WriteLine($"Unable to parse line \"{line}\" in \"{fileName}\"");
						}
						else
							Console.WriteLine($"Unable to parse line \"{line}\" in \"{fileName}\"");
					}
					else
						Console.WriteLine($"Unable to parse line \"{line}\" in \"{fileName}\"");
				}
			}

			return state;
		}
	}
}
