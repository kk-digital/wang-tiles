using System.Diagnostics;

namespace Wang.EdgeTile
{


    public struct EdgeTileInformation
    {
        public int TileID;
        public int TopColor;
        public int BottomColor;
        public int LeftColor;
        public int RightColor;

        public int VarientIndex(int HorizontalColorsCount, int VerticalColorsCount) {

             return LeftColor * (HorizontalColorsCount * VerticalColorsCount * VerticalColorsCount) + RightColor * (VerticalColorsCount * VerticalColorsCount) +
                            BottomColor * VerticalColorsCount + TopColor;
        }
    }
}