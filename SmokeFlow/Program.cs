using SmokeFlow;
Console.WriteLine( "Start of SmokeFlow" );
string[] commandLineArgs = Environment.GetCommandLineArgs();

string? inputFile = "";
string outputFile = "";
bool exit = false;
bool useOutputFile = false;

if( commandLineArgs.Length > 1 )
{
	inputFile = commandLineArgs[1];
	exit = true;
}

if( commandLineArgs.Length > 2 )
{
	outputFile = commandLineArgs[2];
	useOutputFile = true;
}

do
{
	if( string.IsNullOrEmpty( inputFile ) )
	{
		Console.Write( "Input file: " );
		inputFile = Console.ReadLine();
		if( inputFile is null )
			inputFile = "";
	}

	try
	{
		IntField2d intField = FileIO.ReadFieldFromFile( inputFile );

		if( useOutputFile )
			FileIO.WriteMinimumPointsToFile( outputFile, intField );
		else
			FileIO.WriteMinimumPointsToConsole( intField );

		int sumRiskLevel = 0;

		foreach( IntField2d.Coordinate coordinate in intField.GetLocalMinimumPoints() )
		{
			sumRiskLevel += intField[coordinate.x, coordinate.y] + 1;
		}

		Console.WriteLine( "Sum risk level: {0}", sumRiskLevel );
	}
	catch( ArgumentException )
	{
		exit = true;
	}
	catch( FileNotFoundException )
	{
		Console.WriteLine( "The specified file not found." );
	}
	catch( DirectoryNotFoundException )
	{
		Console.WriteLine( "The specified directory not found." );
	}
	catch( FormatException )
	{
		Console.WriteLine( "The file format is incorrect. Please use file contains only digits." );
	}
}
while( !exit );
Console.WriteLine( "End of SmokeFlow" );