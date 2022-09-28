using SmokeFlow;

namespace SmokeFlowTests
{
	[TestClass]
	public class IntFieldTests
	{
		[TestMethod]
		public void FindMinimumSimpleCase()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 4, 5, 6 } );
			intField.AddLine( new int[] { 4, 3, 4, 5 } );
			intField.AddLine( new int[] { 5, 4, 5, 6 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 1 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 1 );
			Assert.AreEqual( coordinate.y, 1 );
		}

		[TestMethod]
		public void FindMinimumOnLeftSide()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 4, 5, 6 } );
			intField.AddLine( new int[] { 2, 3, 4, 5 } );
			intField.AddLine( new int[] { 5, 4, 5, 6 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 1 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 1 );
			Assert.AreEqual( coordinate.y, 0 );
		}
		[TestMethod]
		public void FindMinimumOnRightSide()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 4, 5, 6 } );
			intField.AddLine( new int[] { 4, 3, 2, 1 } );
			intField.AddLine( new int[] { 5, 4, 5, 6 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 1 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 1 );
			Assert.AreEqual( coordinate.y, 3 );
		}
		[TestMethod]
		public void FindMinimumOnFirstLine()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 2, 5, 6 } );
			intField.AddLine( new int[] { 4, 3, 3, 4 } );
			intField.AddLine( new int[] { 5, 4, 5, 6 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 1 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 0 );
			Assert.AreEqual( coordinate.y, 1 );
		}
		[TestMethod]
		public void FindMinimumOnLasttLine()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 2, 5, 6 } );
			intField.AddLine( new int[] { 4, 1, 3, 4 } );
			intField.AddLine( new int[] { 5, 0, 5, 6 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 1 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 2 );
			Assert.AreEqual( coordinate.y, 1 );
		}
		[TestMethod]
		public void Find2MinimumPoints()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 2, 5, 6 } );
			intField.AddLine( new int[] { 4, 3, 3, 4 } );
			intField.AddLine( new int[] { 5, 0, 5, 6 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 2 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 0 );
			Assert.AreEqual( coordinate.y, 1 );
			coordinate = listMinimums[1];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 2 );
			Assert.AreEqual( coordinate.y, 1 );
		}
		[TestMethod]
		public void Find2NeighbourMinimumCentral()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 6, 5, 6 } );
			intField.AddLine( new int[] { 4, 3, 3, 4 } );
			intField.AddLine( new int[] { 5, 6, 5, 6 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 2 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 1 );
			Assert.AreEqual( coordinate.y, 1 );
			coordinate = listMinimums[1];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 1 );
			Assert.AreEqual( coordinate.y, 2 );
		}
		[TestMethod]
		public void Find2NeighbourMinimumLeftOnFirstLine()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 1, 1, 5, 6 } );
			intField.AddLine( new int[] { 4, 3, 3, 4 } );
			intField.AddLine( new int[] { 5, 6, 5, 6 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 2 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 0 );
			Assert.AreEqual( coordinate.y, 0 );
			coordinate = listMinimums[1];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 0 );
			Assert.AreEqual( coordinate.y, 1 );
		}
		[TestMethod]
		public void Find2NeighbourMinimumRightOnLastLine()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 6, 5, 6 } );
			intField.AddLine( new int[] { 4, 3, 3, 4 } );
			intField.AddLine( new int[] { 5, 4, 2, 2 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 2 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 2 );
			Assert.AreEqual( coordinate.y, 2 );
			coordinate = listMinimums[1];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 2 );
			Assert.AreEqual( coordinate.y, 3 );
		}
		[TestMethod]
		public void FindNeighbourMinimumsCross()
		{
			IntField2d intField = new IntField2d( 4, 0 );
			intField.AddLine( new int[] { 5, 6, 5, 6 } );
			intField.AddLine( new int[] { 4, 3, 1, 4 } );
			intField.AddLine( new int[] { 5, 1, 1, 1 } );
			intField.AddLine( new int[] { 5, 4, 1, 2 } );
			intField.AddLine( new int[] { 5, 4, 2, 2 } );
			List<IntField2d.Coordinate> listMinimums = intField.GetLocalMinimumPoints();
			Assert.AreEqual( listMinimums.Count, 5 );
			IntField2d.Coordinate coordinate = listMinimums[0];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 1 );
			Assert.AreEqual( coordinate.y, 2 );
			coordinate = listMinimums[1];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 2 );
			Assert.AreEqual( coordinate.y, 1 );
			coordinate = listMinimums[2];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 2 );
			Assert.AreEqual( coordinate.y, 2 );
			coordinate = listMinimums[3];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 2 );
			Assert.AreEqual( coordinate.y, 3 );
			coordinate = listMinimums[4];
			Assert.IsNotNull( coordinate );
			Assert.AreEqual( coordinate.x, 3 );
			Assert.AreEqual( coordinate.y, 2 );
		}
		[TestMethod]
		public void FillsMissingValues()
		{
			IntField2d intField = new IntField2d( 4, 3 );
			intField.AddLine( new int[] { 1, 2 } );
			Assert.AreEqual( intField[0, 2], 3 );
			Assert.AreEqual( intField[0, 3], 3 );
		}
		[TestMethod]
		[ExpectedException( typeof( IndexOutOfRangeException ) )]
		public void CutEndOfLongArray()
		{
			IntField2d intField = new IntField2d( 4, 3 );
			intField.AddLine( new int[] { 1, 2, 3, 4, 5, 6, 7, 8 } );
			Assert.AreEqual( intField[0, 0], 1 );
			Assert.AreEqual( intField[0, 1], 2 );
			Assert.AreEqual( intField[0, 2], 3 );
			Assert.AreEqual( intField[0, 3], 4 );
			int iOutOfRange = intField[0, 5];
		}
	}
}