using System.Linq;

namespace CornerWangTile
{

 public class WangCornerTileSet
 {
    /* Properties of WangCornerTileSet */
    public WangCornerTile[]? Tiles;

    /* Constructor for WangCornerTile */
    public WangCornerTileSet()
    {
    }

    /* Methods of WangCornerTileSet */
    public WangCornerTile CreateTile(TileGeometry geometry, Color colorNW, Color colorNE, Color colorSE, Color colorSW)
    {
      WangCornerTile newTile = new WangCornerTile(geometry,colorNW,colorNE,colorSE,colorSW); 

      if (this.Tiles==null){
         this.Tiles=new WangCornerTile[1];
         newTile.TileID=0;
         this.Tiles[0]=newTile;
      } else {
         newTile.TileID=this.Tiles.Length;
         this.Tiles=this.Tiles.Append(newTile).ToArray();
      }

      //  Update ColorCountMap
      // Globals.ColorCountMap[colorNW]=Globals.ColorCountMap[colorNW]++;
      // Globals.ColorCountMap[colorNE]=Globals.ColorCountMap[colorNE]++;
      // Globals.ColorCountMap[colorSE]=Globals.ColorCountMap[colorSE]++;
      // Globals.ColorCountMap[colorSW]=Globals.ColorCountMap[colorSW]++;

      return newTile;
    }

    public int[] ReturnMatches(WangCornerTile[] Tiles,Color colorNW, Color colorNE, Color colorSE, Color colorSW)
    {
      int[]? tileMatches=null;
   
      for (int i=0; i<Tiles.Length;i++){
         if ((colorNW==Color.MatchAll || colorNW==Tiles[i].CornerColorNW) && (colorNE==Color.MatchAll || colorNE==Tiles[i].CornerColorNE)
         && (colorSE==Color.MatchAll || colorSE==Tiles[i].CornerColorSE)&& (colorSW==Color.MatchAll || colorSW==Tiles[i].CornerColorSW)){
             if (tileMatches==null){
               tileMatches = new int[1];
               tileMatches[0] = i;
            }else {
               tileMatches=tileMatches.Append(i).ToArray();
            }

         }

      }
        
    return tileMatches;
    }
 }   
}