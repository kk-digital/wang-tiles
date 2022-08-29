using Wang;
using Wang.EdgeTile;
using System.Diagnostics;
using System.Text.Json;
using System;
using Newtonsoft.Json;

namespace Wang.SceneW
{
    public partial class Scene
    {
        public string Type = "Scene";

        public static int LayerCount = Enum.GetNames(typeof(Layer)).Length;
        public Int64 ID;
        public int SizeX;
        public int SizeY;

        public SceneTile[][] SceneTiles;

        public EdgeTileSet[] TileSets;
        public int TileSetsCount;

        public string CreationDate;
        public UInt64 CreationDateUnixTime;




        public Scene(Int64 id, int sizeX, int sizeY)
        {
            ID = id;
            SizeX = sizeX;
            SizeY = sizeY;
            TileSetsCount = 0;

           
            SceneTiles = new SceneTile[LayerCount][];
            TileSets = new EdgeTileSet[1];

            DateTimeOffset dto = DateTimeOffset.Now;

            CreationDate = dto.ToString();
            CreationDateUnixTime = (UInt64)dto.ToUnixTimeSeconds();
           

            for(int layer = 0; layer < LayerCount; layer++)
            {
                SceneTiles[layer] = new SceneTile[SizeX * SizeY];
                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        SceneTile slot = new SceneTile();
                        slot.xPosition = x;
                        slot.yPosition = y;
                        slot.TileIsoType = TileIsoType.FullBlock;
                        slot.TileType = TileType.TileTypeSquareTile;
                        slot.TileID = -1;
                        slot.TileSetID = -1;
                        SceneTiles[layer][y * SizeX + x] = slot;
                    }
                }
            }
        }

        public int AddTileSet(EdgeTileSet edgeTileSet)
        {
            //TODO(Mahdi): Implement
            if (TileSetsCount == TileSets.Length)
            {
                Array.Resize(ref TileSets, TileSetsCount + 1);
            }

            TileSets[TileSetsCount] = edgeTileSet;
            int index = TileSetsCount;
            TileSetsCount++;

            return index;
        }

        struct TilesetSlot
        {
            public int TileID;
            public int TileSetID;
        }

        TilesetSlot[] GetListOfValidTiles(int topColor, int bottomColor, int rightColor, int leftColor)
        {

            TilesetSlot[] result = new TilesetSlot[128];
            int resultCount = 0;

            for(int tilesetIndex = 0; tilesetIndex < TileSetsCount; tilesetIndex++)
            {
                EdgeTileSet tileSet = TileSets[tilesetIndex];
                for (int i = 0; i < tileSet.TileCount; i++)
                {
                    EdgeTileInformation tile = tileSet.InformationArray[i];
                    if (topColor != -1 && tile.TopColor != topColor)
                    {
                        continue;
                    }
                    if (bottomColor != -1 && tile.BottomColor != bottomColor)
                    {
                        continue;
                    }
                    if (rightColor != -1 && tile.RightColor != rightColor)
                    {
                        continue;
                    }

                    if (leftColor != -1 && tile.LeftColor != leftColor)
                    {
                        continue;
                    }

                    result[resultCount++] = new TilesetSlot{TileID = i, TileSetID = tilesetIndex};
                    if (resultCount == result.Length)
                    {
                        Array.Resize(ref result, result.Length + 128);
                    }
                }
            }

            TilesetSlot[] finalResult = new TilesetSlot[resultCount];
            for(int i = 0; i < resultCount; i++)
            {
                finalResult[i] = result[i];
            }

            return finalResult;

        }

        public void Algorithm1()
        {
            DateTimeOffset dto = DateTimeOffset.Now;
            Mt19937.init_genrand((ulong)dto.ToUnixTimeSeconds());

            int[] unassignedArray = new int[SizeY * SizeX];
            int unassignedSize = unassignedArray.Length;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    unassignedArray[y * SizeX + x] = y * SizeX + x;
                }
            }


            while(unassignedSize > 0)
            {
                int random = Math.Abs((int)Mt19937.genrand_int32() % unassignedSize);
                int randomIndex = unassignedArray[random];

                ref SceneTile tile = ref SceneTiles[(int)Layer.LayerFront][randomIndex];
                int leftEdgeColor = -1;
                int rightEdgeColor = -1;
                int topEdgeColor = -1;
                int bottomEdgeColor = -1;

                int leftIndex = randomIndex - 1;
                if (leftIndex >= 0)
                {
                    SceneTile leftTile = SceneTiles[(int)Layer.LayerFront][leftIndex];
                    if (leftTile.TileSetID != -1 && leftTile.TileID != -1)
                    {
                        EdgeTileInformation leftTileInformation = TileSets[leftTile.TileSetID].InformationArray[leftTile.TileID];
                        leftEdgeColor = leftTileInformation.RightColor;
                    }
                }


                int rightIndex = randomIndex + 1;
                if (rightIndex < SizeY * SizeX)
                {
                    SceneTile rightTile = SceneTiles[(int)Layer.LayerFront][rightIndex];
                    if (rightTile.TileSetID != -1 && rightTile.TileID != -1)
                    {
                        EdgeTileInformation rightTileInformation = TileSets[rightTile.TileSetID].InformationArray[rightTile.TileID];
                        rightEdgeColor = rightTileInformation.LeftColor;
                    }
                }

                int topIndex = randomIndex - SizeX;
                if (topIndex >= 0)
                {
                    SceneTile topTile = SceneTiles[(int)Layer.LayerFront][topIndex];
                    if (topTile.TileSetID != -1 && topTile.TileID != -1)
                    {
                        EdgeTileInformation topTileInformation = TileSets[topTile.TileSetID].InformationArray[topTile.TileID];
                        topEdgeColor = topTileInformation.BottomColor;
                    }
                }

                int bottomIndex = randomIndex + SizeX;
                if (bottomIndex < SizeY * SizeX)
                {
                    SceneTile bottomTile = SceneTiles[(int)Layer.LayerFront][bottomIndex];
                    if (bottomTile.TileSetID != -1 && bottomTile.TileID != -1)
                    {
                        EdgeTileInformation bottomTileInformation = TileSets[bottomTile.TileSetID].InformationArray[bottomTile.TileID];
                        bottomEdgeColor = bottomTileInformation.TopColor;
                    }
                }

                TilesetSlot[] slots = GetListOfValidTiles(topEdgeColor, bottomEdgeColor, rightEdgeColor, leftEdgeColor);


                
                if (slots.Length > 0)
                {
                    int randomSlotIndex = Math.Abs((int)Mt19937.genrand_int32() % slots.Length);

                    ref TilesetSlot slot = ref slots[randomSlotIndex]; 
                    tile.TileSetID = slot.TileSetID;
                    tile.TileID = slot.TileID;
                }


                
                unassignedSize--;
                unassignedArray[random] = unassignedArray[unassignedSize];
            }


        }



        public void SetTile(int x, int y, Layer layer, SceneTile sceneTile)
        {
            Debug.Assert(x >= 0 && x < SizeX && y >= 0 && y < SizeY);

            SceneTiles[(int)layer][y * SizeX + x] = sceneTile;
        }


        public string SaveJson(string filename)
        {

            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText(Constants.OutputPath + "/" + filename, json);

            return json;
        }
    }
}