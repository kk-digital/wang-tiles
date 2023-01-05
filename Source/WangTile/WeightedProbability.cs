namespace WangTile{
    public static class WeightedProbability
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////// Weighted-Probability Methods ////////////////////////////////////
        public static TileProbability[] GetProbabilityVector(Board board, int col,int row, int tileSetID, TileMismatch[] tileMismatches){
            TileProbability[] tileProbabilityVector = new TileProbability[tileMismatches.Length];
            int numberOfMismatch = 0;
    
            float k = 1f;
            int maxEnergy = 16;
            float gamma = 2.0f;
            float epsilon = 0f; // 1/16
            int numberOfTiles = board.TileSet[tileSetID].Tiles.Length;
            float maxEnergyPowerGamma = (float)Math.Pow((double)maxEnergy,gamma);

            float epsilonMulInverseOfNumOfTiles = epsilon *(1.0f/numberOfTiles);

            for (int i=0;i<tileMismatches.Length;i++){
                
                numberOfMismatch = tileMismatches[i].NumberOfMismatches;
                // Weight[i] = (k * ((max energy - error(i))^gamma) / (max_energy^gamma) ) + epsilon* (1.0 / number-of-tiles)
                float term1= k * (((float)Math.Pow((double)(maxEnergy-numberOfMismatch),gamma)/maxEnergyPowerGamma));
                float term2= epsilonMulInverseOfNumOfTiles;
                float weight =  term1+term2; 

                TileProbability tileProbability = new TileProbability(tileMismatches[i].TileID, weight, tileMismatches[i].NumberOfMismatches);
                tileProbabilityVector[i]=tileProbability;
            }

            return tileProbabilityVector;
        }


        public static TileProbability[] GetNormalizedProbabilityVector(TileProbability[] tileProbabilityVector){
            float sum = 0f;
            for (int i = 0;i < tileProbabilityVector.Length;i++){
                sum += tileProbabilityVector[i].Probability;
            }

            for (int i = 0;i < tileProbabilityVector.Length;i++){
                float normalizedWeight = tileProbabilityVector[i].Probability/sum;
                tileProbabilityVector[i].Probability=normalizedWeight;
            }

            return tileProbabilityVector;
        }

        public static TileProbability[] GetCumulativeProbabilityVector(TileProbability[] tileProbabilityVector){
            TileProbability[] CV = tileProbabilityVector;

            float sum = 0;
            for (int i = 0;i < CV.Length;i++){
                sum = sum + CV[i].Probability;
                CV[i].Probability=sum;
            }
            
            return CV;
        }

        public static int ChooseTileIndexFromCumulativeProbabilityVector(TileProbability[] CumulativeProbabilityVector, Random random){
            // Generate random number 0-sum of weight probabilities
            // 0 to cumulativeProbabilityVectore length-1
            float randNumber = (float)Utils.GetRandomNumber(0, CumulativeProbabilityVector[CumulativeProbabilityVector.Length-1].Probability, random);
            
            // find index that the probability is less than the random number
            // TODO: improve and use binary search
            for (int i=0;i<CumulativeProbabilityVector.Length;i++){
                if (CumulativeProbabilityVector[i].Probability>randNumber){
                    return CumulativeProbabilityVector[i].TileID;
                }
            }

            return 0;
        }
    }
}