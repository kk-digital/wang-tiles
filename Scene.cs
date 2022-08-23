namespace wang_tiles
{
    public class Scene
    {
        public static int LayerCount = Enum.GetNames(typeof(Layer)).Length;
        public Int64 ID;
        public int SizeX;
        public int SizeY;

        public SceneTile[][] SceneTiles;

        public EdgeTileSet[] TileSets;

        public string CreationDate;
        public UInt64 CreationDateUnixTime;



        
        public Scene(Int64 id, int sizeX, int sizeY)
        {
            ID = id;
            SizeX = sizeX;
            SizeY = sizeY;

           
            SceneTiles = new SceneTile[LayerCount][];
           

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

        public void AddTileSet(EdgeTileSet edgeTileSet)
        {
            //TODO(Mahdi): Implement
        }
    }
}