namespace Wang
{

    public class WangCornerTile
    {
        /* Properties of WangCornerTile */
        public int TileID;
        public TileGeometry TileGeometry;
        public Color CornerColorNW;
        public Color CornerColorNE;
        public Color CornerColorSE;
        public Color CornerColorSW;

        /* Constructor for WangCornerTile */
        public WangCornerTile(TileGeometry tileGeometry, Color cornerColorNW, Color cornerColorNE, Color cornerColorSE, Color cornerColorSW)
        {
            TileGeometry = tileGeometry;
            CornerColorNW = cornerColorNW;
            CornerColorNE = cornerColorNE;
            CornerColorSE = cornerColorSE;
            CornerColorSW = cornerColorSW;
        }
    }
}