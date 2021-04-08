using System;

namespace Conway
{
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

		public override int GetHashCode() => HashCode.Combine(X, Y);
		public override bool Equals(object? obj) => obj is Point vec && X == vec.X && Y == vec.Y;
		public override string ToString() => $"({X}, {Y})";

		public static bool operator ==(Point left, Point right) => left.Equals(right);
		public static bool operator !=(Point left, Point right) => !(left == right);
		public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
		public static Point operator -(Point left, Point right) => new Point(left.X - right.X, left.Y - right.Y);
	}
}
