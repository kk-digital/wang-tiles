using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Wang.Other;

namespace Wang.EdgeTile
{
    public struct Entry
    {
        public Entry(int lVarientIndex, int lCount)
        {
            VarientIndex = lVarientIndex;
            Count = lCount;
        }
        public int VarientIndex;
        public int Count;
    };


    // TODO(Mahdi): https://github.com/kk-digital/kcg/issues/562
    public partial class EdgeTileSet
    {
        public EdgeTileSetDescription Description;
        [JsonConverter(typeof(StringEnumConverter))]
        public TileSize TileSize;
        public int VerticalColorsCount;
        public int HorizontalColorsCount;
        public EdgeTileInformation[] InformationArray;
        Dictionary<Int64, Entry> LookupMap;



       /* public TileSpriteInformation[] TileSpriteInformationArray;
        public UniquePixel[] UniquePixels;*/


        public int TileCount = 0;

        

        public void InitEdgeTileSet(TileSize tileSize, int verticalColorCount, int horizontalColorCount)
        {
            // TODO(Mahdi): Implement

            // Create and Init Variables
            TileSize = tileSize;
            InformationArray = new EdgeTileInformation[16];
            HorizontalColorsCount = horizontalColorCount;
            VerticalColorsCount = verticalColorCount;
            Description = new EdgeTileSetDescription();
            Description.ID = Utils.GenerateID();
            Description.IDString = "" + Description.ID;

            DateTimeOffset dto = DateTimeOffset.Now;

            Description.CreationDate = dto.ToString();
            Description.CreationDateUnixTime = (UInt64)dto.ToUnixTimeSeconds();

            TileCount = 0;

        }


        public void AddTile(TileType tileType, int topColor, int bottomColor, int leftColor, int rightColor)
        {
            // TODO(Mahdi): Implement

            if (TileCount == InformationArray.Length)
            {
                System.Array.Resize(ref InformationArray, InformationArray.Length * 2);
            }

            // Initialize Information Array
            EdgeTileInformation properties = new EdgeTileInformation
            {
                TileID = -1,
                TopColor = topColor,
                BottomColor = bottomColor,
                LeftColor = leftColor,
                RightColor = rightColor
            };
            InformationArray[TileCount++] = properties;
        }

        public void FinalizeTileSet()
        {
            // TODO(Mahdi): Implement


            EdgeTileInformation[] NewInformationArray = new EdgeTileInformation[TileCount];
            for(int i = 0; i < InformationArray.Length; i++)
            {
                NewInformationArray[i] = InformationArray[i];
            }

            InformationArray = NewInformationArray;



            for(int i = 0; i < InformationArray.Length - 1; i++)
            {
                for(int j = i + 1; j < InformationArray.Length; j++)
                {
                    if (InformationArray[i].VarientIndex(HorizontalColorsCount, VerticalColorsCount) >
                        InformationArray[j].VarientIndex(HorizontalColorsCount, VerticalColorsCount))
                    {
                        EdgeTileInformation temp = InformationArray[i];
                        InformationArray[i] = InformationArray[j];
                        InformationArray[j] = temp;
                    }
                }
            }

            LookupMap = new Dictionary<Int64, Entry>();
            int CurrentVarientIndex = 0;
            int CurrentOffset = 0;
            int CurrentCount = 0;

            for(int k = 0; k < InformationArray.Length; k++) {
                InformationArray[k].TileID = k;
                if(InformationArray[k].VarientIndex(HorizontalColorsCount, VerticalColorsCount) == 
                CurrentVarientIndex) {
                    CurrentCount++;
                }
                else {
                    LookupMap.Add(CurrentVarientIndex, new Entry(CurrentOffset, CurrentCount));

                    CurrentVarientIndex = InformationArray[k].VarientIndex(HorizontalColorsCount, VerticalColorsCount);
                    CurrentCount = 1;
                    CurrentOffset = k;
                }
            }

            if(CurrentCount > 1){
                LookupMap.Add(CurrentVarientIndex, new Entry(CurrentOffset, CurrentCount));
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

        public static EdgeTileSet NewWangCompleteTileset (TileSize tileSize, int verticalColorCount, int horizontalColorCount, int variant) 
        {
            EdgeTileSet tileSet = new EdgeTileSet();
            int nbOfTiles = variant * verticalColorCount * verticalColorCount * horizontalColorCount * horizontalColorCount;
            tileSet.InitEdgeTileSet(tileSize, verticalColorCount, horizontalColorCount);

            for(int i = 0; i < variant; i++)
            {
                for(int j = 0; j < verticalColorCount; j++)
                {
                    for(int k = 0; k < verticalColorCount; k++)
                    {
                        for(int x = 0; x < horizontalColorCount; x++)
                        {
                            for(int y = 0; y < horizontalColorCount; y++)
                            {
                                tileSet.AddTile(TileType.TileTypeWang, k, j, y, x);
                            }
                        }
                    }
                }
            }

            tileSet.FinalizeTileSet();

            return tileSet;
        }

        public static EdgeTileSet NewWangRandomTileSet(TileSize tileSize, int horizontalTilesCount, int verticalTilesCount, int numberOfTiles) 
        {
            EdgeTileSet tileSet = new EdgeTileSet();

            tileSet.InitEdgeTileSet(tileSize, verticalTilesCount, horizontalTilesCount);

            for(int i = 0; i < numberOfTiles; i++) {

                ulong topColor = Mt19937.genrand_int32() % (ulong)verticalTilesCount;
                ulong bottomColor = Mt19937.genrand_int32() % (ulong)verticalTilesCount;
                ulong rightColor = Mt19937.genrand_int32() % (ulong)horizontalTilesCount;
                ulong leftColor = Mt19937.genrand_int32() % (ulong)horizontalTilesCount;    

                tileSet.AddTile(TileType.TileTypeWang, (int)topColor, (int)bottomColor, (int)leftColor, 
                    (int)rightColor);
            }
        
            tileSet.FinalizeTileSet();

            return tileSet;
        }
    }
}