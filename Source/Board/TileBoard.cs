using Wang;
using Wang.Other;

namespace Wang.Board
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

        public static TileBoard MakeBackground(int sizeX, int sizeY)
        {
            TileBoard tileBoard = new TileBoard(Utils.GenerateID(), sizeX, sizeY); // Note(Joao) : should we use generateID for tileboard?
            Random rand = new Random();
            float[] cutoffZ = new float[sizeX];

            for (int i = 0; i < sizeX; i++)
            {
                cutoffZ[i] = rand.Next(); // (Todo) 1D perlin instead.
            }

            for (int i = 0; i < tileBoard.BoardSlots.Length; i++)
            {
                tileBoard.BoardSlots[i].Layer = Layer.LayerBack;
                if (tileBoard.BoardSlots[i].yPosition > cutoffZ[i])
                    tileBoard.BoardSlots[i].TileIsoType = TileIsoType.EmptyBlock;
                else
                    tileBoard.BoardSlots[i].TileIsoType = TileIsoType.FullBlock;
            }

        }


        public static TileBoard MakeRadial(int sizeX, int sizeY)
        {

            TileBoard tileBoard = new TileBoard(Utils.GenerateID(), sizeX, sizeY);

            MathW.PerlinNoise perlinNoise = new MathW.PerlinNoise();
            perlinNoise.init(sizeX, sizeY);

            // Create linear gradient function. Note(Joao) (Beta cdf are better here (Todo?))
            float[] distance = new float[sizeX * sizeY];
            int k = 2; // Coefiecient used to tune gradient. 

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    // Normalize postion.
                    // Use centralize tile position.
                    float posX = (x + 0.5f) / sizeX;
                    float posY = (y + 0.5f) / sizeY;

                    distance[(x + y * sizeX)] = k *  MathW.Distance.EuclidianDistance(posX, posY, 0.5f, 0.5f);
                }
            }


            // Start from the center.
            // First half. 
            int maxY = sizeY;
            for (int x= sizeX / 2; x < sizeX; x++)
            {
                for (int y = sizeY / 2; y < maxY; y++)
                {
                    float sample = perlinNoise.noise(x, y);

                    int index = (x + y * sizeX);

                    tileBoard.BoardSlots[index].TileIsoType = TileIsoType.FullBlock;

                    float diff = distance[index] - sample;
                    
                    if (diff >= 0)
                    {
                        // fill all cell after this tile.
                        for (int i = x; i < sizeX; i++)
                        {
                            for (int j = y; j < sizeY; j++)
                            {
                                index = i + j * sizeX;
                                tileBoard.BoardSlots[index].Layer = Layer.LayerFront;
                            }
                            maxY = y;
                        }
                    }
                }
            }

            // Second half.
            int minY = 0;
            for (int x = - 1 + sizeX / 2; x >= 0; x--)
            {
                for (int y = -1 + sizeY / 2; y >= minY; y--)
                {
                    float sample = perlinNoise.noise(x, y); // Not working (Todo: Fix this.)

                    int index = (x + y * sizeX);

                    tileBoard.BoardSlots[index].TileIsoType = TileIsoType.FullBlock;

                    float diff = distance[index] - sample;

                    if (diff >= 0)
                    {
                        // fill all cell after this tile.
                        for (int i = x; i >= 0; i--)
                        {
                            for (int j = y; j >= 0; j--)
                            {
                                index = i + j * sizeX;
                                tileBoard.BoardSlots[index].Layer = Layer.LayerFront;
                            }
                            minY = y;
                        }
                    }
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
                if (tileBoard.BoardSlots[i].yPosition < cutOffY)
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