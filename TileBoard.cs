namespace wang_tiles
{
    public class TileBoard
    {
        public Int64 ID;
        public int SizeX;
        public int SizeY;

        public BoardSlot[] BoardSlots;

        public string CreationDate;
        public UInt64 CreationDateUnixTime;

        public TileBoard(Int64 id, int sizeX, int sizeY)
        {
            ID = id;
            SizeX = sizeX;
            SizeY = sizeY;

            BoardSlots = new BoardSlot[SizeX * SizeY];

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    BoardSlot slot = new BoardSlot();
                    slot.xPosition = x;
                    slot.yPosition = y;
                    slot.TileIsoType = TileIsoType.FullBlock;
                    slot.TileType = TileType.TileTypeSquareTile;
                    slot.Layer = Layer.LayerBack;
                    BoardSlots[y * SizeX + x] = slot;
                }
            }
        }
    }
}