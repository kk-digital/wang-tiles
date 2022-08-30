using System;
using System.Reflection;
using Wang;
using Wang.MathW;
using Wang.Other;

namespace Wang.Board
{
    public class TileBoard
    {
        public string Type = "Board";
        public long ID;
        public int SizeX;
        public int SizeY;
        public int TileSetCount;

        public BoardSlot[] BoardSlots;

        public string CreationDate;
        public ulong CreationDateUnixTime;

        public TileBoard(long id, int sizeX, int sizeY)
        {
            ID = id;
            SizeX = sizeX;
            SizeY = sizeY;
            TileSetCount = 1;

            BoardSlots = new BoardSlot[SizeX * SizeY];

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    BoardSlot slot = new BoardSlot();
                    slot.TileSetID = 0;
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
            TileBoard tileBoard = new TileBoard(Utils.GenerateID(), sizeX, sizeY);
            Random random = new Random();
            MathW.Perlin1D perlin = new MathW.Perlin1D(
                seed: random.Next(), 
                samplingRate: 8, 
                octaves: 1);
            float[] cutoffZ = new float[sizeX];

            for (int i = 0; i < sizeX; i++)
            {
                cutoffZ[i] = perlin.GetNoise(i);
            }

            for (int i = 0; i < tileBoard.BoardSlots.Length; i++)
            {
                tileBoard.BoardSlots[i].TileIsoType = TileIsoType.FullBlock;
                tileBoard.BoardSlots[i].Layer = Layer.LayerBack;
                tileBoard.BoardSlots[i].TileSetID = 0;
                if ((tileBoard.BoardSlots[i].yPosition / (float)sizeY - cutoffZ[tileBoard.BoardSlots[i].xPosition]) > - 0.05f)
                    tileBoard.BoardSlots[i].TileIsoType = TileIsoType.EmptyBlock;
            }

            return tileBoard;

        }

        /// <summary>
        ///  Polar Perlin Noise Loops
        /// </summary>
        public static TileBoard MakeRadial(int sizeX, int sizeY)
        {

            TileBoard tileBoard = new TileBoard(Utils.GenerateID(), sizeX, sizeY);
            Random random = new Random();
            MathW.PerlinField2D perlinNoise = new MathW.PerlinField2D(
                seed: random.Next(),
                samplingRate: 1,
                octaves: 1);

            float[] distances = new float[sizeX * sizeY];
            float[] samples = new float[sizeX * sizeY];

            float max = float.MinValue;
            for (int y = 0, index = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    // Normalize postion.
                    // Use centralize tile position.
                    float posX = (x + 0.5f) / sizeX;
                    float posY = (y + 0.5f) / sizeY;

                    distances[index] = MathW.Distance.EuclidianDistance(posX, posY, 0.5f, 0.5f);
                    float angles = MathF.Atan2(posY - 0.5f, posX - 0.5f);

                    if (distances[index] > max)
                        max = distances[index];

                    const int RADIOUS = 6; // Perlin noise loop radious
                    samples[index] = perlinNoise.GetNoise(MathF.Cos(angles) + 1 * RADIOUS, MathF.Sin(angles) + 1 * RADIOUS); // Add +1 to keep x,y > 0.
                    samples[index] = (samples[index] + 1f) / 2f; // Normalize.
                    index++;
                }
            }

            for (int i = 0; i < distances.Length; i++)
            {
                // Normalize distance.
                distances[i] /= max;

                // Todo: Add incomplete beta function.

                if (distances[i] > samples[i])
                    tileBoard.BoardSlots[i].Layer = Layer.LayerFront;
                else
                    tileBoard.BoardSlots[i].Layer = Layer.LayerBack;
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
                tileBoard.BoardSlots[i].TileSetID = 0;
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
                tileBoard.BoardSlots[i].TileSetID = 0;
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