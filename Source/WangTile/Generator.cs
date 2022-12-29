using System.Diagnostics;
using System.Text;

namespace WangTile
{
 
    class Generator {
        public void TetrisBlocks_V1_GreedyPlacement(int width, int height, string outputName, ColorMatching colorMatching)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Board newBoard = new Board(height,width);
            ColorMap colorMap = new ColorMap();

            WangTileSet tileSet = Utils.GenerateTetrisTileSet(colorMap);
            newBoard.AddTileSet(tileSet);

            // Select random tile to place on first slot
            Random rand = new Random();
            int tileIndex = rand.Next(0,newBoard.TileSet[0].Tiles.Length);

            // place the random tile on the board 
            (int col, int row) pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height, rand);
            // (int col, int row) pos = (0,0);
            newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
            // Console.WriteLine($"First tile is {tileIndex}");
           

            int i=0;
            while (i<5000){
                // Console.WriteLine($"Iteration={i}");
                TetrisBlockIterate_V1(newBoard, colorMatching, rand, pos);
                // Picture newPic2 = new Picture();
                // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
                // Thread.Sleep(500);

                newBoard.RemoveTilesWithMismatches_Tetris(true, colorMatching);

                // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
                // Thread.Sleep(500);
                if (i%100==0){
                    (int col, int row) emptySlotPos = newBoard.GetEmptySlotPosition();
                    // Thread.Sleep(500);
                    if (emptySlotPos.col==newBoard.Height && emptySlotPos.row== newBoard.Width){
                        Console.WriteLine($"After Iteration[{i}], board is complete");
                        // No empty tile slots
                        break;
                    }
                    

                    // There is empty tile slot
                    // then remove the adjacent tiles
                    newBoard.RemoveAdjacentTiles(pos.col, pos.row);
                    newBoard.RemoveTilesWithMismatches_Tetris(true, colorMatching);
                }
 
                i++;
            }
     
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, colorMap, outputName+".png");

             // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }

        void TetrisBlockIterate_V1(Board newBoard,ColorMatching colorMatching, Random rand, (int col, int row) pos){
            int tileSetID = 0;

            for (int i=1; i<newBoard.TileSlots.Length*2;i++){
                // place tiles to random position with atleast 1 adjacent edge side
                pos = newBoard.FindEmptySlotWithAdjacentTilesOnEdges(rand);
                if (pos.col == newBoard.Height && pos.row == newBoard.Width){
                        // No empty tile slots
                        break;
                }
  

                int[] tileMismatches = newBoard.GetTileMismatchArray(tileSetID, pos.col, pos.row, true, colorMatching);
                TileMismatch[] tileMismatchesStruct = Utils.SortTileMismatches(tileMismatches);

                // TileProbability[] probabilityVector = newBoard.GetProbabilityVector(pos.col, pos.row, tileSetID, tileMismatchesStruct);
                // TileProbability[] normalizedProbabilityVector= newBoard.GetNormalizedProbabilityVector(probabilityVector);
                // TileProbability[] cumulativeProbabilityVector = newBoard.GetCumulativeProbabilityVector(normalizedProbabilityVector);
                // int lowestMismatchTileID = newBoard.ChooseTileIndexFromCumulativeProbabilityVector(cumulativeProbabilityVector, rand);
     
                TileMismatch[] lowestTileMismatches = newBoard.GetTilesWithLowestMismatches(tileMismatchesStruct);
                int lowestMismatchTileID = lowestTileMismatches[rand.Next(0, lowestTileMismatches.Length)].TileID;
   
                newBoard.PlaceTile(tileSetID, lowestMismatchTileID, pos.col, pos.row);
            }
        }

        public void TetrisBlocks_V2_GreedyPlacement(int width, int height, string outputName, ColorMatching colorMatching)
        {
            Stopwatch sw = Stopwatch.StartNew();

      
            var csv = new StringBuilder();

 

            Board newBoard = new Board(height,width);
            ColorMap colorMap = new ColorMap();

            WangTileSet tileSet = Utils.GenerateTetrisTileSet(colorMap);
            newBoard.AddTileSet(tileSet);

            int tileSetID = 0;
            bool useBitmasking = true;

            // Select random tile to place on first slot
            Random rand = new Random();
            int tileIndex = rand.Next(0,newBoard.TileSet[tileSetID].Tiles.Length);

            // place the random tile on the board 
            (int col, int row) pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height, rand);
            // (int col, int row) pos = (0,0);
            newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
            // Console.WriteLine($"First tile is {tileIndex}");
           

            int i=0;

            TetrisBlockIterate_V2(newBoard, colorMatching, rand, pos);
            // Picture newPic2 = new Picture();
            //     newPic2.SavePNG(newBoard, colorMap, outputName+".png");
            //     Thread.Sleep(5);
            while (i<200000){
                newBoard.ReplaceTileOrRemoveAdjacent(useBitmasking, colorMatching, rand, tileSetID);

                // if (i%50==0){
                // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
                // }

                int totalMismatch = TetrisMismatchCalculator.GetBoardTotalMismatch(newBoard, tileSetID, useBitmasking,colorMatching);
                var newLine = string.Format("{0},{1}", i, totalMismatch);
                csv.AppendLine(newLine);  

                i++;
            }

            newBoard.RemoveTilesWithMismatches_Tetris(true, colorMatching);

            // Save CSV
            File.WriteAllText("./data/Tetris_16x16_Mismatches.csv", csv.ToString());

            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, colorMap, outputName+".png");
            
             // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }

         void TetrisBlockIterate_V2(Board newBoard,ColorMatching colorMatching, Random rand, (int col, int row) pos){
            int tileSetID = 0;

            while (true){
                // place tiles to random position with atleast 1 adjacent edge side
                pos = newBoard.FindEmptySlotWithAdjacentTilesOnEdges(rand);
                if (pos.col == newBoard.Height && pos.row == newBoard.Width){
                        // No empty tile slots
                        break;
                }
  

                int[] tileMismatches = newBoard.GetTileMismatchArray(tileSetID, pos.col, pos.row, true, colorMatching);
                TileMismatch[] tileMismatchesStruct = Utils.SortTileMismatches(tileMismatches);

                TileMismatch[] lowestTileMismatches = newBoard.GetTilesWithLowestMismatches(tileMismatchesStruct);
                int lowestMismatchTileID = lowestTileMismatches[rand.Next(0, lowestTileMismatches.Length)].TileID;
            
                newBoard.PlaceTile(tileSetID, lowestMismatchTileID, pos.col, pos.row);
            }
        }

          public void TetrisBlocks_V3_Simulated_Annealing(int width, int height, string outputName, ColorMatching colorMatching, int iterations, float temperature, int lIteration, float alpha)
        {
            Stopwatch sw = Stopwatch.StartNew();

      
            var mismatchCSV = new StringBuilder();
            var temperatureCSV = new StringBuilder();

            Board newBoard = new Board(height,width);
            ColorMap colorMap = new ColorMap();

            WangTileSet tileSet = Utils.GenerateTetrisTileSet(colorMap);
            newBoard.AddTileSet(tileSet);

            int tileSetID = 0;
            bool useBitmasking = true;

            // Select random tile to place on first slot
            Random rand = new Random();
            int tileIndex = rand.Next(0,newBoard.TileSet[tileSetID].Tiles.Length);

            // place the random tile on the board 
            (int col, int row) pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height, rand);
            // (int col, int row) pos = (0,0);
            newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
            // Console.WriteLine($"First tile is {tileIndex}");
           

            int i=0;

            TetrisBlockIterate_V2(newBoard, colorMatching, rand, pos);
            // Picture newPic2 = new Picture();
            // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
            // Thread.Sleep(5);

            // initial temperature
            newBoard.Temperature=temperature;
            // Decrease temperature every L iteration
            int L = lIteration;
            while (i<iterations){

                //Generate array with N indexes
                // permute and shuffle them
                // each tile appears once in a random order
                (int col, int row)[] positionArr = new (int col, int row)[newBoard.TileSlots.Length];
                pos = (0,0);
                for (int j=0; j<newBoard.TileSlots.Length;j++){
                    tileIndex=Utils.GetBoardSlotIndex(newBoard.Width, pos.col, pos.row);
                    positionArr[tileIndex].col=pos.col;
                    positionArr[tileIndex].row=pos.row;

                    pos = Utils.GetNextTileSlot(newBoard.Width, pos.col,pos.row);
                }

                for (int j=0;j<newBoard.TileSlots.Length;j++){
                    int z = rand.Next(j,newBoard.TileSlots.Length);
                    (int col, int row) tmp = positionArr[j];
                    positionArr[j]=positionArr[z];
                    positionArr[z]=tmp;
                }
                

                for (int j=0;j<newBoard.TileSlots.Length;j++){
                    pos = positionArr[j];
                    newBoard.ReplaceTileUsingSimulatedAnnealing_Tetris(useBitmasking, colorMatching, rand, tileSetID, pos);

                }

                // if (i%50==0){
                // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
                // }

                int totalMismatch = TetrisMismatchCalculator.GetBoardTotalMismatch(newBoard, tileSetID, useBitmasking,colorMatching);
                var newLine = string.Format("{0},{1}", i, totalMismatch);
                mismatchCSV.AppendLine(newLine);  

                newLine = string.Format("{0},{1}", i, newBoard.Temperature);
                temperatureCSV.AppendLine(newLine);  

                if (i%L==0){
                    newBoard.Temperature=newBoard.UpdateTemperature(rand, alpha);
                }
                i++;
            }

            newBoard.RemoveTilesWithMismatches_Tetris(true, colorMatching);

            // Save CSV
            File.WriteAllText("./data/Tetris_16x16_Mismatches.csv", mismatchCSV.ToString());
            File.WriteAllText("./data/Tetris_16x16_Temperature.csv", temperatureCSV.ToString());

            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, colorMap, outputName+".png");
            
             // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }

        public void TetrisBlocks_V4_Simulated_Annealing_SequentialRejectionSampling(int width, int height, string outputName, ColorMatching colorMatching, int iterations, float temperature, int lIteration, float alpha)
        {
            Stopwatch sw = Stopwatch.StartNew();

      
            var mismatchCSV = new StringBuilder();
            var temperatureCSV = new StringBuilder();

            Board newBoard = new Board(height,width);
            ColorMap colorMap = new ColorMap();

            WangTileSet tileSet = Utils.GenerateTetrisTileSet(colorMap);
            newBoard.AddTileSet(tileSet);

            int tileSetID = 0;
            bool useBitmasking = true;

            // Select random tile to place on first slot
            Random rand = new Random();
            int tileIndex = rand.Next(0,newBoard.TileSet[tileSetID].Tiles.Length);

            // place the random tile on the board 
            (int col, int row) pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height, rand);
            // (int col, int row) pos = (0,0);
            newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
            // Console.WriteLine($"First tile is {tileIndex}");
           

            int i=0;

            TetrisBlockIterate_V2(newBoard, colorMatching, rand, pos);
            // Picture newPic2 = new Picture();
            // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
            // Thread.Sleep(5);

            // initial temperature
            newBoard.Temperature=temperature;
            // Decrease temperature every L iteration
            int L = lIteration;
            while (i<iterations){

                //Generate array with N indexes
                // permute and shuffle them
                // each tile appears once in a random order
                (int col, int row)[] positionArr = new (int col, int row)[newBoard.TileSlots.Length];
                pos = (0,0);
                for (int j=0; j<newBoard.TileSlots.Length;j++){
                    tileIndex=Utils.GetBoardSlotIndex(newBoard.Width, pos.col, pos.row);
                    positionArr[tileIndex].col=pos.col;
                    positionArr[tileIndex].row=pos.row;

                    pos = Utils.GetNextTileSlot(newBoard.Width, pos.col,pos.row);
                }

                for (int j=0;j<newBoard.TileSlots.Length;j++){
                    int z = rand.Next(j,newBoard.TileSlots.Length);
                    (int col, int row) tmp = positionArr[j];
                    positionArr[j]=positionArr[z];
                    positionArr[z]=tmp;
                }
                

                for (int j=0;j<newBoard.TileSlots.Length;j++){
                    pos = positionArr[j];
                    newBoard.ReplaceTileUsingSimulatedAnnealing_SequentialRejectionSampling_Tetris(useBitmasking, colorMatching, rand, tileSetID, pos);

                }

                // if (i%50==0){
                // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
                // }

                int totalMismatch = TetrisMismatchCalculator.GetBoardTotalMismatch(newBoard, tileSetID, useBitmasking,colorMatching);
                var newLine = string.Format("{0},{1}", i, totalMismatch);
                mismatchCSV.AppendLine(newLine);  

                newLine = string.Format("{0},{1}", i, newBoard.Temperature);
                temperatureCSV.AppendLine(newLine);  

                if (i%L==0){
                    newBoard.Temperature=newBoard.UpdateTemperature(rand, alpha);
                }
                i++;
            }

            newBoard.RemoveTilesWithMismatches_Tetris(true, colorMatching);

            // Save CSV
            File.WriteAllText("./data/Tetris_16x16_Mismatches.csv", mismatchCSV.ToString());
            File.WriteAllText("./data/Tetris_16x16_Temperature.csv", temperatureCSV.ToString());

            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, colorMap, outputName+".png");
            
            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }

        public void Tiled_V1_Simulated_Annealing_UsingJSONTiles(int width, int height, string outputName, ColorMatching colorMatching, int iterations, float temperature, int lIteration, float alpha)
        {
            Stopwatch sw = Stopwatch.StartNew();

      
            var mismatchCSV = new StringBuilder();
            var temperatureCSV = new StringBuilder();

            Board newBoard = new Board(height,width);
            ColorMap colorMap = new ColorMap();

            WangTileSet tileSet = Utils.GenerateTileSetFromJSON(colorMap, "./data/json/Map_Tiles_V1.tmj");
            newBoard.AddTileSet(tileSet);

            int tileSetID = 0;
            bool useBitmasking = true;

            // Select random tile to place on first slot
            Random rand = new Random();
            int tileIndex = rand.Next(0,newBoard.TileSet[tileSetID].Tiles.Length);

            // place the random tile on the board 
            (int col, int row) pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height, rand);
            // (int col, int row) pos = (0,0);
            newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
            // Console.WriteLine($"First tile is {tileIndex}");
           

            int i=0;

            TetrisBlockIterate_V2(newBoard, colorMatching, rand, pos);
            // Picture newPic2 = new Picture();
            // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
            // Thread.Sleep(5);

            // initial temperature
            newBoard.Temperature=temperature;
            // Decrease temperature every L iteration
            int L = lIteration;
            while (i<iterations){
                // Generate array with N indexes
                // permute and shuffle them
                // each tile appears once in a random order
                (int col, int row)[] positionArr = new (int col, int row)[newBoard.TileSlots.Length];
                pos = (0,0);
                for (int j=0; j<newBoard.TileSlots.Length;j++){
                    tileIndex=Utils.GetBoardSlotIndex(newBoard.Width, pos.col, pos.row);
                    positionArr[tileIndex].col=pos.col;
                    positionArr[tileIndex].row=pos.row;

                    pos = Utils.GetNextTileSlot(newBoard.Width, pos.col,pos.row);
                }

                for (int j=0;j<newBoard.TileSlots.Length;j++){
                    int z = rand.Next(j,newBoard.TileSlots.Length);
                    (int col, int row) tmp = positionArr[j];
                    positionArr[j]=positionArr[z];
                    positionArr[z]=tmp;
                }
                

                for (int j=0;j<newBoard.TileSlots.Length;j++){
                    pos = positionArr[j];
                    newBoard.ReplaceTileUsingSimulatedAnnealing_SequentialRejectionSampling(useBitmasking, colorMatching, rand, tileSetID, pos);

                }

                int totalMismatch = MismatchCalculator.GetBoardTotalMismatch(newBoard, tileSetID, useBitmasking,colorMatching);
                var newLine = string.Format("{0},{1}", i, totalMismatch);
                mismatchCSV.AppendLine(newLine);  

                newLine = string.Format("{0},{1}", i, newBoard.Temperature);
                temperatureCSV.AppendLine(newLine);  

                if (i%L==0){
                    newBoard.Temperature=newBoard.UpdateTemperature(rand, alpha);
                }
                i++;
                Console.WriteLine($"Iteration={i}");
            }

            newBoard.RemoveTilesWithMismatches(true, colorMatching);

            // Save CSV
            File.WriteAllText("./data/Tetris_16x16_Mismatches.csv", mismatchCSV.ToString());
            File.WriteAllText("./data/Tetris_16x16_Temperature.csv", temperatureCSV.ToString());

            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, colorMap, outputName+".png");
            
            // Combine tile images and save
            string[] imgDirs = Utils.GenerateImageDirsFromTileSlots(newBoard.TileSlots);
            SkiaSharpImageMerger.GenerateMapUsingGivenPictures(imgDirs, newBoard.Width, newBoard.Height, "data/" +outputName+"_Combined.png");

            // Timer stop
            sw.Stop();
            TimeSpan time = sw.Elapsed;
            Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        }
    }
}
