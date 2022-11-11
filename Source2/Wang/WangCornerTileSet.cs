namespace Wang
{

 class WangCornerTileSet
 {
    /* Properties of WangCornerTileSet */
    public WangCornerTile[]? Tiles;

    /* Methods of WangCornerTileSet */
    public void CreateTile(TileGeometry geometry, int colorNW, int colorNE, int colorSE, int colorSW)
    {
       WangCornerTile newTile = new WangCornerTile(geometry,colorNW,colorNE,colorSE,colorSW);
    }
 }   
}