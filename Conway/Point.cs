using System;

namespace Conway
{
	// Stores a point with integer X and Y coordinates.
	// 
	// Using a struct here since a value type makes the most sense for
	// a small piece of data like this.
	public readonly struct Point
	{
		public int X { get; }
		public int Y { get; }

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		// Hash by combining X and Y.
		public override int GetHashCode() => HashCode.Combine(X, Y);
		// Compare the components.
		public override bool Equals(object? obj) => obj is Point point && X == point.X && Y == point.Y;
		public override string ToString() => $"({X}, {Y})";

		// Comparisons using the above Equals method.
		public static bool operator ==(Point left, Point right) => left.Equals(right);
		public static bool operator !=(Point left, Point right) => !(left == right);
		// Basic math is applied to both X and Y.
		public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
		public static Point operator -(Point left, Point right) => new Point(left.X - right.X, left.Y - right.Y);
	}
}
