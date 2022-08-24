using Wang;

namespace Wang
{
    public class TileBoard
    {
        public long ID;
        public int SizeX;
        public int SizeY;

        public BoardSlot[] BoardSlots;

        public string CreationDate;
        public ulong CreationDateUnixTime;

        public TileBoard(long id, int sizeX, int sizeY)
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

            CreationDate = DateTime.Now.ToString();
        }

        public static TileBoard MakeRadial(int sizeX, int sizeY)
        {
            TileBoard tileBoard = new TileBoard(Utils.GenerateID(), sizeX, sizeY); //Note(Joao) : should we use generateID for tileboard?

            Random rand = new Random();
            int x = rand.Next(sizeX);
            int y = rand.Next(sizeY);



            return tileBoard;
        }

        public static TileBoard MakeFlat(int sizeX, int sizeY)
        {
            TileBoard tileBoard = new TileBoard(Utils.GenerateID(), sizeX, sizeY);

            int cutOffY = sizeY / 2;

            for (int i = 0; i < tileBoard.BoardSlots.Length; i++)
            {
                tileBoard.BoardSlots[i].Layer = Layer.LayerFront;
                if (tileBoard.BoardSlots[i].yPosition > cutOffY)
                    tileBoard.BoardSlots[i].TileIsoType = TileIsoType.FullBlock;
                else
                    tileBoard.BoardSlots[i].TileIsoType = TileIsoType.EmptyBlock;
            }

            return tileBoard;
        }

        public static TileBoard MakeFloatingIsland3x3()
        {
            TileBoard tileBoard = new TileBoard(Utils.GenerateID(), 5, 5);

            for (int i = 0; i < tileBoard.BoardSlots.Length; i++)
            {
                tileBoard.BoardSlots[i].Layer = Layer.LayerFront;
                if (tileBoard.BoardSlots[i].yPosition >= 1 && tileBoard.BoardSlots[i].yPosition <= 3
                    && tileBoard.BoardSlots[i].xPosition >= 1 && tileBoard.BoardSlots[i].xPosition <= 3)
                    tileBoard.BoardSlots[i].TileIsoType = TileIsoType.FullBlock;
                else
                    tileBoard.BoardSlots[i].TileIsoType = TileIsoType.EmptyBlock;
            }

            return tileBoard;
        }
    }
}