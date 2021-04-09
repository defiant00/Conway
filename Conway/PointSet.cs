using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conway
{
	// Container class of all points that are currently alive.
	public class PointSet : HashSet<Point>
	{
		// Create and add a point to the collection.
		public void Add(int x, int y) => Add(new Point(x, y));
		// Check whether the specified point exists in the collection.
		public bool Contains(int x, int y) => Contains(new Point(x, y));

		// Set of 8 point offsets that border a point, used to check
		// how many neighbors are alive.
		private static readonly List<Point> Offsets = new()
		{
			new Point(-1, -1),
			new Point(0, -1),
			new Point(1, -1),
			new Point(-1, 0),
			new Point(1, 0),
			new Point(-1, 1),
			new Point(0, 1),
			new Point(1, 1),
		};

		// Execute a single tick.
		//
		// This has no side effects, and returns a PointSet object
		// that is the new state.
		public PointSet Tick()
		{
			// We need to check all current points and those surrounding them.
			// This takes advantage of HashSets not allowing duplicates.
			var pointsToCheck = new PointSet();
			foreach (var point in this)
			{
				pointsToCheck.Add(point);
				foreach (var offset in Offsets)
					pointsToCheck.Add(point + offset);
			}

			var newState = new PointSet();

			// Check the points, adding those that are alive to the new state.
			foreach (var point in pointsToCheck)
			{
				bool isAlive = Contains(point);
				int numNeighbors = Offsets.Count(o => Contains(point + o));

				if ((isAlive && numNeighbors == 2) || numNeighbors == 3)
					newState.Add(point);
			}

			return newState;
		}

		// Print the state. Format is:
		// (x, y) coordinates of the top left item.
		// A grid, X for alive and _ for dead.
		public override string ToString()
		{
			if (Count == 0)
				return "(empty)";

			// Initialize to values that are guaranteed to be replaced.
			int minX = int.MaxValue;
			int maxX = int.MinValue;
			int minY = int.MaxValue;
			int maxY = int.MinValue;

			// Find the bounding box that contains all points.
			foreach (var point in this)
			{
				minX = Math.Min(minX, point.X);
				maxX = Math.Max(maxX, point.X);
				minY = Math.Min(minY, point.Y);
				maxY = Math.Max(maxY, point.Y);
			}

			// Padding.
			minX--;
			maxX++;
			minY--;
			maxY++;

			// Simple string display.
			// (x, y) of top left of what is being displayed.
			// X for alive, _ for dead.
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
