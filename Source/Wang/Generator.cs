using System.Diagnostics;

namespace Wang
{
    ///<Summary>
    /// GeneratorOptions struct is a struct for the needed
    /// options for our generator algorithms.
    ///</Summary>
    public struct GeneratorOptions{
        // The order of how tile slots are updated.
        // 0 - left to right, pass thorugh once only
        // 1 - random positions with mismatch
        public TileSelectionRule TileSelectionRule;

        // The mode of energy calculation
        // 
        // 0 - total number of mismatches.
        // Max mismatch is 24.
        // 
        // 1 - Count only 1 per corner if 
        // there is mismatch. 
        // Max mismatch is 4.

        public EnergyCalculationMode EnergyCalculationMode;

        // Set true to skip unassigned tile
        // that doesn't have any adjacent tiles.
        public bool SkipUnassignedTileWithoutAdjacent;

        // Set true if we want to select
        // the tile with lowest energy.
        public bool SelectLowestEnergy;   

        public int Width;
        public int Height;
        public int NumOfColors;
        public string OutputName;  
    }

    class Generator {
        public void PlacementAlgo_V1(int width, int height, int numOfColors,string outputName)
        {
            Board newBoard = new Board(height,width);
            WangCornerTileSet tileSet= newBoard.GenerateTileSet(numOfColors,1);
            newBoard.AddTileSet(tileSet);
          

            // Select random tile to place on first slot
            Random random = new Random();
            int tileIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);

            // place the random tile on the board slot
            (int col,int row) randomPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);
            newBoard.PlaceTile(0,tileIndex,randomPos.col,randomPos.row);
            
