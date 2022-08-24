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
            // Todo(Joao): Implement this.
            // Different distance in each row and collum.
            // How smooth should it be?

            TileBoard tileBoard = new TileBoard(Utils.GenerateID(), sizeX, sizeY); //Note(Joao) : should we use generateID for tileboard?

            int centerX = sizeX / 2;
            int centerY = sizeY / 2;

            Random rand = new Random(); // Note(joao): Use perlin noise instead.
            int radius = rand.Next(Math.Min(sizeX / 2, sizeY / 2));

            for (int i = 0; i < tileBoard.BoardSlots.Length; i++)
            {
                int distance = (int)MathW.Distance.EuclidianDistance(tileBoard.BoardSlots[i].xPosition, tileBoard.BoardSlots[i].yPosition, centerX, centerY);
                int diff = distance - radius;

                tileBoard.BoardSlots[i].TileIsoType = TileIsoType.FullBlock;
                if (diff >= 0)
                {
                    tileBoard.BoardSlots[i].Layer = Layer.LayerFront;
                }
                else
                {
                    tileBoard.BoardSlots[i].Layer = Layer.LayerBack;
                }
            }

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