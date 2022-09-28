namespace SmokeFlow
{
	/// <summary>
	/// A class to store a 2-dimensional int array with fixed y size
	/// </summary>
	public class IntField2d
	{
		public struct Coordinate : IComparable<Coordinate>
		{
			public int x;
			public int y;
			public int CompareTo( Coordinate other )
			{
				if( x > other.x )
					return 1;

				if( x < other.x )
					return -1;

				if( y > other.y )
					return 1;

				if( y < other.y )
					return -1;

				return 0;
			}
		}
		protected List<int[]> Values { get; set; }
		public int Y { get; protected set; }
		public int DefaultValue { get; protected set; }
		/// <param name="y">Length of a line</param>
		/// <param name="defaultValue">Used to fill created uncomplete lines</param>
		public IntField2d( int y, int defaultValue )
		{
			Values = new List<int[]>();
			DefaultValue = defaultValue;
			Y = y;
		}
		public int this[ int x, int y ]
		{
			get
			{
				if( Values.Count < x )
					throw new IndexOutOfRangeException(); 

				return Values[x][y];
			}
			set
			{
				while( Values.Count < x )
				{
					Values.Add( new int[y] );
					for( int pos = 0; pos < Y; pos++ )
						Values[x][pos] = DefaultValue;
				}

				Values[x][y] = value;
			}
		}
		/// <summary>
		/// Adds a line to the value field with length specified in Y property.
		/// If the parameter array is longer, the method cuts the end of it.
		/// If it is shorter, the method fills te missing fields with the default value.
		/// </summary>
		/// <param name="line"></param>
		public void AddLine( int[] line )
		{
			int[] newLine = new int[Y];
			Values.Add( newLine );
			int pos = 0;

			for( ; pos < line.Length && pos < Y; pos++ )
				newLine[pos] = line[pos];

			for( ; pos < Y; pos++ )
				newLine[pos] = DefaultValue;
		}
		/// <summary>
		/// returns points list where the values of the neighporing area around the point is larger than the point's value.
		/// </summary>
		/// <returns>List of {x, y} coordinates</returns>
		public List<Coordinate> GetLocalMinimumPoints()
		{
			List<Coordinate>minimumPoints = new List<Coordinate>();
			List<Coordinate>potentialMinimumPoints = new List<Coordinate>();

			for( int x = 0; x < Values.Count; x++ )
			{
				for(  int y = 0; y < Y; y++ )
				{
					if( HasSmallerOrEqualNeighbor(x, y) == false )
					{
						minimumPoints.Add( new Coordinate { x = x, y = y });
					}
					else if( HasSmallerNeighbor(x, y) == false )
					{
						potentialMinimumPoints.Add( new Coordinate { x = x, y = y });
					}
				}
			}

			HandlePontentialMinimumPoints( potentialMinimumPoints, minimumPoints );

			minimumPoints.Sort();

			return minimumPoints;
		}
		/// <summary>
		/// Returns true if at least one of the neightbor of the specified point is smaller or equal.
		/// </summary>
		/// <exception cref="IndexOutOfRangeException"></exception>
		protected bool HasSmallerOrEqualNeighbor( int x, int y )
		{
			if( x < 0 || y < 0 || x >= Values.Count || y > Y )
				throw new IndexOutOfRangeException();

			if( x > 0 && Values[x][y] >= Values[x - 1][y] )
				return true;

			if( y > 0 && Values[x][y] >= Values[x][y - 1] )
				return true;

			if( x < Values.Count - 1 && Values[x][y] >= Values[x + 1][y] )
				return true;

			if( y < Y - 1 && Values[x][y] >= Values[x][y + 1] )
				return true;

			return false;
		}
		/// <summary>
		/// Returns true if at least one of the neightbor of the specified point is smaller.
		/// </summary>
		/// <exception cref="IndexOutOfRangeException"></exception>
		protected bool HasSmallerNeighbor( int x, int y )
		{
			if( x < 0 || y < 0 || x >= Values.Count || y > Y )
				throw new IndexOutOfRangeException();

			if( x > 0 && Values[x][y] > Values[x - 1][y] )
				return true;

			if( y > 0 && Values[x][y] > Values[x][y - 1] )
				return true;

			if( x < Values.Count - 1 && Values[x][y] > Values[x + 1][y] )
				return true;

			if( y < Y - 1 && Values[x][y] > Values[x][y + 1] )
				return true;

			return false;
		}
		/// <summary>
		/// Checks the list of points have neighbor with same value and don't have neighbor with lower value.
		/// Where the neighbor don't have lower neighbor, the points will be added to the minimum list
		/// </summary>
		/// <param name="potentialMinimumPoints">List of points have neighbor with same value</param>
		/// <param name="minimumPoints">List of minimum points</param>
		protected void HandlePontentialMinimumPoints( List<Coordinate> potentialMinimumPoints, List<Coordinate> minimumPoints )
		{
			while( potentialMinimumPoints.Count > 0 )
			{
				Coordinate coordinate = potentialMinimumPoints[0];

				if( IsMinimumPoint( coordinate, potentialMinimumPoints, minimumPoints ) )
					minimumPoints.Add( coordinate );

				potentialMinimumPoints.Remove( coordinate );
			}
		}
		/// <summary>
		/// Checks one point from the potential minimum array that is mimimum point or not
		/// </summary>
		/// <param name="coordinate">The checked point's coordinate</param>
		/// <param name="potentialMinimumPoints">list of points have neighbor with equal neighbor</param>
		/// <param name="minimumPoints">identified minimum points</param>
		/// <returns></returns>
		protected bool IsMinimumPoint( Coordinate coordinate, List<Coordinate> potentialMinimumPoints, List<Coordinate> minimumPoints )
		{
			if( coordinate.x > 0 && this[coordinate.x, coordinate.y] == this[coordinate.x - 1, coordinate.y] )
			{
				if( minimumPoints.Exists( ( Coordinate coordinateInArray ) => coordinate.x - 1 == coordinateInArray.x && coordinate.y == coordinateInArray.y ) )
					return true;
				else if( !potentialMinimumPoints.Exists( ( Coordinate coordinateInArray ) => coordinate.x - 1 == coordinateInArray.x && coordinate.y == coordinateInArray.y ) )
					return false;
			}

			if( coordinate.y > 0 && this[coordinate.x, coordinate.y] == this[coordinate.x, coordinate.y - 1] )
			{
				if( minimumPoints.Exists( ( Coordinate coordinateInArray ) => coordinate.x == coordinateInArray.x && coordinate.y - 1 == coordinateInArray.y ) )
					return true;
				else if( !potentialMinimumPoints.Exists( ( Coordinate coordinateInArray ) => coordinate.x == coordinateInArray.x && coordinate.y - 1 == coordinateInArray.y ) )
					return false;
			}

			bool needToCheckRightNeighbor = false;

			if( coordinate.x < Values.Count - 1 && this[coordinate.x, coordinate.y] == this[coordinate.x + 1, coordinate.y] )
			{
				if( minimumPoints.Exists( ( Coordinate coordinateInArray ) => coordinate.x + 1 == coordinateInArray.x && coordinate.y == coordinateInArray.y ) )
					return true;
				else if( !potentialMinimumPoints.Exists( ( Coordinate coordinateInArray ) => coordinate.x + 1 == coordinateInArray.x && coordinate.y == coordinateInArray.y ) )
					return false;
				else
					needToCheckRightNeighbor = true;
			}

			bool bNeedToCheckDownNeightbor = false;

			if( coordinate.y < Y - 1 && this[coordinate.x, coordinate.y] == this[coordinate.x, coordinate.y + 1] )
			{
				if( minimumPoints.Exists( ( Coordinate coordinateInArray ) => coordinate.x == coordinateInArray.x && coordinate.y + 1 == coordinateInArray.y ) )
					return true;
				else if( !potentialMinimumPoints.Exists( ( Coordinate coordinateInArray ) => coordinate.x == coordinateInArray.x && coordinate.y + 1 == coordinateInArray.y ) )
					return false;
				else
					bNeedToCheckDownNeightbor = true;
			}

			if( needToCheckRightNeighbor && !IsMinimumPoint( potentialMinimumPoints.Find( ( Coordinate coordinateInArray ) => coordinate.x + 1 == coordinateInArray.x && coordinate.y == coordinateInArray.y ), potentialMinimumPoints, minimumPoints ) )
				return false;

			if( bNeedToCheckDownNeightbor && !IsMinimumPoint( potentialMinimumPoints.Find( ( Coordinate coordinateInArray ) => coordinate.x == coordinateInArray.x && coordinate.y + 1 == coordinateInArray.y ), potentialMinimumPoints, minimumPoints ) )
				return false;

			return true;
		}
	}
}
