using System;
using System.Collections.Generic;
using System.Text;

namespace Conway
{
	public class State : HashSet<Point>
	{
		public void Add(int x, int y) => Add(new Point(x, y));
		public bool Contains(int x, int y) => Contains(new Point(x, y));

		private static readonly List<Point> Comparisons = new List<Point>
		{
			new Point(-1, -1),  new Point(0, -1),   new Point(1, -1),
			new Point(-1, 0),                       new Point(1, 0),
			new Point(-1, 1),   new Point(0, 1),    new Point(1, 1),
		};

		public State Tick()
		{
			// TODO - build a list of all points to check and create the new state

			return new State();
		}

		public override string ToString()
		{
			if (Count == 0)
				return "(empty)";

			int minX = int.MaxValue;
			int maxX = int.MinValue;
			int minY = int.MaxValue;
			int maxY = int.MinValue;

			// Find the bounding box that contains all points.
			foreach (var p in this)
			{
				minX = Math.Min(minX, p.X);
				maxX = Math.Max(maxX, p.X);
				minY = Math.Min(minY, p.Y);
				maxY = Math.Max(maxY, p.Y);
			}

			// Padding.
			minX--;
			maxX++;
			minY--;
			maxY++;

			var sb = new StringBuilder();
			sb.AppendLine($"({minX}, {minY})");

			for (int y = minY; y <= maxY; y++)
			{
				for (int x = minX; x <= maxX; x++)
					sb.Append(Contains(x, y) ? "X" : "_");
				sb.AppendLine();
			}
			return sb.ToString();
		}
	}
}
