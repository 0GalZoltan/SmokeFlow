namespace SmokeFlow
{
	/// <summary>
	/// Class to read IntField2d objects from file
	/// </summary>
	public class FileIO
	{
		/// <summary>
		/// Converts a string to one-digit int array
		/// </summary>
		/// <param name="ints">digits without any separation</param>
		/// <exception cref="FormatException"></exception>
		public static int[] ReadInts( string ints )
		{
			int[] result = new int[ints.Length];
			int pos = 0;
			foreach( char c in ints )
			{
				if( !char.IsDigit( c ) )
					throw new FormatException();

				result[pos++] = (int)char.GetNumericValue( c );
			}

			return result;
		}
		public static IntField2d ReadFieldFromFile( string filePath )
		{
			using StreamReader streamReader = new StreamReader( filePath );
			string? line = streamReader.ReadLine();

			if( line is null )
				return new IntField2d( 0, 0 );

			int[] intLine = ReadInts( line );
			IntField2d intField = new IntField2d( intLine.Length, 0 );
			intField.AddLine( intLine );
			line = streamReader.ReadLine();

			while( line is not null )
			{
				intLine = ReadInts( line );
				intField.AddLine( intLine );
				line = streamReader.ReadLine();
			}

			return intField;
		}
		public static void WriteMinimumPointsToFile( string filePath, IntField2d intField )
		{
			using StreamWriter streamWriter = new StreamWriter( filePath );

			foreach( IntField2d.Coordinate coordinate in intField.GetLocalMinimumPoints() )
			{
				streamWriter.WriteLine( "{0};{1};{2}", coordinate.x, coordinate.y, intField[coordinate.x, coordinate.y] + 1 );
			}
		}
		public static void WriteMinimumPointsToConsole( IntField2d intField )
		{
			foreach( IntField2d.Coordinate coordinate in intField.GetLocalMinimumPoints() )
			{
				Console.WriteLine( "{0};{1};{2}", coordinate.x, coordinate.y, intField[coordinate.x, coordinate.y] + 1 );
			}
		}
	}
}