            (int col, int row) val= (randomPos.col,randomPos.row);
            for (int i=1;i<newBoard.TileSlots.Length;i++){
                val=newBoard.AddTileToAdjacent(val.col,val.row);
            }
  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, outputName+".png");

        }

        public void PlacementAlgo_V2(int width, int height, int numOfColors,string outputName)
        {   
            // Create board
            Board newBoard = new Board(height,width);

            // Generate tile sets
            WangCornerTileSet tileSet= newBoard.GenerateTileSet(numOfColors,1);
            newBoard.AddTileSet(tileSet);

            // Others tile set is for air, wall, etc
            WangCornerTileSet othersTileSet= newBoard.GenerateOthersTileSet();
            newBoard.AddTileSet(othersTileSet);
          
            // Get Random tile to put
            Random random = new Random();
            int tileIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);

            // Put first slot at upper left
            (int col, int row) val = (0,0);
            newBoard.PlaceTile(0,tileIndex,val.col,val.row);
       
            for (int i=1;i<newBoard.TileSlots.Length;i++){
                val=newBoard.AddTileToNextSlot(val.col,val.row);
            }
  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, outputName+".png");

        }

        public void SchoningsAlgo_V1(int width, int height, int numOfColors,string outputName)
        {   
            Stopwatch sw = Stopwatch.StartNew();

            int numberOfFlips = 0;
            int numberOfMismatch = 0;
            // Create board
            Board newBoard = new Board(height,width);

            // Generate tile set
            WangCornerTileSet tileSet= newBoard.GenerateTileSet(numOfColors,1);

            // Add tile set to board
            newBoard.AddTileSet(tileSet);
          
            // // Get Random tile to put
            Random random = new Random();
            (int col,int row) randomPos;


            while (true){
                // Choose random position
                randomPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);

                // check if there are mismatches
                bool isThereMismatch = newBoard.IsThereMismatch(randomPos.col,randomPos.row);

                // place random tile if there is
                if (isThereMismatch){
                    int tileIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);
                    newBoard.PlaceTile(0,tileIndex,randomPos.col,randomPos.row);
                    numberOfFlips++;
                }

                numberOfMismatch=newBoard.GetBoardTotalMismatch();
                if (numberOfMismatch==0){
                    break;
                }

                // time out after N flips
                if (numberOfFlips==1000000){
                    break;
                }
            }

  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, outputName+".png");

            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Utils.PrintResult(newBoard, numOfColors,numberOfFlips,numberOfMismatch, time);
        }

        public void SchoningsAlgo_V2(int width, int height, int numOfColors,string outputName)
        {   
            Stopwatch sw = Stopwatch.StartNew();

            int numberOfFlips = 0;
            int numberOfMismatch = 0;
            // Create board
            Board newBoard = new Board(height,width);

            // Generate tile set
            WangCornerTileSet tileSet= newBoard.GenerateTileSet(numOfColors,1);

            // Add tile set to board
            newBoard.AddTileSet(tileSet);
          
            // // Get Random tile to put
            Random random = new Random();
            (int col,int row) randomPos;

            while (true){
                // Choose random position
                randomPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);

                // check if there are mismatches
                bool isThereMismatch = newBoard.IsThereMismatch(randomPos.col,randomPos.row);

                // place random tile if there is
                if (isThereMismatch){
                    int tileIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);
                    newBoard.PlaceTile(0,tileIndex,randomPos.col,randomPos.row);
                    numberOfFlips++;

                    int currNumberOfMismatch = newBoard.GetBoardTotalMismatch();
                    if (currNumberOfMismatch > numberOfMismatch) {
                        // remove the placed tile
                        newBoard.RemoveTile(randomPos.col,randomPos.row);
                    }
                }

                numberOfMismatch=newBoard.GetBoardTotalMismatch();
                if (numberOfMismatch==0){
                    break;
                }

                // time out after N flips
                if (numberOfFlips==1000000){
                    break;
                }
            }

  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, outputName+".png");

            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Utils.PrintResult(newBoard, numOfColors,numberOfFlips,numberOfMismatch, time);
        }

        public void WeightedProbability_V1(GeneratorOptions options)
        {   
            Stopwatch sw = Stopwatch.StartNew();

            int numberOfFlips = 0;
            int numberOfMismatch = 0;
            // Create board
            Board newBoard = new Board(options.Height,options.Width);

            // Generate tile set
            WangCornerTileSet tileSet= newBoard.GenerateTileSet(options.NumOfColors,1);

            // Add tile set to board
            newBoard.AddTileSet(tileSet);
          
            // Default tilesetID to use for now
            int tileSetID = 0;

            // // Get Random tile to put
            Random random = new Random();
            (int col,int row) randomPos;

            while (true){
                // Choose random position
                randomPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);

                // check if there are mismatches
                bool isThereMismatch = newBoard.IsThereMismatch(randomPos.col,randomPos.row);
   
                // place random tile if there is
                if (isThereMismatch){
                    float[] probabilityVector = newBoard.GetProbabilityVector(randomPos.col,randomPos.row,tileSetID,options.EnergyCalculationMode);
                    float[] normalizedProbabilityVector= newBoard.GetNormalizedProbabilityVector(probabilityVector);
                    float[] cumulativeProbabilityVector = newBoard.GetCumulativeProbabilityVector(normalizedProbabilityVector);
                  
                    int tileID = newBoard.ChooseTileIndexFromCumulativeProbabilityVector(cumulativeProbabilityVector);

                    // place selected tile
                    newBoard.PlaceTile(tileSetID, tileID,randomPos.col,randomPos.row);

                    numberOfFlips++;
                     numberOfMismatch=newBoard.GetBoardTotalMismatch();
                }

                if (numberOfMismatch==0){
                    break;
                }

                // time out after N flips
                if (numberOfFlips==1000000){
                    break;
                }
            }

  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, options.OutputName+".png");

            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Utils.PrintResult(newBoard, options.NumOfColors,numberOfFlips,numberOfMismatch, time);
        }

        public void TestAlgo_V1(GeneratorOptions options)
        {   
            Stopwatch sw = Stopwatch.StartNew();

            int numberOfFlips = 0;
            int numberOfMismatch = 0;

            // Create board
            Board newBoard = new Board(options.Height,options.Width);

            // Generate tile set
            WangCornerTileSet tileSet= newBoard.GenerateTileSet(options.NumOfColors,1);

            // Add tile set to board
            newBoard.AddTileSet(tileSet);
          
            // Default tilesetID to use for now
            int tileSetID = 0;
            int tileID=0;

            (int col,int row) slotPos;
             slotPos = (col:0,row:0);

            switch (options.TileSelectionRule){
                case TileSelectionRule.LeftToRight:
                    slotPos = (col:0,row:0);
                    break;
                case TileSelectionRule.RandomWithMismatch:
                    // Choose random position
                    slotPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);
                    break;
            }

            while (true){
                // check if there are mismatches
                bool isThereMismatch = newBoard.IsThereMismatch(slotPos.col,slotPos.row);
   
                // place random tile if there is
                if (isThereMismatch){
                    float[] probabilityVector = newBoard.GetProbabilityVector(slotPos.col,slotPos.row,tileSetID, options.EnergyCalculationMode);
                    TileWeight[] sortedTileWeight = newBoard.SortTileWeight(probabilityVector);

                    float[] normalizedProbabilityVector= newBoard.GetNormalizedProbabilityVector(probabilityVector);
                    TileWeight[] sortedNormalizedTileWeight = newBoard.SortTileWeight(normalizedProbabilityVector);

                    int[] tileSetMismatches = newBoard.GetTileSetMismatches(slotPos.col,slotPos.row,tileSetID, options.EnergyCalculationMode);
                    TileMismatch[] sortedTileSetMismatches = newBoard.SortTileMismatches(tileSetMismatches);
                    

                    float[] cumulativeProbabilityVector = newBoard.GetCumulativeProbabilityVector(normalizedProbabilityVector);

                    if (options.SelectLowestEnergy){
                        tileID = sortedTileSetMismatches[0].TileID;
                    }else {
                        tileID = newBoard.ChooseTileIndexFromCumulativeProbabilityVector(cumulativeProbabilityVector);

                    }
                    
                    // place selected tile
                    newBoard.PlaceTile(tileSetID, tileID,slotPos.col,slotPos.row);

                    numberOfFlips++;
                    numberOfMismatch=newBoard.GetBoardTotalMismatch();

                    Console.Write($"\nSorted TileMismatches tileID    = ");
                    for (int i=0;i<sortedTileSetMismatches.Length;i++){
                        Console.Write($"{sortedTileSetMismatches[i].TileID}, ");
                    }
                    Console.Write($"\nSorted number of mismatch       = ");
                    for (int i=0;i<sortedTileSetMismatches.Length;i++){
                        Console.Write($"{sortedTileSetMismatches[i].NumberOfMismatches}, ");
                    }

                    Console.Write($"\n\nSorted Prenormalization Weight Vector TileID= ");
                    for (int i=0;i<sortedTileWeight.Length;i++){
                        Console.Write($"{sortedTileWeight[i].TileID}, ");
                    }

                    Console.Write($"\nSorted Prenormalization Weight Vector Weight= ");
                    for (int i=0;i<sortedTileWeight.Length;i++){
                        Console.Write($"{sortedTileWeight[i].Weight}, ");
                    }

                    Console.Write($"\n\nSorted Normalized Weight Vector TileID= ");
                    for (int i=0;i<sortedNormalizedTileWeight.Length;i++){
                        Console.Write($"{sortedNormalizedTileWeight[i].TileID}, ");
                    }

                    Console.Write($"\nSorted Normalized Weight Vector Weight= ");
                    for (int i=0;i<sortedNormalizedTileWeight.Length;i++){
                        Console.Write($"{sortedNormalizedTileWeight[i].Weight}, ");
                    }

                    Console.WriteLine("\n--------------");
                }

                if (numberOfMismatch==0){
                    break;
                }

                // time out after N flips
                if (numberOfFlips==1000000){
                    break;
                }

                // Next position
                if (options.TileSelectionRule== TileSelectionRule.LeftToRight){
                    slotPos = newBoard.GetNextTileSlot(slotPos.col,slotPos.row);
                    if (slotPos.col==newBoard.Height && slotPos.row==0 ){
                        break;
                    }  
                }else if (options.TileSelectionRule==TileSelectionRule.RandomWithMismatch){
                    // Choose random position
                    slotPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);
                    break;
                }
                    
            }

  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, options.OutputName+".png");

            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Utils.PrintResult(newBoard, options.NumOfColors,numberOfFlips,numberOfMismatch, time);
        }
    }
}
