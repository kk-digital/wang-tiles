namespace Wang
{
    class Generator {
        public void PlacementAlgo_V1(int width, int height, int numOfColors,string outputName)
        {
            Board newBoard = new Board(10,10);
            WangCornerTileSet newTileSet = new WangCornerTileSet();
            WangCornerTileSet tileSet= newTileSet.GenerateTileSet(numOfColors,1);
            newBoard.AddTileSet(tileSet);
          

            // Select random tile to place on first slot
            Random random = new Random();
            int randIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);
            WangCornerTile tile=newBoard.TileSet[0].Tiles[randIndex];

            // place the random tile on the board slot
            (int col,int row) randomPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);
            newBoard.PlaceTile(tile,randomPos.col,randomPos.row);
            
            (int col, int row) val= (randomPos.col,randomPos.row);
            for (int i=1;i<newBoard.TileSlots.Length;i++){
                val=newBoard.AddTileToAdjacent(val.col,val.row);
            }
  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, outputName+".png");

        }

        public void PlacementAlgo_V2(int width, int height, int numOfColors,string outputName)
        {   
            // Create board
            Board newBoard = new Board(10,10);

            // Generate tile set
            WangCornerTileSet newTileSet = new WangCornerTileSet();
            WangCornerTileSet tileSet= newTileSet.GenerateTileSet(numOfColors,1);

            // Add tile set to board
            newBoard.AddTileSet(tileSet);
          
            // Get Random tile to put
            Random random = new Random();
            int randIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);
            WangCornerTile tile=newBoard.TileSet[0].Tiles[randIndex];

            // Put first slot at upper left
            (int col, int row) val = (0,0);
            newBoard.PlaceTile(tile,val.col,val.row);
       
            for (int i=1;i<newBoard.TileSlots.Length;i++){
                val=newBoard.AddTileToNextSlot(val.col,val.row);
            }
  
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, outputName+".png");

        }
    }
}
