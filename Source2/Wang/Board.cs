namespace Wang
{
    class Board
    {
        public BoardTileSlots[] TileSlots;
        public int Height;
        public int Width;
        public WangCornerTileSet[] TileSet;

        // Constructor
        public Board(int height, int width)
        {
            TileSlots = new BoardTileSlots[height*width];
            Height = height;
            Width = width;

            TileSet=new WangCornerTileSet[99];
        }

        // Methods
        public void AddTileSet(WangCornerTileSet tileSet)
        {
            TileSet=TileSet.Append(tileSet).ToArray();
        }

        public void PlaceTile(WangCornerTile tile,int x, int y)
        {
            int index = (Width * x) + y;
            TileSlots[index].Tile=tile;
        }
    }

    struct BoardTileSlots
    {
        public WangCornerTile Tile;
    }
}