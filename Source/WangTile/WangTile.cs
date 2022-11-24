namespace WangTile
{

    public class WangTile
    {
        /* Properties of WangTile */
        public int TileID;

        public CornerColor CornerColorNW;
        public CornerColor CornerColorNE;
        public CornerColor CornerColorSE;
        public CornerColor CornerColorSW;

        public VerticalColor EdgeColorNorth;
        public VerticalColor EdgeColorSouth;

        public HorizontalColor EdgeColorWest;
        public HorizontalColor EdgeColorEast;

        public int BitMask;

        /* Constructor for WangTile */
        public WangTile(CornerColor cornerColorNW, CornerColor cornerColorNE, CornerColor cornerColorSE, CornerColor cornerColorSW, VerticalColor edgeColorN, HorizontalColor edgeColorE, VerticalColor edgeColorS, HorizontalColor edgeColorW)
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

        /* Methods for WangTile */
        public void ClearBitfield(){
            this.BitMask=0;
        }

        public void SetBit(BitMask bit){
            this.BitMask=this.BitMask | (1<<(int)bit);
        }
    }
}