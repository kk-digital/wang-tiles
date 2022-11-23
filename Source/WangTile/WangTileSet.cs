namespace WangTile
{
    public class WangTileSet{
        public CornerColorData[] CornerColors;
        public HorizontalColorData[] HorizontalColors;
        public VerticalColorData[] VerticalColors;

        public WangTile[]? Tiles;

        /* Methods of WangCornerTileSet */
        public WangTile CreateTile(int cornerColorNW, int cornerColorNE, int cornerColorSE, int cornerColorSW, int edgeColorN, int edgeColorE, int edgeColorS, int edgeColorW)
        {
            WangTile newTile = new WangTile(cornerColorNW, cornerColorNE, cornerColorSE, cornerColorSW, edgeColorN, edgeColorE,edgeColorS, edgeColorW); 

            if (this.Tiles==null){
                this.Tiles=new WangTile[1];
                this.Tiles[0]=newTile;
            } else {
                this.Tiles=this.Tiles.Append(newTile).ToArray();
            }

            return newTile;
        }
    }
}