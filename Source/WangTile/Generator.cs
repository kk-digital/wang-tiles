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

                newBoard.RemoveTilesWithMismatches(true, colorMatching);

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
                    newBoard.RemoveTilesWithMismatches(true, colorMatching);
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

                int totalMismatch = TetrisMismatchCalculator.GetBoardTotalMismatch(newBoard, useBitmasking,colorMatching);
                var newLine = string.Format("{0},{1}", i, totalMismatch);
                csv.AppendLine(newLine);  

                i++;
            }

            newBoard.RemoveTilesWithMismatches(true, colorMatching);

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

          public void TetrisBlocks_V3_Simulated_Annealing(int width, int height, string outputName, ColorMatching colorMatching, int iterations)
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
            // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
            // Thread.Sleep(5);
            while (i<iterations){
                newBoard.ReplaceTileUsingSimulatedAnnealing(useBitmasking, colorMatching, rand, tileSetID, i);

                // if (i%50==0){
                // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
                // }

                int totalMismatch = TetrisMismatchCalculator.GetBoardTotalMismatch(newBoard, useBitmasking,colorMatching);
                var newLine = string.Format("{0},{1}", i, totalMismatch);
                csv.AppendLine(newLine);  

                i++;
            }

            newBoard.RemoveTilesWithMismatches(true, colorMatching);

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



        // public void TetrisBlocks_V2_ProbabilityDistribution(int width, int height, string outputName, ColorMatching colorMatching)
        // {
        //     Stopwatch sw = Stopwatch.StartNew();

        //     Board newBoard = new Board(height,width);
        //     ColorMap colorMap = new ColorMap();

        //     WangTileSet tileSet = Utils.GenerateTetrisTileSet(colorMap);
        //     newBoard.AddTileSet(tileSet);

        //     // Select random tile to place on first slot
        //     Random rand = new Random();
        //     int tileIndex = rand.Next(0,newBoard.TileSet[0].Tiles.Length);

        //     // place the random tile on the board 
        //     (int col, int row) pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height, rand);
        //     // (int col, int row) pos = (0,0);
        //     newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
        //     // Console.WriteLine($"First tile is {tileIndex}");
           

        //     int i=0;
        //     while (i<5000){
        //         // Console.WriteLine($"Iteration={i}");
        //         TetrisBlockIterate(newBoard, colorMatching, rand, pos);
        //         // Picture newPic2 = new Picture();
        //         // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
        //         // Thread.Sleep(500);

        //         newBoard.RemoveTilesWithMismatches(true, colorMatching);

        //         // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
        //         // Thread.Sleep(500);
        //         if (i%100==0){
        //             (int col, int row) emptySlotPos = newBoard.GetEmptySlotPosition();
        //             // Thread.Sleep(500);
        //             if (emptySlotPos.col==newBoard.Height && emptySlotPos.row== newBoard.Width){
        //                 Console.WriteLine($"After Iteration[{i}], board is complete");
        //                 // No empty tile slots
        //                 break;
        //             }
                    

        //             // There is empty tile slot
        //             // then remove the adjacent tiles
        //             newBoard.RemoveAdjacentTiles(pos.col, pos.row);
        //             newBoard.RemoveTilesWithMismatches(true, colorMatching);
        //         }
 
        //         i++;
        //     }
     
        //     // Generate and Save PNG
        //     Picture newPic = new Picture();
        //     newPic.SavePNG(newBoard, colorMap, outputName+".png");

        //      // Timer stop
        //     sw.Stop();
        //     TimeSpan time = sw.Elapsed;
        //     Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        // }

        // Cannot be run directly since there are needed changes for complete tile set
        // public void TetrisBlocks_V2_CompleteTileSet_Test(int width, int height, string outputName, ColorMatching colorMatching)
        // {
        //     Stopwatch sw = Stopwatch.StartNew();

        //     Board newBoard = new Board(height,width);
        //     ColorMap colorMap = new ColorMap();

        //     WangTileSet tileSet = Utils.GenerateCompleteTileSet(colorMap, 4);
        //     newBoard.AddTileSet(tileSet);

        //     // Select random tile to place on first slot
        //     Random rand = new Random();
        //     int tileIndex = rand.Next(0,newBoard.TileSet[0].Tiles.Length);

        //     // place the random tile on the board 
        //     (int col, int row) pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height, rand);
        //     // (int col, int row) pos = (0,0);
        //     newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
        //     // Console.WriteLine($"First tile is {tileIndex}");
           
        //     int i=0;
        //     while (i<5000){
        //         TetrisBlockIterate(newBoard, colorMatching, rand, pos);
        //         // Picture newPic2 = new Picture();
        //         // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
        //         // Thread.Sleep(500);

        //         newBoard.RemoveTilesWithMismatches(true, colorMatching);

        //         // newPic2.SavePNG(newBoard, colorMap, outputName+".png");
        //         // Thread.Sleep(500);

        //         if (i%100==0){
        //             (int col, int row) emptySlotPos = newBoard.GetEmptySlotPosition();
        //             // Thread.Sleep(500);
        //             if (emptySlotPos.col==newBoard.Height && emptySlotPos.row== newBoard.Width){
        //                 Console.WriteLine($"After Iteration[{i}], board is complete");
        //                 // No empty tile slots
        //                 break;
        //             }
                    
        //             // There is empty tile slot
        //             // then remove the adjacent tiles
        //             newBoard.RemoveAdjacentTiles(pos.col, pos.row);
        //             newBoard.RemoveTilesWithMismatches(true, colorMatching);
        //         }
 
        //         i++;
        //     }
     
        //     // Generate and Save PNG
        //     Picture newPic = new Picture();
        //     newPic.SavePNG(newBoard, colorMap, outputName+".png");

        //     // Timer stop
        //     sw.Stop();
        //     TimeSpan time = sw.Elapsed;
        //     Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
        // }
    }
}
