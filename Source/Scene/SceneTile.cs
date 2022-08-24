using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wang
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
    }
}