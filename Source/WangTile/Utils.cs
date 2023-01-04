using SkiaSharp;

namespace WangTile
{
    public static class Utils
    {
        public static (int col,int row) GetRandomPosition(int width, int height, Random rand)
        {
            int randCol = rand.Next(0,height);
            int randRow = rand.Next(0,width);

            return (col:randCol, row:randRow);
        }

        public static int GetBoardSlotIndex(int Width, int col, int row)
        {
            return (Width*col)+row;
        }

        public static (int col, int row) GetNextTileSlot(int width,int col, int row){
            if (row==width-1){
                return (col:col+1,row:0);
            } else {
                return (col:col, row:row+1);
            }
        }

        public static bool SelectProbability(Random random, float probability)
        {
            float randFloat = (float)random.NextDouble();
            if (randFloat<probability){
                return true;
            }

            return false;
        }

        public static TileProbability[] PermutationShuffleTileProbabilities(TileProbability[] tileProbabilities, Random rand){
            for (int j=0;j<tileProbabilities.Length;j++){
                int z = rand.Next(j,tileProbabilities.Length);
                TileProbability tmp = tileProbabilities[j];
                tileProbabilities[j]=tileProbabilities[z];
                tileProbabilities[z]=tmp;
            }

            return tileProbabilities;
        }

        public static double GetRandomNumber(double minimum, double maximum, Random random)
        { 
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static (int col,int row) GetNorthCoordinates(int col,int row){
            return (col:col-1,row:row);
        }

        public static (int col,int row) GetEastCoordinates(int col,int row){
            return (col:col,row:row+1);
        }

        public static (int col,int row) GetSouthCoordinates(int col,int row){
            return (col:col+1,row:row);
        }

        public static (int col,int row) GetWestCoordinates(int col,int row){
            return (col:col,row:row-1);
        }

        public static TileMismatch[] SortTileMismatches(int[] tileMismatches){
            TileMismatch[] tileMismatchArray = new TileMismatch[tileMismatches.Length];

            for (int i=0;i<tileMismatches.Length;i++){
                TileMismatch newTileMismatch = new TileMismatch(tileID:i,numberOfMismatches:tileMismatches[i]);
                tileMismatchArray[i] = newTileMismatch;
            }

            Array.Sort<TileMismatch>(tileMismatchArray , (x,y) => x.NumberOfMismatches.CompareTo(y.NumberOfMismatches));

            return tileMismatchArray;
        }


        public static string ChangeFileExtension(ReadOnlySpan<char> path, ReadOnlySpan<char> extension)
        {
            var lastPeriod = path.LastIndexOf('.');
            return string.Concat(path[..lastPeriod], extension);
        }

        public static int[] GetInner16x16(int[] tileData){
            int width = 18;
            int height = 18;

            int newWidth = 16;
            int newHeight = 16;
            int[] newTileData = new int[newWidth*newHeight];

            int i=0;
            for (int col=0;col<height;col++){
                if (col==0 || col==height-1){
                    continue;
                }

                for(int row=0;row<width;row++){
                    if (row==0 || row==width-1){
                        continue;
                    }

                    int index = GetBoardSlotIndex(width, col, row);
                    newTileData[i]= tileData[index];
                    i++;
                }
            }

            return newTileData;
        }

    }
}