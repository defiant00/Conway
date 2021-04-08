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

			var start = DateTime.Now;

			if (args.Length != 2)
			{
				Console.WriteLine("Usage: Conway.exe [file] [ticks]");
				return;
			}

			bool res = int.TryParse(args[1], out int ticks);
			if (!res)
			{
				Console.WriteLine($"Could not parse \"{args[1]}\" for number of ticks.");
				return;
			}

			State current = Load(args[0]);

			Console.WriteLine("Start:");
			Console.WriteLine(current);

			for (int i = 0; i < ticks; i++)
			{
				current = current.Tick();

				// Here is where you would store/save/display the current state.
			}

			Console.WriteLine();
			Console.WriteLine("End:");
			Console.WriteLine(current);
			Console.WriteLine();
			Console.WriteLine($"{ticks} tick(s) calculated, elapsed time: {DateTime.Now - start}");
		}

		public static State Load(string fileName)
		{
			var state = new State();

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
