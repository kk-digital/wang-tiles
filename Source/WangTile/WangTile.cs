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

        public int TileBitMask;

        /* Only used for Tiled tileset */
        public int[]? TileData;

        /* Constructor for WangTile */
        public WangTile(CornerColor cornerColorNW, CornerColor cornerColorNE, CornerColor cornerColorSE, CornerColor cornerColorSW, VerticalColor edgeColorN, HorizontalColor edgeColorE, VerticalColor edgeColorS, HorizontalColor edgeColorW, int[] tileData = null)
        {
            this.CornerColorNW = cornerColorNW;
            this.CornerColorNE = cornerColorNE;
            this.CornerColorSE = cornerColorSE;
            this.CornerColorSW = cornerColorSW;

            this.EdgeColorNorth=edgeColorN;
            this.EdgeColorEast=edgeColorE;
            this.EdgeColorSouth=edgeColorS;
            this.EdgeColorWest=edgeColorW;

            this.TileBitMask=0;

            this.TileData=tileData;
        }

        /* Methods for WangTile */
        public void ClearBitfield(){
            this.TileBitMask=0;
        }

        public void SetBit(BitMask bit){
            this.TileBitMask=this.TileBitMask | (1<<(int)bit);
        }

        public void MaskAllCorners(){
            for (BitMask i=BitMask.NW_8NE;i<=BitMask.SW_8SE;i++){
                this.SetBit(i);
            }
        }

        public void MaskAllEdges(){
            for (BitMask i=BitMask.N_2S;i<=BitMask.W_8E;i++){
                this.SetBit(i);
            }
        }
    }
}