namespace Wang
{
    class Board
    {
        public BoardTileSlots[] TileSlots;
        public int Height;
        public int Width;
        public WangCornerTileSet[]? TileSet;

        // Constructor
        public Board(int height, int width)
        {
            TileSlots = new BoardTileSlots[height*width];
            Height = height;
            Width = width;
        }

        // Methods
        public void AddTileSet(WangCornerTileSet tileSet)
        {   
            if (this.TileSet==null){
                this.TileSet = new WangCornerTileSet[1];
                this.TileSet[0]=tileSet;
            }else{
            this.TileSet=this.TileSet.Append(tileSet).ToArray();
            }
        }

        public void PlaceTile(WangCornerTile tile, int x, int y)
        {
            int index = (this.Width * x) + y;
            this.TileSlots[index].Tile=tile;
        }

        public (WangCornerTile tile, int x, int y) AddTileToAdjacent(WangCornerTile tile,int x, int y)
        {
            var pos=chooseRandomAdjacentSide(x,y);
            WangCornerTile[] matchTiles = new WangCornerTile[1];

            // find matches to the chosen adjacent side of the placed tile
            switch (pos.direction){
                case 0:
                    // north
                    matchTiles = this.TileSet[0].ReturnMatches(this.TileSet[0].Tiles,Color.MatchAll,Color.MatchAll,tile.CornerColorNE,tile.CornerColorNW);
                    break;
                case 1:
                    // east
                    matchTiles = this.TileSet[0].ReturnMatches(this.TileSet[0].Tiles,tile.CornerColorNE,Color.MatchAll,Color.MatchAll,tile.CornerColorSE);
                    break;
                case 2:
                    // south
                    matchTiles = this.TileSet[0].ReturnMatches(this.TileSet[0].Tiles,tile.CornerColorSW,tile.CornerColorSE,Color.MatchAll,Color.MatchAll);
                    break;
                case 3:
                    // west
                    matchTiles = this.TileSet[0].ReturnMatches(this.TileSet[0].Tiles,Color.MatchAll,tile.CornerColorNW,tile.CornerColorSW,Color.MatchAll);
                    break;
            }
            
            Random randomIndex = new Random();
            // Select random tile to place
            int randIndex = randomIndex.Next(0,matchTiles.Length);

            WangCornerTile newTile=matchTiles[randIndex];
            this.PlaceTile(newTile,pos.x,pos.y);

            return (tile:newTile,x:pos.x,y:pos.y);
        }

        (int x,int y, int direction) chooseRandomAdjacentSide(int x, int y){

            // randomize options
            // 0-north, 1-east, 2-south, 3-west
            var sideOptions = new int[]{0,1,2,3};
            Random rand = new Random();
            sideOptions = sideOptions.OrderBy(x => rand.Next()).ToArray();

            int option=0;
            int xPos=0;
            int yPos=0;
            for (int i=0;i<4;i++){
                option=sideOptions[i];

                switch (option){
                case 0:
                    // north
                    xPos=x-1;
                    yPos=y;
                    
                    if ((xPos<0 || xPos>=this.Width) || (yPos<0 || yPos>=this.Height)){
                        continue;
                    }
                    break;
                case 1:
                    // east
                    xPos=x;
                    yPos=y+1;

                    if ((xPos<0 || xPos>=this.Width) || (yPos<0 || yPos>=this.Height)){
                        continue;
                    }
                    break;
                case 2:
                    // south
                    xPos=x+1;
                    yPos=y;

                    if ((xPos<0 || xPos>=this.Width) || (yPos<0 || yPos>=this.Height)){
                        continue;
                    }
                    break;
                case 3:
                    // west
                    xPos=x;
                    yPos=y-1;

                    if ((xPos<0 || xPos>=this.Width) || (yPos<0 || yPos>=this.Height)){
                        continue;
                    }
                    break;
                }

                if (isValidPosition(xPos,yPos) && !isTileAlreadyExist(xPos,yPos)){
                        return (x:xPos,y:yPos,direction:option);
                }
            }

            return (x:0,y:0,direction:0); 
        }

        bool isValidPosition(int x, int y){
            if (x >=0 && x<this.Width && y>=0 && y<this.Height){
                return true;
            }

            return false;
        }

         bool isTileAlreadyExist(int x, int y){
            int index = (this.Width*x)+y;
            if (this.TileSlots[index].Tile==null){
                return false;
            }

            return true;
        }
    }

    struct BoardTileSlots
    {
        public WangCornerTile Tile;
        // Add field for number of violations for north south east west
    }
}