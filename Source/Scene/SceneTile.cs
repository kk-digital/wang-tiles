using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wang;

namespace Wang.SceneW
{
    public struct SceneTile
    {
        public int xPosition;
        public int yPosition;
        public int TileID;
        public int TileSetID;
        [JsonConverter(typeof(StringEnumConverter))]
        public TileIsoType TileIsoType;
        [JsonConverter(typeof(StringEnumConverter))]
        public TileType TileType;


        public SceneTile(int x, int y, int tileId, int tileSetID, TileIsoType isoType, TileType type)
        {
            xPosition = x;
            yPosition = y;
            TileID = tileId;
            TileSetID = tileSetID;
            TileIsoType = isoType;
            TileType = type;
        }
    }
}