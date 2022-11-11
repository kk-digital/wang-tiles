namespace Wang
{

    class WangCornerTile
    {
        /* Properties of WangCornerTile */
        public int TileID;
        public TileGeometry TileGeometry;
        public int CornerColorNW;
        public int CornerColorNE;
        public int CornerColorSE;
        public int CornerColorSW;

        /* Constructor for WangCornerTile */
        public WangCornerTile(TileGeometry tileGeometry, int cornerColorNW, int cornerColorNE, int cornerColorSE, int cornerColorSW)
        {
            TileGeometry = tileGeometry;
            CornerColorNW = cornerColorNW;
            CornerColorNE = cornerColorNE;
            CornerColorSE = cornerColorSE;
            CornerColorSW = cornerColorSW;
        }
    }
}