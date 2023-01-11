using SkiaSharp;

namespace WangTile
{
    public static class TileSetGenerator
    {
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
            // Tetris Block 1 - I horizontal
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

            // Tetris Block 3 - I vertical block
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

            // Tetris Block 6 - T block facing right
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

            // Tetris Block 7 - T block facing down
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

            // Tetris Block 8 - T block facing left
            //   []       [1]
            // [][]  - [0][2]
            //   []       [3]
            //  
            // Tile 0 of Tetris Block 8
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BZ,CornerColor.BU,CornerColor.BW,CornerColor.CA,VerticalColor.AU,HorizontalColor.BZ,VerticalColor.AV,HorizontalColor.BY);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 1 of Tetris Block 8
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BR,CornerColor.BS,CornerColor.BT,CornerColor.BU,VerticalColor.AW,HorizontalColor.AU,VerticalColor.AX,HorizontalColor.AT);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 2 of Tetris Block 8
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BU,CornerColor.BT,CornerColor.BV,CornerColor.BW,VerticalColor.AX,HorizontalColor.AS,VerticalColor.AY,HorizontalColor.BZ);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 3 of Tetris Block 8
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.BW,CornerColor.BV,CornerColor.BX,CornerColor.BY,VerticalColor.AY,HorizontalColor.AW,VerticalColor.AZ,HorizontalColor.AV);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 9 - Z block facing up
            //   []       [0]
            // [][]  - [2][1]
            // []      [3]
            //  
            // Tile 0 of Tetris Block 9
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CB,CornerColor.CC,CornerColor.CD,CornerColor.CE,VerticalColor.BA,HorizontalColor.AY,VerticalColor.BB,HorizontalColor.AX);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 1 of Tetris Block 9
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CE,CornerColor.CD,CornerColor.CF,CornerColor.CG,VerticalColor.BB,HorizontalColor.BB,VerticalColor.BC,HorizontalColor.BA);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 2 of Tetris Block 9
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CI,CornerColor.CE,CornerColor.CG,CornerColor.CH,VerticalColor.BD,HorizontalColor.BA,VerticalColor.BE,HorizontalColor.AZ);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);

            // Tile 3 of Tetris Block 9
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CH,CornerColor.CG,CornerColor.CJ,CornerColor.CK,VerticalColor.BE,HorizontalColor.BD,VerticalColor.BF,HorizontalColor.BC);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 10 - L block facing up
            // []     [0]
            // []   - [1]
            // [][]   [2][3]
            //  
            // Tile 0 of Tetris Block 10
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CL,CornerColor.CM,CornerColor.CN,CornerColor.CO,VerticalColor.BG,HorizontalColor.BF,VerticalColor.BH,HorizontalColor.BE);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 1 of Tetris Block 10
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CO,CornerColor.CN,CornerColor.CP,CornerColor.CQ,VerticalColor.BH,HorizontalColor.BH,VerticalColor.BI,HorizontalColor.BG);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 2 of Tetris Block 10
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CQ,CornerColor.CP,CornerColor.CR,CornerColor.CS,VerticalColor.BI,HorizontalColor.BJ,VerticalColor.BJ,HorizontalColor.BI);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 3 of Tetris Block 10
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CP,CornerColor.CT,CornerColor.CU,CornerColor.CR,VerticalColor.BK,HorizontalColor.BK,VerticalColor.BL,HorizontalColor.BJ);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 11 - L block facing down
            // [][]     [0][1]
            //   []   -    [2]
            //   []        [3]
            //  
            // Tile 0 of Tetris Block 11
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CV,CornerColor.CW,CornerColor.CX,CornerColor.CY,VerticalColor.BM,HorizontalColor.BM,VerticalColor.BN,HorizontalColor.BL);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 1 of Tetris Block 11
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CW,CornerColor.CZ,CornerColor.DA,CornerColor.CX,VerticalColor.BO,HorizontalColor.BN,VerticalColor.BP,HorizontalColor.BM);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);

            // Tile 2 of Tetris Block 11
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.CX,CornerColor.DA,CornerColor.DB,CornerColor.DC,VerticalColor.BP,HorizontalColor.BP,VerticalColor.BQ,HorizontalColor.BO);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);

            // Tile 3 of Tetris Block 11
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.DC,CornerColor.DB,CornerColor.DD,CornerColor.DE,VerticalColor.BQ,HorizontalColor.BR,VerticalColor.BR,HorizontalColor.BQ);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tetris Block 12 - L block facing right - rotated 90 deg to right
            // [][][]    [0][1][2]
            // []   -    [3]
            //  
            // Tile 0 of Tetris Block 12
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.DF,CornerColor.DG,CornerColor.DH,CornerColor.DI,VerticalColor.BS,HorizontalColor.BT,VerticalColor.BT,HorizontalColor.BS);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);

            // Tile 1 of Tetris Block 12
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.DG,CornerColor.DL,CornerColor.DM,CornerColor.DH,VerticalColor.BV,HorizontalColor.BU,VerticalColor.BW,HorizontalColor.BT);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 2 of Tetris Block 12
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.DL,CornerColor.DN,CornerColor.DO,CornerColor.DM,VerticalColor.BX,HorizontalColor.BV,VerticalColor.BY,HorizontalColor.BU);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            // Tile 3 of Tetris Block 12
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.DI,CornerColor.DH,CornerColor.DJ,CornerColor.DK,VerticalColor.BT,HorizontalColor.BX,VerticalColor.BU,HorizontalColor.BW);
            tileSet.Tiles[tileID].MaskAllCorners();
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);

            return tileSet;
        }

        public static WangTileSet GenerateHemingwayTileSet(ColorMap colorMap)
        {
            WangTileSet tileSet = new WangTileSet();
            int tileID=0;
            // Hemingway Block 1
            // [ ]  _ [0]
            // [ ]    [1]
            // 
            // Tile 0 of Hemingway Block 1
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.A,CornerColor.B,CornerColor.C,CornerColor.D,VerticalColor.N,HorizontalColor.J,VerticalColor.D,HorizontalColor.M);
            tileSet.Tiles[tileID].MaskAllCorners();

            // Tile 1 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.D,CornerColor.C,CornerColor.E,CornerColor.F,VerticalColor.D,HorizontalColor.P,VerticalColor.O,HorizontalColor.I);
            tileSet.Tiles[tileID].MaskAllCorners();

            // Hemingway Block 2
            // [ ][ ] - [0][1]
            // 
            // Tile 0 of Hemingway Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.G,CornerColor.I,CornerColor.J,CornerColor.H,VerticalColor.K,HorizontalColor.A,VerticalColor.E,HorizontalColor.J);
            tileSet.Tiles[tileID].MaskAllCorners();

            // Tile 1 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.I,CornerColor.K,CornerColor.L,CornerColor.J,VerticalColor.H,HorizontalColor.I,VerticalColor.G,HorizontalColor.A);
            tileSet.Tiles[tileID].MaskAllCorners();

            // Hemingway Block 3
            // [ ]  _ [0]
            // [ ]    [1]
            // 
            // Tile 0 of Hemingway Block 3
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.M,CornerColor.N,CornerColor.O,CornerColor.P,VerticalColor.E,HorizontalColor.F,VerticalColor.C,HorizontalColor.P);
            tileSet.Tiles[tileID].MaskAllCorners();

            // Tile 1 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.P,CornerColor.O,CornerColor.Q,CornerColor.R,VerticalColor.C,HorizontalColor.M,VerticalColor.H,HorizontalColor.L);
            tileSet.Tiles[tileID].MaskAllCorners();

            // Hemingway Block 4
            // [ ][ ] - [0][1]
            // 
            // Tile 0 of Hemingway Block 4
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.S,CornerColor.U,CornerColor.V,CornerColor.T,VerticalColor.G,HorizontalColor.B,VerticalColor.N,HorizontalColor.F);
            tileSet.Tiles[tileID].MaskAllCorners();

            // Tile 1 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.U,CornerColor.W,CornerColor.X,CornerColor.V,VerticalColor.O,HorizontalColor.L,VerticalColor.K,HorizontalColor.B);
            tileSet.Tiles[tileID].MaskAllCorners();


            return tileSet;
        }

        public static WangTileSet GenerateTileSetFromJSON(ColorMap colorMap, Dictionary<int, SKImage> imageMap, Dictionary<int, SKImage> tileImageMap, string jsonDirectory, string jsonName)
        {
            WangTileSet wangTileSet= new WangTileSet();
            int tileID=0;

            TileJSON tile = WangTileJSON.DeserializeTileJSON(jsonDirectory+"/"+jsonName);
            
            for (int i=0;i<tile.Layers[0].Chunks.Length;i++){
                TileChunksJSON tileLayerChunk = tile.Layers[0].Chunks[i];

                // Console.WriteLine($"Tile Chunk {i}");
                WangTileJSON.CheckCornerMarkers(tileLayerChunk);

                // Get north color
                int northColorJSON = WangTileJSON.GetNorthColorFromTileChunks(tileLayerChunk);
                // Get east color
                int eastColorJSON = WangTileJSON.GetEastColorFromTileChunks(tileLayerChunk);
                // Get south color
                int southColorJSON = WangTileJSON.GetSouthColorFromTileChunks(tileLayerChunk);
                // Get west color
                int westColorJSON = WangTileJSON.GetWestColorFromTileChunks(tileLayerChunk);

                VerticalColor northColor = colorMap.RetrieveVerticalColorForJSON(northColorJSON);
                HorizontalColor eastColor = colorMap.RetrieveHorizontalColorForJSON(eastColorJSON);
                VerticalColor southColor = colorMap.RetrieveVerticalColorForJSON(southColorJSON);
                HorizontalColor westColor = colorMap.RetrieveHorizontalColorForJSON(westColorJSON);

                // Get inner 16x16 data only since outer layer is only used to determine the colors.
                int[] tileData = Utils.GetInner16x16(tileLayerChunk.Data);

                // Add tiles to tileset
                // CornerColor is temporary
                tileID=wangTileSet.CreateTile(colorMap,CornerColor.A,CornerColor.A,CornerColor.A,CornerColor.A,northColor,eastColor,southColor,westColor, tileData);
                wangTileSet.Tiles[tileID].MaskAllCorners();

                // Update image map from tileset data
                UpdateImageMap(imageMap, tileData, tile.TileSets, jsonDirectory);
                
                // Add tile image to the tileImageMap
                tileImageMap[tileID] = SkiaSharpImageGenerator.CreateTileImage(imageMap, tileData);
            }

            return wangTileSet;
        }

        public static TileSetJSON GetTileSetInfo(int tileID, TileSetJSON[] tileSets){
            if (tileSets.Length==1){
                return tileSets[0];
            }

            for (int i = 1;i<tileSets.Length;i++){
                if (tileID>=tileSets[i-1].FirstGID && tileID<tileSets[i].FirstGID){
                    return tileSets[i-1];
                }
            }

            return tileSets[tileSets.Length-1];
        }

        public static void UpdateImageMap(Dictionary<int, SKImage> imageMap, int[] tileData, TileSetJSON[] tileSets, string jsonDirectory){
            foreach (int ID in tileData){
                if (!imageMap.ContainsKey(ID)){
                    if (ID == 0){
                        // ID 0 is blank
                        imageMap[0] = SkiaSharpImage.CreateBlankImage();
                    } else {
                        TileSetJSON tileSet = GetTileSetInfo(ID, tileSets);
                        string tileSetSource = Utils.ChangeFileExtension(tileSet.Source, ".tsj");
                        ColorTileSetJSON colorTileSet = WangTileJSON.DeserializeColorTileSetJSON(jsonDirectory+"/"+tileSetSource);
                        string imageSource = colorTileSet.Image;
                        SKImage tileImage = SkiaSharpImage.GetTileImageFromTileSetImage(jsonDirectory+"/"+imageSource, ID-tileSet.FirstGID);
                        imageMap[ID]=tileImage;
                    }   
                }
            }
        }
    }
}