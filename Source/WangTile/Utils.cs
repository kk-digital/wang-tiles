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

        public static bool IsValidPosition(int height, int width, int col, int row){
            if (col >=0 && col<height && row>=0 && row<width){
                return true;
            }

            return false;
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

        public static (PixelColor[] cornerPixelColors, PixelColor[] verticalPixelColors, PixelColor[] horizontalPixelColors) GetHemingwayColors(){
            int length=119;
            PixelColor[] CornerPixelColors = new PixelColor[length];
            PixelColor[] VerticalPixelColors = new PixelColor[length];
            PixelColor[] HorizontalPixelColors = new PixelColor[length];

            // Sky blue 135, 206, 235
            // flesh 255, 233, 209
            // blue (0,0,255)
            // green (0,255,0)
            for (int i=0;i<=5;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(135, 206, 235);
            }
            for (int i=6;i<=11;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255, 233, 209);
            }
            for (int i=11;i<=16;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(0,0,255);
            }
            for (int i=17;i<=28;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(0,255,0);
            }

            // Brown - 29-38 
            // Light yellow - 39-48 
            // Orange - 49-58 
            // Dark yellow - 59-68
            // DarkOliveGreen- 69-78  ( 85, 107, 47, 1 )
            // DarkSalmon- 79-88 ( 233, 150, 122, 1 )
            // MediumAquamarine- 89-98 ( 102, 205, 170, 1 )
            // Magenta- 99-108 ( 255, 0, 255, 1 )
            // Maroon- 109-118 ( 128, 0, 0, 1 )

            for (int i=29;i<=38;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(92,64,51, 255);
            }

            for (int i=39;i<=48;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255,250,205,255);
            }

            for (int i=49;i<=58;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255,127,80);
            }

            for (int i=59;i<=68;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(246,190,0,255);
            }

            for (int i=69;i<=78;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(85, 107, 47, 1);
            }

            for (int i=79;i<=88;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(233, 150, 122, 1);
            }

            for (int i=89;i<=98;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(102, 205, 170, 1);
            }

            for (int i=99;i<=108;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255, 0, 255, 1);
            }

            for (int i=109;i<=118;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor( 128, 0, 0, 1);
            }

            
            PixelColor[] colorSet = new PixelColor[length];
            colorSet[0]=PixelColor.MakePixelColor(0,0,255,255);
            colorSet[1]=PixelColor.MakePixelColor(255,255,0,255);
            colorSet[2]=PixelColor.MakePixelColor(255,255,204, 255);
            colorSet[3]=PixelColor.MakePixelColor(0,255,255, 255);
            colorSet[4]=PixelColor.MakePixelColor(128,0,0, 255);
            colorSet[5]=PixelColor.MakePixelColor(0,128,0, 255);
            colorSet[6]=PixelColor.MakePixelColor(0,0,128, 255);
            colorSet[7]=PixelColor.MakePixelColor(128,128,0, 255);
            colorSet[8]=PixelColor.MakePixelColor(0,128,128, 255);
            colorSet[9]=PixelColor.MakePixelColor(255,0,255,255);
            
            // different vertical colors in all indexes
            int j = 0;
            for (int i=0;i<length;i++){
                if (j==10){
                    j=0;
                }

                VerticalPixelColors[i]=colorSet[j];
                j++;
            }
        

            // different horizontal colors in all indexes
            j = 0;
            for (int i=0;i<length;i++){
                if (j==10){
                    j=0;
                }

                HorizontalPixelColors[i]=colorSet[j];
                j++;
            }

            return (cornerPixelColors:CornerPixelColors, verticalPixelColors:VerticalPixelColors, horizontalPixelColors:HorizontalPixelColors);
        }

        public static (PixelColor[] cornerPixelColors, PixelColor[] verticalPixelColors, PixelColor[] horizontalPixelColors) GetTetrisColors(){
            int length=119;
            PixelColor[] CornerPixelColors = new PixelColor[length];
            PixelColor[] VerticalPixelColors = new PixelColor[length];
            PixelColor[] HorizontalPixelColors = new PixelColor[length];

            // Grey - 0-9 index
            // Blue - 10-18 
            // Green - 19-28 
            // Brown - 29-38 
            // Light yellow - 39-48 
            // Orange - 49-58 
            // Dark yellow - 59-68
            // DarkOliveGreen- 69-78  ( 85, 107, 47, 1 )
            // DarkSalmon- 79-88 ( 233, 150, 122, 1 )
            // MediumAquamarine- 89-98 ( 102, 205, 170, 1 )
            // Magenta- 99-108 ( 255, 0, 255, 1 )
            // Maroon- 109-118 ( 128, 0, 0, 1 )

            for (int i=0;i<=9;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(128, 128, 128, 255);
            }

            for (int i=10;i<=18;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(0,0,205, 255);
            }

            for (int i=19;i<=28;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(0,205,0, 255);
            }

            for (int i=29;i<=38;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(92,64,51, 255);
            }

            for (int i=39;i<=48;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255,250,205,255);
            }

            for (int i=49;i<=58;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255,127,80);
            }

            for (int i=59;i<=68;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(246,190,0,255);
            }

            for (int i=69;i<=78;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(85, 107, 47, 1);
            }

            for (int i=79;i<=88;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(233, 150, 122, 1);
            }

            for (int i=89;i<=98;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(102, 205, 170, 1);
            }

            for (int i=99;i<=108;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255, 0, 255, 1);
            }

            for (int i=109;i<=118;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor( 128, 0, 0, 1);
            }

            
            PixelColor[] colorSet = new PixelColor[length];
            colorSet[0]=PixelColor.MakePixelColor(0,0,255,255);
            colorSet[1]=PixelColor.MakePixelColor(255,255,0,255);
            colorSet[2]=PixelColor.MakePixelColor(255,255,204, 255);
            colorSet[3]=PixelColor.MakePixelColor(0,255,255, 255);
            colorSet[4]=PixelColor.MakePixelColor(128,0,0, 255);
            colorSet[5]=PixelColor.MakePixelColor(0,128,0, 255);
            colorSet[6]=PixelColor.MakePixelColor(0,0,128, 255);
            colorSet[7]=PixelColor.MakePixelColor(128,128,0, 255);
            colorSet[8]=PixelColor.MakePixelColor(0,128,128, 255);
            colorSet[9]=PixelColor.MakePixelColor(255,0,255,255);
            
            // different vertical colors in all indexes
            int j = 0;
            for (int i=0;i<length;i++){
                if (j==10){
                    j=0;
                }

                VerticalPixelColors[i]=colorSet[j];
                j++;
            }
        

            // different horizontal colors in all indexes
            j = 0;
            for (int i=0;i<length;i++){
                if (j==10){
                    j=0;
                }

                HorizontalPixelColors[i]=colorSet[j];
                j++;
            }

            return (cornerPixelColors:CornerPixelColors, verticalPixelColors:VerticalPixelColors, horizontalPixelColors:HorizontalPixelColors);
        }

        public static Dictionary<int,int> GetTileFrequency(WangTileSet[]? tileSets, BoardTileSlot[] tileSlots){
            Dictionary<int,int> tileFrequency = new Dictionary<int, int>();
            
            // Use tileset 0 for now
            int tileSetID = 0;
            foreach (WangTile tile in tileSets[tileSetID].Tiles){
                tileFrequency[tile.TileID]=0;
            }

            for (int i=0;i<tileSlots.Length;i++){
                if (tileSlots[i].TileID==null){
                    continue;
                }

                int tileID = (int)tileSlots[i].TileID;
                if (tileFrequency.ContainsKey(tileID)){
                    int frequency = tileFrequency[tileID];
                    frequency++;
                    tileFrequency[tileID]= frequency;
                }else{
                    tileFrequency[tileID]=1;
                }
            }

            return tileFrequency;
        }
        public static void PrintTileSlotsInfo(WangTileSet[]? tileSets, BoardTileSlot[] tileSlots){
            Dictionary<int,int> tileFrequency = GetTileFrequency(tileSets, tileSlots);
            
            // Use tileset 0 for now
            int tileSetID = 0;
            
            Console.WriteLine($"Number Of Tiles in Tileset {tileSetID}: {tileSets[tileSetID].Tiles.Length}");
            Console.WriteLine($"Tile Frequency");
            foreach (var tile in tileFrequency){
                Console.WriteLine($"TileID={tile.Key}, Frequency={tile.Value}");
            }
            
        }

    }
}