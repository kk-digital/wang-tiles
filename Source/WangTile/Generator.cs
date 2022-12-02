using System.Diagnostics;

namespace WangTile
{
 
    class Generator {
        public void TetrisBlocks_V1(int width, int height, string outputName)
        {
            Board newBoard = new Board(height,width);
            ColorMap colorMap = new ColorMap();

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
    

            newBoard.AddTileSet(tileSet);

            Console.WriteLine($"length of CornerColorsData={tileSet.CornerColors.Length}");
            Console.WriteLine($"length of HorizontalColorsData={tileSet.HorizontalColors.Length}");
            Console.WriteLine($"length of VerticalColorsData={tileSet.VerticalColors.Length}");

            Console.WriteLine($"Number of times Corner.A is used={tileSet.CornerColors[(int)CornerColor.A].NumberOfTimesUsed}");
            Console.WriteLine($"Number of times Corner.B is used={tileSet.CornerColors[(int)CornerColor.B].NumberOfTimesUsed}");
            // Select random tile to place on first slot
            Random random = new Random();
            // int tileIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);
            int tileIndex = 4;


            // place the random tile on the board slot position 0,0
            (int col, int row) pos = (0,0);
            newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
            Console.WriteLine($"First tile is {tileIndex}");


            for (int i=1; i<100;i++){
                Console.WriteLine($"Tile Number={i}");

                // place tiles to next tile, left to right
                pos = Utils.GetNextTileSlot(newBoard.Width, pos.col, pos.row);
                // random
                // pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);

                int[] tileMismatches = newBoard.GetTileMismatchArray(0,pos.col,pos.row, true);
                TileMismatch[] tileMismatchesStruct = Utils.SortTileMismatches(tileMismatches);
                for (int x=0; x<tileMismatchesStruct.Length;x++){
                    Console.WriteLine($"TileID={tileMismatchesStruct[x].TileID}, Mismatches={tileMismatchesStruct[x].NumberOfMismatches}");
                }

                int lowestMismatchTileID = tileMismatchesStruct[0].TileID;
                if (tileMismatchesStruct[0].NumberOfMismatches==tileMismatchesStruct[1].NumberOfMismatches){
                    Random rand= new Random();
                    lowestMismatchTileID  = tileMismatchesStruct[rand.Next(0,2)].TileID;
                }

                Console.WriteLine($"Tile put={lowestMismatchTileID}");
                Console.WriteLine("-----------------");

                newBoard.PlaceTile(0,lowestMismatchTileID,pos.col,pos.row);
            }
  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, colorMap, outputName+".png");
        }
    }
}