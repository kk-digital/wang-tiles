using System.Diagnostics;

namespace Wang
{
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
            Console.WriteLine("Statistics");
            Console.WriteLine($"Board Size {newBoard.Width} by {newBoard.Height}");
            Console.WriteLine("Number of colors is "+ numOfColors);
            Console.WriteLine("Tileset length is "+ newBoard.TileSet[0].Tiles.Length);
            Console.WriteLine("Number of flips is "+ numberOfFlips);
            Console.WriteLine("Energy is "+ numberOfMismatch);
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
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
            Console.WriteLine("Statistics");
            Console.WriteLine($"Board Size {newBoard.Width} by {newBoard.Height}");
            Console.WriteLine("Number of colors is "+ numOfColors);
            Console.WriteLine("Tileset length is "+ newBoard.TileSet[0].Tiles.Length);
            Console.WriteLine("Number of flips is "+ numberOfFlips);
            Console.WriteLine("Energy is "+ numberOfMismatch);
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }

        public void WeightedProbability_V1(int width, int height, int numOfColors,string outputName)
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
                    float[] probabilityVector = newBoard.GetProbabilityVector(randomPos.col,randomPos.row,tileSetID);
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
            newPic.SavePNG(newBoard, outputName+".png");

            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Statistics");
            Console.WriteLine($"Board Size {newBoard.Width} by {newBoard.Height}");
            Console.WriteLine("Number of colors is "+ numOfColors);
            Console.WriteLine("Tileset length is "+ newBoard.TileSet[0].Tiles.Length);
            Console.WriteLine("Number of flips is "+ numberOfFlips);
            Console.WriteLine("Energy is "+ numberOfMismatch);
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }

        public void TestAlgo_V1(int width, int height, int numOfColors,string outputName)
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
          
            // Default tilesetID to use for now
            int tileSetID = 0;

            // Get Random tile to put
            // Random random = new Random();
            (int col,int row) slotPos;

            // start left to right
            slotPos = (col:0,row:0);

            while (true){
                // check if there are mismatches
                bool isThereMismatch = newBoard.IsThereMismatch(slotPos.col,slotPos.row);
   
                // place random tile if there is
                if (isThereMismatch){
                    float[] probabilityVector = newBoard.GetProbabilityVector(slotPos.col,slotPos.row,tileSetID);
                    TileWeight[] sortedTileWeight = newBoard.SortTileWeight(probabilityVector);

                    float[] normalizedProbabilityVector= newBoard.GetNormalizedProbabilityVector(probabilityVector);
                    TileWeight[] sortedNormalizedTileWeight = newBoard.SortTileWeight(normalizedProbabilityVector);

                    int[] tileSetMismatches = newBoard.GetTileSetMismatches(slotPos.col,slotPos.row,tileSetID);
                    TileMismatch[] sortedTileSetMismatches = newBoard.SortTileMismatches(tileSetMismatches);
                    

                    float[] cumulativeProbabilityVector = newBoard.GetCumulativeProbabilityVector(normalizedProbabilityVector);
                    int tileID = newBoard.ChooseTileIndexFromCumulativeProbabilityVector(cumulativeProbabilityVector);

           
                    
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

                    // place selected tile
                    newBoard.PlaceTile(tileSetID, tileID,slotPos.col,slotPos.row);

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

                slotPos = newBoard.GetNextTileSlot(slotPos.col,slotPos.row);
                if (slotPos.col==newBoard.Height && slotPos.row==0 ){
                    break;
                }  
            }

  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, outputName+".png");

            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Statistics");
            Console.WriteLine($"Board Size {newBoard.Width} by {newBoard.Height}");
            Console.WriteLine("Number of colors is "+ numOfColors);
            Console.WriteLine("Tileset length is "+ newBoard.TileSet[0].Tiles.Length);
            Console.WriteLine("Number of flips is "+ numberOfFlips);
            Console.WriteLine("Energy is "+ numberOfMismatch);
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }

        public void TestAlgo_V2(int width, int height, int numOfColors,string outputName)
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
          
            // Default tilesetID to use for now
            int tileSetID = 0;

            // // Get Random tile to put
            // Random random = new Random();
            (int col,int row) slotPos;

            slotPos = (col:0,row:0);

            while (true){
                // check if there are mismatches
                bool isThereMismatch = newBoard.IsThereMismatch(slotPos.col,slotPos.row);
   
                // place random tile if there is
                if (isThereMismatch){
                    float[] probabilityVector = newBoard.GetProbabilityVector_TestAlgo_V4(slotPos.col,slotPos.row,tileSetID);
                    TileWeight[] sortedTileWeight = newBoard.SortTileWeight(probabilityVector);

                    float[] normalizedProbabilityVector= newBoard.GetNormalizedProbabilityVector(probabilityVector);
                    TileWeight[] sortedNormalizedTileWeight = newBoard.SortTileWeight(normalizedProbabilityVector);

                    int[] tileSetMismatches = newBoard.GetTileSetMismatches_TestAlgo_V4(slotPos.col,slotPos.row,tileSetID);
                    TileMismatch[] sortedTileSetMismatches = newBoard.SortTileMismatches(tileSetMismatches);
                    

                    float[] cumulativeProbabilityVector = newBoard.GetCumulativeProbabilityVector(normalizedProbabilityVector);
                    int tileID = newBoard.ChooseTileIndexFromCumulativeProbabilityVector(cumulativeProbabilityVector);
                    
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

                    // place selected tile
                    newBoard.PlaceTile(tileSetID, tileID,slotPos.col,slotPos.row);

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

                slotPos = newBoard.GetNextTileSlot(slotPos.col,slotPos.row);
                if (slotPos.col==newBoard.Height && slotPos.row==0 ){
                    break;
                }  
            }

  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, outputName+".png");

            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Statistics");
            Console.WriteLine($"Board Size {newBoard.Width} by {newBoard.Height}");
            Console.WriteLine("Number of colors is "+ numOfColors);
            Console.WriteLine("Tileset length is "+ newBoard.TileSet[0].Tiles.Length);
            Console.WriteLine("Number of flips is "+ numberOfFlips);
            Console.WriteLine("Energy is "+ numberOfMismatch);
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }
    }
}
