using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wang;

namespace Wang.Board
{
    public struct BoardSlot
    {
        public int TileSetID;
        public int xPosition;
        public int yPosition;
        [JsonConverter(typeof(StringEnumConverter))]
        public TileIsoType TileIsoType;
        [JsonConverter(typeof(StringEnumConverter))]
        public TileType TileType;
        [JsonConverter(typeof(StringEnumConverter))]
        public Layer Layer;
    }
}