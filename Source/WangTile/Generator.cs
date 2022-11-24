using System.Diagnostics;

namespace WangTile
{
 
    class Generator {
        public void TetrisBlocks_V1(int width, int height, string outputName)
        {
            Board newBoard = new Board(height,width);
            ColorMap colorMap = new ColorMap();

            WangTileSet tileSet= new WangTileSet();

            // Tetris Block 1 - L horizontal
            // [][][][] 
            // [0,1,2,3]
            // 
            // Tile 0 of Tetris Block 1
            // Add tiles to tileset
            int tileID=tileSet.CreateTile(colorMap,CornerColor.A,CornerColor.B,CornerColor.C,CornerColor.D,VerticalColor.A,HorizontalColor.B,VerticalColor.B,HorizontalColor.A);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_8NE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_1SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_2SW);
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_2SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_3SW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_5NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_6NE);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_6NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_7NE);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_8SE);

            // Tile 1 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.B,CornerColor.E,CornerColor.F,CornerColor.C,VerticalColor.C,HorizontalColor.C,VerticalColor.D,HorizontalColor.B);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_1SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_2SW);
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_2SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_3SW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_5NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_6NE);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_6NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_7NE);

            // Tile 2 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.E,CornerColor.G,CornerColor.H,CornerColor.F,VerticalColor.E,HorizontalColor.D,VerticalColor.F,HorizontalColor.C);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_1SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_2SW);
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_2SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_3SW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_5NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_6NE);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_6NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_7NE);

            // Tile 3 of Tetris Block 1
            tileID=tileSet.CreateTile(colorMap,CornerColor.G,CornerColor.I,CornerColor.J,CornerColor.H,VerticalColor.G,HorizontalColor.E,VerticalColor.H,HorizontalColor.D);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_1SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_2SW);
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_2SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_3SW);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_4NW);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_4SW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_5NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_6NE);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_6NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_7NE);

            // Tetris Block 2 - Square block
            // [][] _ [0,1]
            // [][]   [3,2]
            // 
            // Tile 0 of Tetris Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.K,CornerColor.L,CornerColor.M,CornerColor.N,VerticalColor.I,HorizontalColor.G,VerticalColor.J,HorizontalColor.F);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_8NE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_1SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_2SW);
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_2SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_3SW);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_7NE);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_8SE);

            // Tile 1 of Tetris Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.L,CornerColor.O,CornerColor.P,CornerColor.M,VerticalColor.K,HorizontalColor.H,VerticalColor.L,HorizontalColor.G);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_1SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_2SW);
            tileSet.Tiles[tileID].SetBit(BitMask.N_2S);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_2SE);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_3SW);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_4NW);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_4SW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_5NW);

            // Tile 2 of Tetris Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.M,CornerColor.P,CornerColor.Q,CornerColor.R,VerticalColor.L,HorizontalColor.J,VerticalColor.M,HorizontalColor.I);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_3SW);
            tileSet.Tiles[tileID].SetBit(BitMask.NE_4NW);
            tileSet.Tiles[tileID].SetBit(BitMask.E_4W);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_4SW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_5NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_6NE);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_7NE);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_6NW);

            // Tile 3 of Tetris Block 2
            // Add tiles to tileset
            tileID=tileSet.CreateTile(colorMap,CornerColor.N,CornerColor.M,CornerColor.R,CornerColor.S,VerticalColor.J,HorizontalColor.I,VerticalColor.N,HorizontalColor.K);
            tileSet.Tiles[tileID].SetBit(BitMask.W_8E);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_8NE);
            tileSet.Tiles[tileID].SetBit(BitMask.NW_1SE);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_5NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SE_6NE);
            tileSet.Tiles[tileID].SetBit(BitMask.S_6N);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_7NE);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_6NW);
            tileSet.Tiles[tileID].SetBit(BitMask.SW_8SE);
    

            newBoard.AddTileSet(tileSet);

            Console.WriteLine($"length of CornerColorsData={tileSet.CornerColors.Length}");
            Console.WriteLine($"length of HorizontalColorsData={tileSet.HorizontalColors.Length}");
            Console.WriteLine($"length of VerticalColorsData={tileSet.VerticalColors.Length}");

            Console.WriteLine($"Number of times Corner.A is used={tileSet.CornerColors[(int)CornerColor.A].NumberOfTimesUsed}");
            Console.WriteLine($"Number of times Corner.B is used={tileSet.CornerColors[(int)CornerColor.B].NumberOfTimesUsed}");
            // Select random tile to place on first slot
            Random random = new Random();
            int tileIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);

            // place the random tile on the board slot position 0,0
            (int col,int row) randomPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);
            newBoard.PlaceTile(0,tileIndex,randomPos.col,randomPos.row);
            
            // place tiles to next tile, left to right
  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, colorMap, outputName+".png");
        }
    }
}
