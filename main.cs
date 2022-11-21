
using Wang;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.CommandLine;
using System.CommandLine.DragonFruit;
using System.CommandLine.NamingConventionBinder;

class MainClass
{
    private static async Task Main(string[] args)
    {
        var root = new RootCommand("Wang Tile Generator")
        {
        };

        var placementAlgoCommand = new Command("placement-algo")
        {
            new Option<int>(
                name:"--version",
                description:"(int) The version of Placement Algo to run (1 or 2)."),
            new Option<int>(
                name: "--width",
                description:"(int) The width of the board to be made."),
            new Option<int>(
                name:"--height",
                description:"(int) The height of the board to be made."),
            new Option<int>(
                name:"--colors",
                description:"(int) The number of colors used to generate the tile set."),
            new Option<string>(
                name:"--output-name",
                description:"(string) The filename of the resulting picture (default directory is ./data)."),
        };


       placementAlgoCommand.Handler = CommandHandler.Create<int,int,int,int,string>((version,width,height,colors,outputName) =>
        {
            Generator newGeneratedBoard= new Generator();
            switch (version){
                case 1:
                    newGeneratedBoard.PlacementAlgo_V1(width,height,colors,outputName);
                    break;
                case 2:
                    newGeneratedBoard.PlacementAlgo_V2(width,height,colors,outputName);
                    break;
            }
            
        });

        var schoningsAlgoCommand = new Command("schonings-algo")
        {
            new Option<int>(
                name:"--version",
                description:"(int) The version of Schonings Algo to run (1 or 2)."),
            new Option<int>(
                name: "--width",
                description:"(int) The width of the board to be made."),
            new Option<int>(
                name:"--height",
                description:"(int) The height of the board to be made."),
            new Option<int>(
                name:"--colors",
                description:"(int) The number of colors used to generate the tile set."),
            new Option<string>(
                name:"--output-name",
                description:"(string) The filename of the resulting picture (default directory is ./data)."),
        };

        schoningsAlgoCommand.Handler = CommandHandler.Create<int,int,int,int,string>((version,width,height,colors,outputName) =>
        {
            Generator newGeneratedBoard= new Generator();

            switch (version){
                case 1:
                    newGeneratedBoard.SchoningsAlgo_V1(width,height,colors,outputName);
                    break;
                case 2:
                    newGeneratedBoard.SchoningsAlgo_V2(width,height,colors,outputName);
                    break;
            }
        });

         var weightedProbabilityCommand = new Command("weighted-probability")
        {
            new Option<int>(
                name:"--version",
                description:"(int) The version of Weighted Probability Algo to run (1)."),
            new Option<int>(
                name: "--width",
                description:"(int) The width of the board to be made."),
            new Option<int>(
                name:"--height",
                description:"(int) The height of the board to be made."),
            new Option<int>(
                name:"--colors",
                description:"(int) The number of colors used to generate the tile set."),
            new Option<string>(
                name:"--output-name",
                description:"(string) The filename of the resulting picture (default directory is ./data)."),
        };

        weightedProbabilityCommand.Handler = CommandHandler.Create<int,int,int,int,string>((version,width,height,colors,outputName) =>
        {
            Generator newGeneratedBoard= new Generator();

            switch (version){
                case 1:
                    newGeneratedBoard.WeightedProbability_V1(width,height,colors,outputName);
                    break;
            }
        });

         var testAlgoCommand = new Command("test-algo")
        {
            new Option<int>(
                name:"--version",
                description:"(int) The version of Test Algo to run (1)."),
            new Option<int>(
                name: "--width",
                description:"(int) The width of the board to be made."),
            new Option<int>(
                name:"--height",
                description:"(int) The height of the board to be made."),
            new Option<int>(
                name:"--colors",
                description:"(int) The number of colors used to generate the tile set."),
            new Option<string>(
                name:"--output-name",
                description:"(string) The filename of the resulting picture (default directory is ./data)."),
        };

        testAlgoCommand.Handler = CommandHandler.Create<int,int,int,int,string>((version,width,height,colors,outputName) =>
        {
            Generator newGeneratedBoard= new Generator();

            switch (version){
                case 1:
                    newGeneratedBoard.TestAlgo_V1(width,height,colors,outputName);
                    break;
                case 2:
                    newGeneratedBoard.TestAlgo_V2(width,height,colors,outputName);
                break;
            }
        });


        root.AddCommand(placementAlgoCommand);
        root.AddCommand(schoningsAlgoCommand);
        root.AddCommand(weightedProbabilityCommand);
        root.AddCommand(testAlgoCommand);

        root.Invoke(args);
    }



}


