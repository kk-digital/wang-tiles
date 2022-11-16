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

        public void PlaceTile(WangCornerTile tile, int col, int row)
        {
            int index = Utils.GetBoardSlotIndex(this.Width,col,row);
            this.TileSlots[index].Tile=tile;
        }

        public (int col, int row) AddTileToAdjacent(int col, int row)
        {
            var pos=chooseRandomAdjacentSide(col,row);
            var adjacentColors=getTileAdjacentColors(pos.col,pos.row);
            var matchTiles=this.TileSet[0].ReturnMatches(this.TileSet[0].Tiles,adjacentColors.colorNW,adjacentColors.colorNE,adjacentColors.colorSE,adjacentColors.colorSW);
   
            Random randomIndex = new Random();
            // Select random tile to place
            int randIndex = randomIndex.Next(0,matchTiles.Length);

            WangCornerTile newTile=matchTiles[randIndex];
            this.PlaceTile(newTile,pos.col,pos.row);

            return (col:pos.col,row:pos.row);
        }

        public (int col,int row) AddTileToNextSlot(int col,int row){
            var pos = getNextTileSlot(col,row);

            var adjacentColors=getTileAdjacentColors(pos.col,pos.row);
            var matchTiles=this.TileSet[0].ReturnMatches(this.TileSet[0].Tiles,adjacentColors.colorNW,adjacentColors.colorNE,adjacentColors.colorSE,adjacentColors.colorSW);
   
            Random randomIndex = new Random();
            // Select random tile to place
            int randIndex = randomIndex.Next(0,matchTiles.Length);

            WangCornerTile newTile=matchTiles[randIndex];
            this.PlaceTile(newTile,pos.col,pos.row);

            return (col:pos.col,row:pos.row);
        }

        (Color colorNW, Color colorNE, Color colorSE, Color colorSW) getTileAdjacentColors(int col, int row){
            WangCornerTile? northTile = getNorthTile(col,row);
            WangCornerTile? eastTile = getEastTile(col,row);
            WangCornerTile? southTile = getSouthTile(col,row);
            WangCornerTile? westTile = getWestTile(col,row);

            Color colorNW = Color.MatchAll;
            Color colorNE = Color.MatchAll;
            Color colorSE = Color.MatchAll;
            Color colorSW = Color.MatchAll;

            if (northTile!=null){
                colorNW=northTile.CornerColorSW;
                colorNE=northTile.CornerColorSE;
            }

            if (eastTile!=null){
                colorNE=eastTile.CornerColorNW;
                colorSE=eastTile.CornerColorSW;
            }

            if (southTile!=null){
                colorSW=southTile.CornerColorNW;
                colorSE=southTile.CornerColorNE;
            }

            if (westTile!=null){
                colorNW=westTile.CornerColorNE;
                colorSW=westTile.CornerColorSE;
            }

            return (colorNW:colorNW,colorNE:colorNE,colorSE:colorSE,colorSW:colorSW);

        }

        (int col,int row) chooseRandomAdjacentSide(int col, int row){

            // randomize options
            // 0-north, 1-east, 2-south, 3-west
            var sideOptions = new int[]{0,1,2,3};
            Random rand = new Random();
            sideOptions = sideOptions.OrderBy(col => rand.Next()).ToArray();

            int option=0;
            (int col, int row) coord = (col:0,row:0);
            for (int i=0;i<4;i++){
                option=sideOptions[i];

                switch (option){
                case 0:
                    // north
                    coord = getNorthCoordinates(col,row);
                    
                    break;
                case 1:
                    // east
                    coord=getEastCoordinates(col,row);

                    break;
                case 2:
                    // south
                    coord=getSouthCoordinates(col,row);

                    break;
                case 3:
                    // west
                    coord=getWestCoordinates(col,row);
                  
                    break;
                }

                if (isValidPosition(coord.col,coord.row) && !isTileAlreadyExist(coord.col,coord.row)){
                        return (col:coord.col,row:coord.row);
                }
            }

            // No valid adjacent tile then,
            // find random slot that has no tile.
            coord= getRandomCoordinates();

            return (col:coord.col,row:coord.row); 
        }

        (int col, int row) getNextTileSlot(int col, int row){
            if (row==this.Width-1){
                return (col:col+1,row:0);
            } else {
                return (col:col, row:row+1);
            }
        }

        (int col,int row) getNorthCoordinates(int col,int row){
            return (col:col-1,row:row);
        }

        (int col,int row) getEastCoordinates(int col,int row){
            return (col:col,row:row+1);
        }

        (int col,int row) getSouthCoordinates(int col,int row){
            return (col:col+1,row:row);
        }

         (int col,int row) getWestCoordinates(int col,int row){
            return (col:col,row:row-1);
        }

        (int col, int row) getRandomCoordinates(){
            Random rand = new Random();

            while (true){
                int randX = rand.Next(0, this.Width);
                int randY = rand.Next(0, this.Height);

                if (!isTileAlreadyExist(randX,randY)){
                    return (col:randX,row:randY);
                }
            }
        }

        WangCornerTile? getNorthTile(int col, int row){
            (int col, int row) coord = getNorthCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                return this.TileSlots[index].Tile;
            }

            return null;
        }

        WangCornerTile? getEastTile(int col, int row){
            (int col, int row) coord = getEastCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                return this.TileSlots[index].Tile;
            }

            return null;
        }

        WangCornerTile? getSouthTile(int col, int row){
            (int col, int row) coord = getSouthCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                return this.TileSlots[index].Tile;
            }

            return null;
        }

        WangCornerTile? getWestTile(int col, int row){
            (int col, int row) coord = getWestCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                return this.TileSlots[index].Tile;
            }

            return null;
        }

        bool isValidPosition(int col, int row){
            if (col >=0 && col<this.Width && row>=0 && row<this.Height){
                return true;
            }

            return false;
        }

         bool isTileAlreadyExist(int col, int row){
            int index = (this.Width*col)+row;
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