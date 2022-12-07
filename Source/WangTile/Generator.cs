using System.Diagnostics;

namespace WangTile
{
 
    class Generator {
        public void TetrisBlocks_V1(int width, int height, string outputName, ColorMatching colorMatching)
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

            newBoard.AddTileSet(tileSet);


            // Select random tile to place on first slot
            Random rand = new Random();
            int tileIndex = rand.Next(0,newBoard.TileSet[0].Tiles.Length);

            // place the random tile on the board 
            (int col, int row) pos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height, rand);
            // (int col, int row) pos = (0,0);
            newBoard.PlaceTile(0,tileIndex,pos.col,pos.row);
            Console.WriteLine($"First tile is {tileIndex}");
           

            int i=0;
            while (i<10000){
                Console.WriteLine($"Iteration={i}");
                TetrisBlockIterate(newBoard, colorMatching, rand);
                if (i%100==0){
                Picture newPic2 = new Picture();
                newPic2.SavePNG(newBoard, colorMap, outputName+".png");
                }

                // Thread.Sleep(1000);
                newBoard.RemoveTilesWithMismatches(true, colorMatching);

                if (i%100==0){
                    (int col, int row) emptySlotPos = newBoard.GetEmptySlotPosition();
                   
                    // Thread.Sleep(500);
                    if (emptySlotPos.col==newBoard.Height && emptySlotPos.row== newBoard.Width){
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
        }

        void TetrisBlockIterate(Board newBoard,ColorMatching colorMatching, Random rand){
            (int col, int row) pos;
            // for (int i=1; i<4;i++){
            while (true){
                // place tiles to random position with atleast 1 adjacent edge side
                pos = newBoard.FindEmptySlotWithAdjacentTilesOnEdges(rand);
                if (pos.col == newBoard.Height && pos.row == newBoard.Width){
                        // No empty tile slots
                        break;
                }

                int[] tileMismatches = newBoard.GetTileMismatchArray(0,pos.col,pos.row, true, colorMatching);
                TileMismatch[] tileMismatchesStruct = Utils.SortTileMismatches(tileMismatches);
                Console.WriteLine($"Pos={pos.col},{pos.row}");
                for (int x=0; x<tileMismatchesStruct.Length;x++){
                    Console.WriteLine($"Tile ID={tileMismatchesStruct[x].TileID}, Mismatches={tileMismatchesStruct[x].NumberOfMismatches}");
                }
                // Console.WriteLine("------------");
                int lowestMismatchTileID = tileMismatchesStruct[0].TileID;
                int max = 0;
                if (tileMismatchesStruct[0].NumberOfMismatches==tileMismatchesStruct[5].NumberOfMismatches){
                    max = 5 ;
                }else if (tileMismatchesStruct[0].NumberOfMismatches==tileMismatchesStruct[4].NumberOfMismatches){
                    max = 4 ;
                }else if (tileMismatchesStruct[0].NumberOfMismatches==tileMismatchesStruct[3].NumberOfMismatches){
                    max = 3 ;
                }else if (tileMismatchesStruct[0].NumberOfMismatches==tileMismatchesStruct[2].NumberOfMismatches){
                    max = 2 ;
                } else if (tileMismatchesStruct[0].NumberOfMismatches==tileMismatchesStruct[1].NumberOfMismatches){
                    max = 1;
                }

                if (max!=0){
                    lowestMismatchTileID  = tileMismatchesStruct[rand.Next(0,max+1)].TileID;
                }

                newBoard.PlaceTile(0,lowestMismatchTileID,pos.col,pos.row);
            }
        }
    }
}
