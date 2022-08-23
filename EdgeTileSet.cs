using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using wang_tiles;
using System.Linq;

namespace wang_tiles
{


    // TODO(Mahdi): https://github.com/kk-digital/kcg/issues/562
    public partial class EdgeTileSet
    {
        public TileSetDescription Description;
        public TileSize TileSize;
        public int VerticalColorsCount;
        public int HorizontalColorsCount;
        public TileInformation[] InformationArray;
        public int TileIndex;
        public int[] TileLookUpArray;

        public UniquePixel[] UniquePixels;
    

        

        public void InitEdgeTileSet()
        {
            // TODO(Mahdi): Implement

            // Create and Init Variables
            InformationArray = new TileInformation[16];
            VerticalColorsCount = 0;
            HorizontalColorsCount = 0;
            Description = new TileSetDescription();
            Description.ID = Utils.GenerateID();
            Description.IDString = "" + Description.ID;
        }


        public void AddTile(TileType tileType, int topColor, int bottomColor, int leftColor, int rightColor)
        {
            Debug.Assert(topColor >= -1 && topColor < HorizontalColorsCount);
            Debug.Assert(bottomColor >= -1 && bottomColor < HorizontalColorsCount);
            Debug.Assert(rightColor >= -1 && rightColor < VerticalColorsCount);
            Debug.Assert(leftColor >= -1 && leftColor < VerticalColorsCount);

            VerticalColorsCount++;
            HorizontalColorsCount++;
            
            // TODO(Mahdi): Implement

            // Initialize Information Array
            TileInformation properties = new TileInformation
            {
                TileID = -1,
                TopColor = topColor,
                BottomColor = bottomColor,
                LeftColor = leftColor,
                RightColor = rightColor
            };
            InformationArray.Append(properties);

            TileLookUpArray = new int[HorizontalColorsCount * VerticalColorsCount];
            

            TileIndex = leftColor * (HorizontalColorsCount * VerticalColorsCount) + rightColor * (VerticalColorsCount * VerticalColorsCount) +
                            bottomColor * VerticalColorsCount + topColor;

            TileLookUpArray.Append(TileIndex);

        }

        public void FinalizeTileSet()
        {
            // TODO(Mahdi): Implement


        }
    }
}