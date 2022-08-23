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




        public TileSpriteInformation[] TileSpriteInformationArray;
        public UniquePixel[] UniquePixels;
    

        

        public void InitEdgeTileSet(int verticalColorCount, int horizontalColorCount)
        {
            // TODO(Mahdi): Implement

            // Create and Init Variables
            InformationArray = new TileInformation[16];
            HorizontalColorsCount = horizontalColorCount;
            VerticalColorsCount = verticalColorCount;
            Description = new TileSetDescription();
            Description.ID = Utils.GenerateID();
            Description.IDString = "" + Description.ID;
        }


        public void AddTile(TileType tileType, int topColor, int bottomColor, int leftColor, int rightColor)
        {
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
        }

        public void FinalizeTileSet()
        {
            // TODO(Mahdi): Implement

            TileLookUpArray = new int[HorizontalColorsCount * VerticalColorsCount];

            for(int i = 0; i < InformationArray.Length - 1; i++)
            {
                for(int j = i + 1; j < InformationArray.Length; j++)
                {
                    if (InformationArray[i].VarientIndex(HorizontalColorsCount, VerticalColorsCount) >
                        InformationArray[j].VarientIndex(HorizontalColorsCount, VerticalColorsCount))
                    {
                        TileInformation temp = InformationArray[i];
                        InformationArray[i] = InformationArray[j];
                        InformationArray[j] = temp;
                    }
                }
            }

            Dictionary<Int64, Entry> HashMap = new Dictionary<Int64, Entry>();

            int CurrentVarientIndex = 0;
            int CurrentOffset = 0;
            int CurrentCount = 0;

            for(int k = 0; k < InformationArray.Length; k++) {
                if(InformationArray[k].VarientIndex(HorizontalColorsCount, VerticalColorsCount) == 
                CurrentVarientIndex) {
                    CurrentCount++;
                }
                else {
                    HashMap.Add(CurrentVarientIndex, new Entry(CurrentOffset, CurrentCount));

                    CurrentVarientIndex = InformationArray[k].VarientIndex(HorizontalColorsCount, VerticalColorsCount);
                    CurrentCount = 1;
                    CurrentOffset = k;
                }
            }

            if(CurrentCount > 1){
                HashMap.Add(CurrentVarientIndex, new Entry(CurrentOffset, CurrentCount));
            }

        }

        public int ColorScan(int leftColor, int rightColor, int bottomColor, int topColor)
        {
            for (int k = 0; k < InformationArray.Length; k++)
            {
                if(InformationArray[k].RightColor == InformationArray[k].LeftColor ||
                    InformationArray[k].TopColor == InformationArray[k].BottomColor) {
                    return k;
                }
            }
            return 0;
        }
    }
}