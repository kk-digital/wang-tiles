using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace wang_tiles
{
    public struct BoardSlot
    {
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