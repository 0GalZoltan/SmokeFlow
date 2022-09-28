using SmokeFlow;

namespace SmokeFlowTests
{
	[TestClass]
	public class StringTest
	{
		[TestMethod]
		public void ReadInts()
		{
			int[] ints = FileIO.ReadInts( "1234567891" );
			Assert.AreEqual( ints[0], 1 );
			Assert.AreEqual( ints[1], 2 );
			Assert.AreEqual( ints[2], 3 );
			Assert.AreEqual( ints[3], 4 );
			Assert.AreEqual( ints[4], 5 );
			Assert.AreEqual( ints[5], 6 );
			Assert.AreEqual( ints[6], 7 );
			Assert.AreEqual( ints[7], 8 );
			Assert.AreEqual( ints[8], 9 );
			Assert.AreEqual( ints[9], 1 );
		}
		[TestMethod]
		[ExpectedException( typeof( FormatException ) )]
		public void TryToReadLetterAsInt()
		{
			int[] ints = FileIO.ReadInts( "12345b78910" );
		}
	}
}
