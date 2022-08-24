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

        public void SetTile(int x, int y, Layer layer, SceneTile sceneTile)
        {
            Debug.Assert(x >= 0 && x < SizeX && y >= 0 && y < SizeY);

            SceneTiles[(int)layer][y * SizeX + x] = sceneTile;
        }


        public string SaveJson(string filename)
        {

            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText(Constants.OutputPath + "\\" + filename, json);

            return json;
        }

        public SceneTile[] FindMatchingCorners(TileSize tileSize, int topColor, int bottomColor, int rightColor, 
            int leftColor) 
        {
            SceneTile[] tileSet = new SceneTile[LayerCount];
            Random rnd = new Random();

            for(int i = 0; i < tileSet.Length; i++)
            {
                SceneTile sceneTile = SceneTiles[(int)Layer.LayerFront][rnd.Next(-5000, 5000) + rnd.Next(-5000, 5000) * SizeX];

                EdgeTileInformation tile = TileSets[sceneTile.TileSetID].InformationArray[sceneTile.TileID];

                if(tile.TileID == -1) 
                {
                    if(tile.TopColor == topColor)
                        tileSet.Append<SceneTile>(sceneTile);
                    else if(tile.BottomColor == bottomColor)
                        tileSet.Append<SceneTile>(sceneTile);
                    else if(tile.RightColor == rightColor)
                        tileSet.Append<SceneTile>(sceneTile);
                    else if(tile.LeftColor == leftColor)
                        tileSet.Append<SceneTile>(sceneTile);
                }
            }

            if(tileSet.Length <= 0)
            {
                //return -2
                // No Matching Found
            }
            else if (tileSet.Length > 0) 
            {
                SceneTile randomTile = tileSet[rnd.Next(0, tileSet.Length)];
            }

            return tileSet;
        }
    }
}