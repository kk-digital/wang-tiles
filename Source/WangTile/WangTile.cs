namespace WangTile
{

    public class WangTile
    {
        /* Properties of WangCornerTile */
        public int TileID;

        public int CornerColorNW;
        public int CornerColorNE;
        public int CornerColorSE;
        public int CornerColorSW;

        public int EdgeColorNorth;
        public int EdgeColorSouth;

        public int EdgeColorWest;
        public int EdgeColorEast;

        public int BitMask;

        /* Constructor for WangTile */
        public WangTile(int cornerColorNW, int cornerColorNE, int cornerColorSE, int cornerColorSW, int edgeColorN, int edgeColorE, int edgeColorS, int edgeColorW)
        {
            this.CornerColorNW = cornerColorNW;
            this.CornerColorNE = cornerColorNE;
            this.CornerColorSE = cornerColorSE;
            this.CornerColorSW = cornerColorSW;

            this.EdgeColorNorth=edgeColorN;
            this.EdgeColorEast=edgeColorE;
            this.EdgeColorSouth=edgeColorS;
            this.EdgeColorWest=edgeColorW;

            this.BitMask=0;
        }

        public void ClearBitfield(){
            this.BitMask=0;
        }

        public void SetBit(BitMask bit){
            this.BitMask=this.BitMask | (1<<(int)bit);
        }
    }
}