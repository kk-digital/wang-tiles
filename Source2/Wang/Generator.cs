namespace Wang
{
    class Generator {
        public void GenerateSample()
        {
            Board newBoard = new Board(10,10);
            WangCornerTileSet newTileSet = new WangCornerTileSet();
            WangCornerTileSet tileSet= newTileSet.GenerateTileSet(3,1);
            newBoard.AddTileSet(tileSet);
            newBoard.TileSet[0]=tileSet;

            // Select random tile to place on first slot
            Random random = new Random();
            int randIndex = random.Next(0,15);
            // int randIndex = 0;
            WangCornerTile tile=newBoard.TileSet[0].Tiles[randIndex];
            
            newBoard.PlaceTile(tile,0,0);
            newBoard.TileSlots[0].Tile=tile;
            WangCornerTile[] matchTiles = newBoard.TileSet[0].ReturnMatches(tileSet.Tiles,Color.MatchAll,tile.CornerColorNE,tile.CornerColorSE,Color.MatchAll);

            newBoard.PlaceTile(matchTiles[0],1,0);
            newBoard.TileSlots[1].Tile=matchTiles[1];
            
            // Generate and Save PNG
            Picture newPic = new Picture();
            newPic.SavePNG(newBoard, "testBoard.png");

        }
    }
}
