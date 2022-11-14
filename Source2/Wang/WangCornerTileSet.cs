using System.Linq;

namespace Wang
{

 class WangCornerTileSet
 {
    /* Properties of WangCornerTileSet */
    public WangCornerTile[] Tiles;

    /* Constructor for WangCornerTile */
    public WangCornerTileSet()
    {
      // TODO: Should be changed so that
      // length will not be hard coded.
      Tiles = new WangCornerTile[999];
    }

    /* Methods of WangCornerTileSet */
    public WangCornerTile CreateTile(TileGeometry geometry, Color colorNW, Color colorNE, Color colorSE, Color colorSW)
    {
      WangCornerTile newTile = new WangCornerTile(geometry,colorNW,colorNE,colorSE,colorSW);  
      Tiles=Tiles.Append(newTile).ToArray();

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
      int count=0;
      WangCornerTileSet tileSet = new WangCornerTileSet();

      // +2 because colors in our enum starts with 2
      // 0 and 1 are special cases
      numberOfColors=numberOfColors+2; 
      for (NW = 2; NW < numberOfColors; NW++) {
         for (NE = 2; NE < numberOfColors; NE++) {
            for (SE = 2; SE < numberOfColors; SE++) {
               for (SW = 2; SW < numberOfColors; SW++) {
                  WangCornerTile newTile=CreateTile(TileGeometry.FP,(Color)NW,(Color)NE,(Color)SE,(Color)SW);
                  tileSet.Tiles[count]=newTile;
                  count++;
               }
            }
         }
	   } 

      return tileSet;
    }

    public WangCornerTile[] ReturnMatches(WangCornerTile[] Tiles,Color colorNW, Color colorNE, Color colorSE, Color colorSW)
    {
      // TODO: Should be changed so that
      // length will not be hard coded.
      WangCornerTile[] tileMatches=new WangCornerTile[999];

      // 1 - north
      // 2 - east
      // 3 - south
      // 4 - west
      int whichDirection = 0;
      if (colorSE == Color.MatchAll && colorSW== Color.MatchAll){
         whichDirection = 1;
      } else if (colorNW == Color.MatchAll && colorSW== Color.MatchAll){
         whichDirection = 2;
      } else if (colorNW == Color.MatchAll && colorNE== Color.MatchAll){
         whichDirection = 3;
      } else{
         whichDirection = 4;
      }

   // TilesLength
      for (int i =1; i<15;i++){
         switch (whichDirection){
            case 1:
               // then find matches north of the tile
               if (Tiles[i].CornerColorNW==colorNW && Tiles[i].CornerColorNE==colorNE){
                  tileMatches=tileMatches.Append(Tiles[i]).ToArray();
               }

               break;
            case 2:
               // then find matches east of the tile
               if (Tiles[i].CornerColorNE==colorNE && Tiles[i].CornerColorSE==colorSE){
                  tileMatches=tileMatches.Append(Tiles[i]).ToArray();
               }

               break;
            case 3:
               // then find matches south of the tile
               if (Tiles[i].CornerColorSE==colorSE && Tiles[i].CornerColorSW==colorSW){
                  tileMatches=tileMatches.Append(Tiles[i]).ToArray();
               }

               break;
            case 4:
               // then find matches west of the tile
               if (Tiles[i].CornerColorSW==colorSW && Tiles[i].CornerColorNW==colorNW){
                  tileMatches=tileMatches.Append(Tiles[i]).ToArray();
               }
               
               break;
         }
      }

    return tileMatches;
    }
 }   
}