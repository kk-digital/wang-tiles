using System.Diagnostics;

namespace wang
{


    // TODO(Mahdi): https://github.com/kk-digital/kcg/issues/562
    public partial class EdgeTileSet
    {
        public TileSize TileSize;
        public int VerticalColorsCount;
        public int HorizontalColorsCount;
        public TileInformation[] InformationArray;


        

        public void InitEdgeTileSet()
        {
            // TODO(Mahdi): Implement
        }


        public void AddTile(TileType tileType, int topColor, int bottomColor, int leftColor, int rightColor)
        {
            Debug.Assert(topColor >= -1 && topColor < HorizontalColorsCount);
            Debug.Assert(bottomColor >= -1 && bottomColor < HorizontalColorsCount);
            Debug.Assert(rightColor >= -1 && rightColor < VerticalColorsCount);
            Debug.Assert(leftColor >= -1 && leftColor < VerticalColorsCount);

            // TODO(Mahdi): Implement

        }

        public void FinalizeTileSet()
        {
            // TODO(Mahdi): Implement
        }
    }
}