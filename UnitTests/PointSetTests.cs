using Conway;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	// Tests for the PointSet class.
	[TestClass]
	public class PointSetTests
	{
		// Start		End (1 tick)
		// (empty)		(empty)
		[TestMethod]
		public void Empty()
		{
			var current = new PointSet();
			var expected = new PointSet();

			current = current.Tick();

			Assert.IsTrue(current.SetEquals(expected));
		}

		// Start		End (1 tick)
		// ___			(empty)
		// _X_
		// ___
		[TestMethod]
		public void SinglePoint()
		{
			var current = new PointSet
			{
				{0, 0},
			};
			var expected = new PointSet();

			current = current.Tick();

			Assert.IsTrue(current.SetEquals(expected));
		}

		// Start		End (1 tick)
		// ____			(empty)
		// _XX_
		// ____
		[TestMethod]
		public void TwoPoints()
		{
			var current = new PointSet
			{
				{0, 0},
				{1, 0},
			};
			var expected = new PointSet();

			current = current.Tick();

			Assert.IsTrue(current.SetEquals(expected));
		}

		// Start		Intermediate (1 tick)	End (2 ticks)
		// _____		___						_____
		// _XXX_		_X_						_XXX_
		// _____		_X_						_____
		//				_X_
		//				___
		[TestMethod]
		public void Blinker()
		{
			var current = new PointSet
			{
				{0, 0},
				{1, 0},
				{2, 0},
			};
			var intermediate = new PointSet
			{
				{1, -1},
				{1, 0},
				{1, 1},
			};
			var end = new PointSet
			{
				{0, 0},
				{1, 0},
				{2, 0},
			};

			current = current.Tick();
			Assert.IsTrue(current.SetEquals(intermediate));

			current = current.Tick();
			Assert.IsTrue(current.SetEquals(end));
		}

		// Start		End (1 tick)
		// ____			____
		// _XX_			_XX_
		// _XX_			_XX_
		// ____			____
		[TestMethod]
		public void Block()
		{
			var current = new PointSet
			{
				{0, 0},
				{1, 0},
				{0, 1},
				{1, 1},
			};
			var expected = new PointSet
			{
				{0, 0},
				{1, 0},
				{0, 1},
				{1, 1},
			};

			current = current.Tick();

			Assert.IsTrue(current.SetEquals(expected));
		}

		// Start		End (16 ticks)
		// _____		_____
		// __X__		__X__
		// ___X_		___X_
		// _XXX_		_XXX_
		// _____		_____
		[TestMethod]
		public void Glider()
		{
			var current = new PointSet
			{
				{1, 0},
				{2, 1},
				{0, 2},
				{1, 2},
				{2, 2},
			};
			var expected = new PointSet
			{
				{5, 4},
				{6, 5},
				{4, 6},
				{5, 6},
				{6, 6},
			};

			// Glider cycles every 4 ticks and moves by (1, 1) every cycle.
			for (int i = 0; i < 16; i++)
				current = current.Tick();

			Assert.IsTrue(current.SetEquals(expected));
		}

		// Start					End (1 tick)
		// (min and max corners)	(min and max corners)
		//
		// Integers wrap on overflow, so this should act like a block.
		[TestMethod]
		public void Bounds()
		{
			var current = new PointSet
			{
				{int.MinValue, int.MinValue},
				{int.MinValue, int.MaxValue},
				{int.MaxValue, int.MinValue},
				{int.MaxValue, int.MaxValue},
			};
			var expected = new PointSet()
			{
				{int.MinValue, int.MinValue},
				{int.MinValue, int.MaxValue},
				{int.MaxValue, int.MinValue},
				{int.MaxValue, int.MaxValue},
			};

			current = current.Tick();

			Assert.IsTrue(current.SetEquals(expected));
		}
	}
}
