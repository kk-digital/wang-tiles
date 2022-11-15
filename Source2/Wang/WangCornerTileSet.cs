using System.Linq;

namespace Wang
{

 class WangCornerTileSet
 {
    /* Properties of WangCornerTileSet */
    public WangCornerTile[]? Tiles;

    /* Constructor for WangCornerTile */
    public WangCornerTileSet()
    {
      // TODO: Should be changed so that
      // length will not be hard coded.
      // Tiles = new WangCornerTile[999];
    }

    /* Methods of WangCornerTileSet */
    public WangCornerTile CreateTile(TileGeometry geometry, Color colorNW, Color colorNE, Color colorSE, Color colorSW)
    {
      WangCornerTile newTile = new WangCornerTile(geometry,colorNW,colorNE,colorSE,colorSW); 

      if (this.Tiles==null){
         this.Tiles=new WangCornerTile[1];
         this.Tiles[0]=newTile;
      } else {
         this.Tiles=this.Tiles.Append(newTile).ToArray();
      }

      //  Update ColorCountMap
      // Globals.ColorCountMap[colorNW]=Globals.ColorCountMap[colorNW]++;
      // Globals.ColorCountMap[colorNE]=Globals.ColorCountMap[colorNE]++;
      // Globals.ColorCountMap[colorSE]=Globals.ColorCountMap[colorSE]++;
      // Globals.ColorCountMap[colorSW]=Globals.ColorCountMap[colorSW]++;

      return newTile;
    }

   // numberOfVariants is the number of 
   // variants per color combination.
   // numberOfColors is the number of 
   // colors available to make a combination.
    public WangCornerTileSet GenerateTileSet(int numberOfColors, int numberOfVariants)
    {
      int NW, NE, SE, SW;
      WangCornerTileSet tileSet = new WangCornerTileSet();

      // +2 because colors in our enum starts with 2
      // 0 and 1 are special cases
      numberOfColors=numberOfColors+2; 
      for (NW = 2; NW < numberOfColors; NW++) {
         for (NE = 2; NE < numberOfColors; NE++) {
            for (SE = 2; SE < numberOfColors; SE++) {
               for (SW = 2; SW < numberOfColors; SW++) {
                  WangCornerTile newTile=CreateTile(TileGeometry.FP,(Color)NW,(Color)NE,(Color)SE,(Color)SW);

                  if (tileSet.Tiles==null){
                     tileSet.Tiles=new WangCornerTile[1];
                     tileSet.Tiles[0]=newTile;
                  } else {
                     tileSet.Tiles=tileSet.Tiles.Append(newTile).ToArray();
                  }
               }
            }
         }
	   } 

      return tileSet;
    }

    public WangCornerTile[] ReturnMatches(WangCornerTile[] Tiles,Color colorNW, Color colorNE, Color colorSE, Color colorSW)
    {
      WangCornerTile[]? tileMatches=null;
   
      for (int i=0; i<Tiles.Length;i++){
         if ((colorNW==Color.MatchAll || colorNW==Tiles[i].CornerColorNW) && (colorNE==Color.MatchAll || colorNE==Tiles[i].CornerColorNE)
         && (colorSE==Color.MatchAll || colorSE==Tiles[i].CornerColorSE)&& (colorSW==Color.MatchAll || colorSW==Tiles[i].CornerColorSW)){
             if (tileMatches==null){
               tileMatches = new WangCornerTile[1];
               tileMatches[0] = Tiles[i];
            }else {
               tileMatches=tileMatches.Append(Tiles[i]).ToArray();
            }

         }

      }
        
    return tileMatches;
    }
 }   
}