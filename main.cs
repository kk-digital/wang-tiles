
using CornerWangTile;
using WangTile;
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
            CornerWangTile.Generator newGeneratedBoard= new CornerWangTile.Generator();
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
            CornerWangTile.Generator newGeneratedBoard= new CornerWangTile.Generator();

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
            CornerWangTile.Generator newGeneratedBoard= new CornerWangTile.Generator();
            CornerWangTile.GeneratorOptions options = new CornerWangTile.GeneratorOptions();

            options.Width = width;
            options.Height = height;
            options.NumOfColors = colors;
            options.OutputName = outputName;

            options.TileSelectionRule = TileSelectionRule.RandomWithMismatch;
            options.EnergyCalculationMode = EnergyCalculationMode.TotalCornerMismatches;
            options.SkipUnassignedTileWithoutAdjacent = false;
            options.SelectLowestEnergy = false;
            switch (version){
                case 1:
                    newGeneratedBoard.WeightedProbability_V1(options);
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
            new Option<int>(
                name:"--tile-selection-rule",
                description:"(int) The tile selection rule to use."),
            new Option<int>(
                name:"--energy-calculation-mode",
                description:"(int) The mode for energy calculation."),
            new Option<bool>(
                name:"--skip-unassigned-tile-without-adjacent",
                description:"(int) Set true to skip unassigned tile that doesn't have any adjacent tiles."),
            new Option<bool>(
                name:"--select-lowest-energy",
                description:"(int) Set true if we want to select the tile with lowest energy."),
        };

        testAlgoCommand.Handler = CommandHandler.Create<int,int,int,int,string,int,int,bool,bool>((version,width,height,colors,outputName, tileSelectionRule, energyCalculationMode,skipUnassignedTileWithoutAdjacent,selectLowestEnergy) =>
        {
            CornerWangTile.Generator newGeneratedBoard= new CornerWangTile.Generator();
            CornerWangTile.GeneratorOptions options = new CornerWangTile.GeneratorOptions();

            options.Width = width;
            options.Height = height;
            options.NumOfColors = colors;
            options.OutputName = outputName;

            options.TileSelectionRule = (TileSelectionRule)tileSelectionRule;
            options.EnergyCalculationMode = (EnergyCalculationMode)energyCalculationMode;
            options.SkipUnassignedTileWithoutAdjacent = skipUnassignedTileWithoutAdjacent;
            options.SelectLowestEnergy = selectLowestEnergy;
           
            switch (version){
                case 1:
                    newGeneratedBoard.TestAlgo_V1(options);
                    break;
            }
        });

        var tetrisCommand = new Command("tetris")
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
            new Option<string>(
                name:"--output-name",
                description:"(string) The filename of the resulting picture (default directory is ./data)."),
            new Option<int>(
                name:"--color-matching",
                description:"(int) The color matching option to be used(0 - CurrentBitmasking, 1 - SymmetricalMatching)."),
        };

        tetrisCommand.Handler = CommandHandler.Create<int,int,int,string, int>((version,width,height,outputName, colorMatching) =>
        {
            WangTile.Generator newGeneratedBoard= new WangTile.Generator();
            switch (version){
                case 1:
                    newGeneratedBoard.TetrisBlocks_V1(width,height,outputName, (ColorMatching)colorMatching);
                    break;
            }
        });

        root.AddCommand(placementAlgoCommand);
        root.AddCommand(schoningsAlgoCommand);
        root.AddCommand(weightedProbabilityCommand);
        root.AddCommand(testAlgoCommand);
        root.AddCommand(tetrisCommand);

        root.Invoke(args);
    }



}


