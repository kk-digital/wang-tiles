namespace Wang
{
    class Generator {
        public void PlacementAlgo_V1()
        {
            Board newBoard = new Board(10,10);
            WangCornerTileSet newTileSet = new WangCornerTileSet();
            WangCornerTileSet tileSet= newTileSet.GenerateTileSet(4,1);
            newBoard.AddTileSet(tileSet);
          

            // Select random tile to place on first slot
            Random random = new Random();
            int randIndex = random.Next(0,newBoard.TileSet[0].Tiles.Length);
            WangCornerTile tile=newBoard.TileSet[0].Tiles[randIndex];

            // place the random tile on the board slot
            (int x,int y) randomPos = Utils.GetRandomPosition(newBoard.Width,newBoard.Height);
            newBoard.PlaceTile(tile,randomPos.x,randomPos.y);
            
            // temporarily putting one by one instead of looping this for testing
            (WangCornerTile newTile, int x, int y) val=newBoard.AddTileToAdjacent(tile,randomPos.x,randomPos.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
            val=newBoard.AddTileToAdjacent(val.newTile,val.x,val.y);
         
       
            
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, "PlacementAlgo_V1.png");

        }
    }
}
