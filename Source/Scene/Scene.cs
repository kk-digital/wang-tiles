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

            File.WriteAllText(Constants.OutputPath + "/" + filename, json);

            return json;
        }

        public static SceneTile[] FindMatchingCorners(TileSize tileSize, Scene scene, int topColor, int bottomColor, int rightColor, 
            int leftColor) 
        {
            SceneTile[] tileSet = new SceneTile[100];
            Random rnd = new Random();

            for(int i = 0; i < tileSet.Length; i++)
            {
                SceneTile sceneTile = scene.SceneTiles[(int)Layer.LayerFront][rnd.Next(0, scene.SizeX) + rnd.Next(0, scene.SizeY) * scene.SizeX];
                EdgeTileInformation tile = new EdgeTileInformation();

                if(sceneTile.TileSetID != -1 || sceneTile.TileID != -1)
                {
                    tile = scene.TileSets[sceneTile.TileSetID].InformationArray[sceneTile.TileID];
                }
            
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

            return tileSet;
        }

        public static Scene Algorithm1(TileSize tileSize)
        {
            Scene _scene = new Scene(Wang.Other.Utils.GenerateID(), 5, 5);
            EdgeTileSet newTileSet = EdgeTileSet.NewWangCompleteTileset(tileSize, 2, 2, 1);
            _scene.AddTileSet(newTileSet);

            Random rnd = new Random();

            SceneTile sceneTile = _scene.SceneTiles[(int)Layer.LayerFront][rnd.Next(0, _scene.SizeX) + rnd.Next(0, _scene.SizeY) * _scene.SizeX];

            EdgeTileInformation tile = new EdgeTileInformation();

            if(sceneTile.TileSetID != -1 || sceneTile.TileID != -1)
            {
                tile = _scene.TileSets[sceneTile.TileSetID].InformationArray[sceneTile.TileID];
            }

            while(tile.TileID == -1)
            {
                // Find All Matching Edge Conditions
                SceneTile[] tiles = FindMatchingCorners(tileSize, _scene, tile.TopColor, tile.BottomColor, tile.RightColor, 
                    tile.LeftColor);
                
                // Any matching conditions found?
                if(tiles.Length > 0)
                {
                    SceneTile placeTile = tiles[rnd.Next(0, tiles.Length)];
                    EdgeTileInformation placeTileProp = _scene.TileSets[placeTile.TileSetID].InformationArray[placeTile.TileID];

                    // Place Random Tile Selected From Matching Corners List
                    _scene.SetTile(placeTile.xPosition, placeTile.xPosition, Layer.LayerFront, 
                        sceneTile);
                }
            }

            return _scene;
        }
    }
}