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

        // numberOfColors is the number of 
        // colors available to make a combination.
        public static WangTileSet GenerateCompleteTileSet(ColorMap colorMap, int numberOfColors)
        {
        int NW, NE, SE, SW;
        WangTileSet tileSet = new WangTileSet();

        for (NW = 0; NW < numberOfColors; NW++) {
            for (NE = 0; NE < numberOfColors; NE++) {
                for (SE = 0; SE < numberOfColors; SE++) {
                    for (SW = 0; SW < numberOfColors; SW++) {

                        // +2 because colors in our enum starts with 2
                        // 0 and 1 are special cases
                        int tileID=tileSet.CreateTile(colorMap,CornerColor.A,CornerColor.B,CornerColor.C,CornerColor.D,(VerticalColor)NW+3,(HorizontalColor)NE+3,(VerticalColor)SE+3,(HorizontalColor)SW+2);
                        tileSet.Tiles[tileID].MaskAllCorners();
                    }
                }
            }
        } 

        return tileSet;
        }

        public static WangTileSet GenerateTetrisTileSet(ColorMap colorMap)
        {
            WangTileSet tileSet= new WangTileSet();
            int tileID=0;
            // Tetris Block 1 - l horizontal
            // [][][][] 
            // [0,1,2,3]
            // 
            // Tile 0 of Tetris Block 1
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.A,CornerColor.B,CornerColor.C,CornerColor.D,VerticalColor.A,HorizontalColor.B,VerticalColor.B,HorizontalColor.A);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 1 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.B,CornerColor.E,CornerColor.F,CornerColor.C,VerticalColor.C,HorizontalColor.C,VerticalColor.D,HorizontalColor.B);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 2 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.E,CornerColor.G,CornerColor.H,CornerColor.F,VerticalColor.E,HorizontalColor.D,VerticalColor.F,HorizontalColor.C);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 3 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.G,CornerColor.I,CornerColor.J,CornerColor.H,VerticalColor.G,HorizontalColor.E,VerticalColor.H,HorizontalColor.D);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 2 - Square block
            // [][] _ [0,1]
            // [][]   [3,2]
            // 
            // Tile 0 of Tetris Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.K,CornerColor.L,CornerColor.M,CornerColor.N,VerticalColor.I,HorizontalColor.G,VerticalColor.J,HorizontalColor.F);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);

            // Tile 1 of Tetris Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.L,CornerColor.O,CornerColor.P,CornerColor.M,VerticalColor.K,HorizontalColor.H,VerticalColor.L,HorizontalColor.G);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 2 of Tetris Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.M,CornerColor.P,CornerColor.Q,CornerColor.R,VerticalColor.L,HorizontalColor.J,VerticalColor.M,HorizontalColor.I);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 3 of Tetris Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.N,CornerColor.M,CornerColor.R,CornerColor.S,VerticalColor.J,HorizontalColor.I,VerticalColor.N,HorizontalColor.K);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 3 - l vertical block
            // []   [0]
            // [] _ [1]
            // []   [2]
            // []   [3]
            // 
            // Tile 0 of Tetris Block 3
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.T,CornerColor.U,CornerColor.V,CornerColor.W,VerticalColor.O,HorizontalColor.M,VerticalColor.P,HorizontalColor.L);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);

            // Tile 1 of Tetris Block 3
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.W,CornerColor.V,CornerColor.X,CornerColor.Y,VerticalColor.P,HorizontalColor.O,VerticalColor.Q,HorizontalColor.N);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);

            // Tile 2 of Tetris Block 3
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.Y,CornerColor.X,CornerColor.Z,CornerColor.AA,VerticalColor.Q,HorizontalColor.Q,VerticalColor.R,HorizontalColor.P);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);

            // Tile 3 of Tetris Block 3
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AA,CornerColor.Z,CornerColor.AB,CornerColor.AC,VerticalColor.R,HorizontalColor.S,VerticalColor.S,HorizontalColor.R);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            
            // Tetris Block 4 - T block facing up
            //   []   _   [0]
            // [][][]  [1][2][3]
            // 
            // Tile 0 of Tetris Block 4
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AD,CornerColor.AE,CornerColor.AF,CornerColor.AG,VerticalColor.T,HorizontalColor.U,VerticalColor.U,HorizontalColor.T);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);

            // Tile 1 of Tetris Block 4
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AJ,CornerColor.AG,CornerColor.AI,CornerColor.AK,VerticalColor.W,HorizontalColor.W,VerticalColor.X,HorizontalColor.V);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 2 of Tetris Block 4
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AG,CornerColor.AF,CornerColor.AH,CornerColor.AI,VerticalColor.U,HorizontalColor.X,VerticalColor.V,HorizontalColor.W);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 3 of Tetris Block 4
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AF,CornerColor.AL,CornerColor.AM,CornerColor.AH,VerticalColor.Y,HorizontalColor.Y,VerticalColor.Z,HorizontalColor.X);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 5 - Z block 
            // [][]   _ [0][1]
            //   [][]      [2][3]
            // 
            // Tile 0 of Tetris Block 5
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AN,CornerColor.AO,CornerColor.AP,CornerColor.AQ,VerticalColor.AA,HorizontalColor.AA,VerticalColor.AB,HorizontalColor.Z);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 1 of Tetris Block 5
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AO,CornerColor.AR,CornerColor.AS,CornerColor.AP,VerticalColor.AC,HorizontalColor.AB,VerticalColor.AD,HorizontalColor.AA);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 2 of Tetris Block 5
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AP,CornerColor.AS,CornerColor.AT,CornerColor.AU,VerticalColor.AD,HorizontalColor.AD,VerticalColor.AE,HorizontalColor.AC);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 3 of Tetris Block 5
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AS,CornerColor.AV,CornerColor.AW,CornerColor.AT,VerticalColor.AF,HorizontalColor.AE,VerticalColor.AG,HorizontalColor.AD);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 6 - T block  facing right
            // []   _ [0]
            // [][]   [1][3]
            // []     [2]
            // 
            // Tile 0 of Tetris Block 6
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AX,CornerColor.AY,CornerColor.AZ,CornerColor.BA,VerticalColor.AH,HorizontalColor.AG,VerticalColor.AI,HorizontalColor.AF);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 1 of Tetris Block 6
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BA,CornerColor.AZ,CornerColor.BB,CornerColor.BC,VerticalColor.AI,HorizontalColor.AI,VerticalColor.AJ,HorizontalColor.AH);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);

            // Tile 2 of Tetris Block 6
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BC,CornerColor.BB,CornerColor.BD,CornerColor.BE,VerticalColor.AJ,HorizontalColor.AL,VerticalColor.AK,HorizontalColor.AK);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 3 of Tetris Block 6
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.AZ,CornerColor.BF,CornerColor.BG,CornerColor.BB,VerticalColor.AL,HorizontalColor.AJ,VerticalColor.AM,HorizontalColor.AI);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 7 - T block  facing down
            // [][][] _ [0][1][2]
            //   []        [3]
            // 
            // Tile 0 of Tetris Block 7
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BH,CornerColor.BI,CornerColor.BJ,CornerColor.BK,VerticalColor.AN,HorizontalColor.AN,VerticalColor.AO,HorizontalColor.AM);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 1 of Tetris Block 7
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BI,CornerColor.BL,CornerColor.BM,CornerColor.BJ,VerticalColor.AP,HorizontalColor.AO,VerticalColor.AQ,HorizontalColor.AN);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);

            // Tile 2 of Tetris Block 7
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BL,CornerColor.BN,CornerColor.BO,CornerColor.BM,VerticalColor.AS,HorizontalColor.AP,VerticalColor.AT,HorizontalColor.AO);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 3 of Tetris Block 7
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BJ,CornerColor.BM,CornerColor.BP,CornerColor.BQ,VerticalColor.AQ,HorizontalColor.AR,VerticalColor.AR,HorizontalColor.AQ);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            return tileSet;
        }
    }
}