namespace wang
{
    public class TileBoard
    {
        int SizeX;
        int SizeY;

        BoardSlot[] BoardSlots;

        TileBoard(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            BoardSlots = new BoardSlot[SizeX * SizeY];

            for(int y = 0; y < SizeY; y++)
            {
                for(int x = 0; x < SizeX; x++)
                {
                    BoardSlot slot = new BoardSlot();
                    slot.TileIsoType = TileIsoType.FullBlock;
                    slot.TileType = TileType.TileTypeSquareTile;
                    slot.Layer = Layer.LayerBack;
                    BoardSlots[y * SizeX + x] = slot;
                }
            }
        }
    }
}