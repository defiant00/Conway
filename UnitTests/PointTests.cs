using Conway;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	// Tests for the Point structure.
	[TestClass]
	public class PointTests
	{
		// Test Equals and the == and != operators.
		[TestMethod]
		public void Equals()
		{
			Assert.IsTrue(new Point(1, 1).Equals(new Point(1, 1)));
			Assert.IsFalse(new Point(1, 1).Equals(new Point(2, 3)));
			Assert.IsFalse(new Point(1, 1).Equals(null));
			Assert.IsFalse(new Point(1, 1).Equals("hello"));

			Assert.IsTrue(new Point(1, 1) == new Point(1, 1));
			Assert.IsFalse(new Point(1, 1) == new Point(2, 3));

			Assert.IsTrue(new Point(1, 1) != new Point(2, 3));
			Assert.IsFalse(new Point(1, 1) != new Point(1, 1));
		}

		// Test addition and subtraction of points.
		[TestMethod]
		public void Math()
		{
			Assert.IsTrue(new Point(1, 1) + new Point(2, 3) == new Point(3, 4));
			Assert.IsTrue(new Point(2, 3) - new Point(1, 1) == new Point(1, 2));
		}
	}
}
