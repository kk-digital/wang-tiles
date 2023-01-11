namespace WangTile{
    public static class SimulatedAnnealing
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////// Simulated Annealing Methods /////////////////////////////////////
        public static void ReplaceTileUsingSimulatedAnnealing_Tetris(Board board, bool useBitmasking, Random random, int tileSetID, (int col, int row) pos){
            int currentMismatch = 0;

            WangTile tile = board.GetTile(pos.col,pos.row);
            (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = board.GetTileAdjacentColorValues(useBitmasking,pos.col,pos.row);

            
            currentMismatch = TetrisMismatchCalculator.CountMismatchOnCorners_ForPlacement(cColors,tile.TileBitMask,board.ColorMatching);
            currentMismatch += TetrisMismatchCalculator.CountMismatchVertical_ForPlacement(vColors,tile.TileBitMask,board.ColorMatching);
            currentMismatch += TetrisMismatchCalculator.CountMismatchHorizontal_ForPlacement(hColors,tile.TileBitMask,board.ColorMatching);

            if (currentMismatch>0 || (Utils.IsValidPosition(board.Height, board.Width, pos.col,pos.row) && !board.IsTileAlreadyExist(pos.col,pos.row))){
                int[] tileMismatches = board.GetTileMismatchArray(tileSetID, pos.col, pos.row, true, board.ColorMatching);
                TileMismatch[] tileMismatchesStruct = Utils.SortTileMismatches(tileMismatches);
                TileProbability[] tileProbabilities = GetTileProbability_SimulatedAnnealing(board, tileMismatchesStruct, currentMismatch);
                TileProbability[] tileNormalizedProbabilities = WeightedProbability.GetNormalizedProbabilityVector(tileProbabilities);
                TileProbability[] tileCumulativeProbabilities = WeightedProbability.GetCumulativeProbabilityVector(tileNormalizedProbabilities);

                // int tileID = this.ChooseRandomTileIDBasedOnProbability(tileProbabilities,random);
                int tileID = WeightedProbability.ChooseTileIndexFromCumulativeProbabilityVector(tileCumulativeProbabilities, random);
                
                board.PlaceTile(tileSetID, tileID, pos.col, pos.row);
            } 
        }

        public static void ReplaceTileUsingSimulatedAnnealing(Board board, bool useBitmasking, ColorMatching colorMatching, Random random, int tileSetID, (int col, int row) pos){
            int currentMismatch = 0;

            WangTile tile = board.GetTile(pos.col,pos.row);
            (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = board.GetTileAdjacentColorValues(useBitmasking,pos.col,pos.row);

            
            currentMismatch = MismatchCalculator.CountMismatchOnCorners_ForPlacement(cColors,tile.TileBitMask,colorMatching);
            currentMismatch += MismatchCalculator.CountMismatchVertical_ForPlacement(vColors,tile.TileBitMask,colorMatching);
            currentMismatch += MismatchCalculator.CountMismatchHorizontal_ForPlacement(hColors,tile.TileBitMask,colorMatching);

            if (currentMismatch>0 || (Utils.IsValidPosition(board.Height, board.Width, pos.col,pos.row) && !board.IsTileAlreadyExist(pos.col,pos.row))){
                int[] tileMismatches = board.GetTileMismatchArray(tileSetID, pos.col, pos.row, true, colorMatching);
                TileMismatch[] tileMismatchesStruct = Utils.SortTileMismatches(tileMismatches);
                TileProbability[] tileProbabilities = GetTileProbability_SimulatedAnnealing(board, tileMismatchesStruct, currentMismatch);
                TileProbability[] tileNormalizedProbabilities =  WeightedProbability.GetNormalizedProbabilityVector(tileProbabilities);
                TileProbability[] tileCumulativeProbabilities = WeightedProbability.GetCumulativeProbabilityVector(tileNormalizedProbabilities);

                // int tileID = this.ChooseRandomTileIDBasedOnProbability(tileProbabilities,random);
                int tileID = WeightedProbability.ChooseTileIndexFromCumulativeProbabilityVector(tileCumulativeProbabilities, random);
                
                board.PlaceTile(tileSetID, tileID, pos.col, pos.row);
            } 
        }

        public static void ReplaceTileUsingSimulatedAnnealing_SequentialRejectionSampling_Tetris(Board board, bool useBitmasking, Random random, int tileSetID, (int col, int row) pos){
            int currentMismatch = 0;

            WangTile tile = board.GetTile(pos.col, pos.row);
            (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = board.GetTileAdjacentColorValues(useBitmasking, pos.col, pos.row);

            
            currentMismatch = TetrisMismatchCalculator.CountMismatchOnCorners_ForPlacement(cColors, tile.TileBitMask, board.ColorMatching);
            currentMismatch += TetrisMismatchCalculator.CountMismatchVertical_ForPlacement(vColors, tile.TileBitMask, board.ColorMatching);
            currentMismatch += TetrisMismatchCalculator.CountMismatchHorizontal_ForPlacement(hColors, tile.TileBitMask, board.ColorMatching);
    

            if (currentMismatch>0 || (Utils.IsValidPosition(board.Height, board.Width, pos.col, pos.row) && !board.IsTileAlreadyExist(pos.col, pos.row))){
                int[] tileMismatches = board.GetTileMismatchArray(tileSetID, pos.col, pos.row, true, board.ColorMatching);
                TileMismatch[] tileMismatchesStruct = Utils.SortTileMismatches(tileMismatches);
                TileProbability[] tileProbabilities = GetTileProbability_SimulatedAnnealing(board, tileMismatchesStruct, currentMismatch);
                TileProbability[] tileProbabilitiesPermutationShuffle = Utils.PermutationShuffleTileProbabilities(tileProbabilities, random);

                int tileID = 0;
                float randFloat = (float)random.NextDouble();
                for (int i=0;i<tileProbabilitiesPermutationShuffle.Length;i++){
                    if (tileProbabilitiesPermutationShuffle[i].Probability>randFloat){
                        tileID = tileProbabilitiesPermutationShuffle[i].TileID;
                        break;
                    }
                }
 
                board.PlaceTile(tileSetID, tileID, pos.col, pos.row);
            } 
        }

        public static void ReplaceTileUsingSimulatedAnnealing_SequentialRejectionSampling(Board board, bool useBitmasking, Random random, int tileSetID, (int col, int row) pos){
            int currentMismatch = 0;

            WangTile tile = board.GetTile(pos.col, pos.row);
            (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = board.GetTileAdjacentColorValues(useBitmasking, pos.col, pos.row);

            
            currentMismatch = MismatchCalculator.CountMismatchOnCorners_ForPlacement(cColors, tile.TileBitMask, board.ColorMatching);
            currentMismatch += MismatchCalculator.CountMismatchVertical_ForPlacement(vColors, tile.TileBitMask, board.ColorMatching);
            currentMismatch += MismatchCalculator.CountMismatchHorizontal_ForPlacement(hColors, tile.TileBitMask, board.ColorMatching);
    

            if (currentMismatch>0 || (Utils.IsValidPosition(board.Height, board.Width, pos.col, pos.row) && !board.IsTileAlreadyExist(pos.col, pos.row))){
                int[] tileMismatches = board.GetTileMismatchArray(tileSetID, pos.col, pos.row, true, board.ColorMatching);
                TileMismatch[] tileMismatchesStruct = Utils.SortTileMismatches(tileMismatches);
                TileProbability[] tileProbabilities = GetTileProbability_SimulatedAnnealing(board, tileMismatchesStruct, currentMismatch);
                TileProbability[] tileProbabilitiesPermutationShuffle = Utils.PermutationShuffleTileProbabilities(tileProbabilities, random);

                int tileID = 0;
                float randFloat = (float)random.NextDouble();
                for (int i=0;i<tileProbabilitiesPermutationShuffle.Length;i++){
                    if (tileProbabilitiesPermutationShuffle[i].Probability>randFloat){
                        tileID = tileProbabilitiesPermutationShuffle[i].TileID;
                        break;
                    }
                }
 
                board.PlaceTile(tileSetID, tileID, pos.col, pos.row);
            } 
        }

        

        public static TileProbability[] GetTileProbability_SimulatedAnnealing(Board board, TileMismatch[] tileMismatches, int currentMismatch){
            float p = 0f;
            TileProbability[] tileProbabilities = new TileProbability[0];
            for (int i=0; i<tileMismatches.Length;i++){
                int mismatchDifference = tileMismatches[i].NumberOfMismatches-currentMismatch;

                if (mismatchDifference<=0){
                    p = 1f;
                } else {
                    p = (float)Math.Exp(((double)-1*(double)mismatchDifference)/((double)board.Temperature));
                }
                TileProbability newTileProbability = new TileProbability(tileMismatches[i].TileID, p, tileMismatches[i].NumberOfMismatches);

                tileProbabilities = tileProbabilities.Append(newTileProbability).ToArray();
            }

            return tileProbabilities;
        }

        public static float UpdateTemperature(Board board, Random random, float alpha){
            // value range 0.8-0.99
            // return this.Temperature*alpha;

            // L&M model
            // alpha 0.50 or 0.90
            return board.Temperature/(1+(alpha*board.Temperature));
        }

        public static int ChooseRandomTileIDBasedOnProbability(TileProbability[] tileProbabilities, Random rand){
            float sumOfProbabilities = 0f;
            for (int i=0;i<tileProbabilities.Length;i++){
                sumOfProbabilities+=tileProbabilities[i].Probability;
            }

            float x = (float)rand.NextDouble()*sumOfProbabilities;
            for (int i=0; i<tileProbabilities.Length;++i){
                x-=tileProbabilities[i].Probability;
                if (x<=0){
                    return tileProbabilities[i].TileID;
                }
            }

            return tileProbabilities[tileProbabilities.Length-1].TileID;
        }
    }
}