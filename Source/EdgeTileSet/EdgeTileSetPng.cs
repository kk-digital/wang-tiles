using System.Diagnostics;
using System.Drawing;

namespace Wang.EdgeTile
{


    // TODO(Mahdi): https://github.com/kk-digital/kcg/issues/567
    public partial class EdgeTileSet
    {


        public void SavePNG(string filename, int tilesPerRow = 8)
        {
            // TODO(Mahdi): Implement

            ColorPaleteMap colorPaleteMap = new ColorPaleteMap();
            colorPaleteMap.Initialize();

            int tileSizeInPixels = 16;

            if (TileSize == TileSize.TileSize_8x8)
            {
                tileSizeInPixels = 8;
            }
            else if (TileSize == TileSize.TileSize_16x16)
            {
                tileSizeInPixels = 16;
            }
            else if (TileSize == TileSize.TileSize_32x32)
            {
                tileSizeInPixels = 32;
            }

            int numberOfRows = (TileCount / tilesPerRow);
            if (((float)TileCount / (float)tilesPerRow) - numberOfRows > 0)
            {
                numberOfRows++;
            }

            
            var builder = BigGustave.PngBuilder.Create(tileSizeInPixels * tilesPerRow, tileSizeInPixels * numberOfRows, true);


            for(int tileIndex = 0; tileIndex < InformationArray.Length; tileIndex++)
            {
                EdgeTileInformation tile = InformationArray[tileIndex];
                int xPosition = tileIndex % tilesPerRow;
                int yPosition = tileIndex / tilesPerRow;

                int xPixelPosition = xPosition * tileSizeInPixels;
                int yPixelPosition = yPosition * tileSizeInPixels;

                DrawTileBorder(builder, xPixelPosition, yPixelPosition, tileSizeInPixels);
                DrawHorizontalEdge(builder, colorPaleteMap, tile.BottomColor, xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + tileSizeInPixels - 3, tileSizeInPixels);
                DrawVerticalEdge(builder, colorPaleteMap, tile.RightColor, xPixelPosition + tileSizeInPixels - 3, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
                DrawHorizontalEdge(builder, colorPaleteMap, tile.TopColor, xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + 1, tileSizeInPixels);
                DrawVerticalEdge(builder, colorPaleteMap, tile.LeftColor, xPixelPosition + 1, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
            }

            using (FileStream fs = File.OpenWrite(Constants.OutputPath + "/" + filename))
            {
                builder.Save(fs);
            }


        }


        public void DrawTileBorder(BigGustave.PngBuilder builder, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, 1, tileSizeInPixels, PixelColor.Gray);
            Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, tileSizeInPixels, 1, PixelColor.Gray);
            Other.Utils.FillRectangle(builder, xPixelPosition + tileSizeInPixels - 1, yPixelPosition, 1, tileSizeInPixels, PixelColor.Gray);
            Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition + tileSizeInPixels - 1, tileSizeInPixels, 1, PixelColor.Gray);
        }

        public void DrawVerticalEdge(BigGustave.PngBuilder builder, ColorPaleteMap colorPaleteMap, 
        int colorIndex, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorPaleteMap.GetVerticalColor(colorIndex);
            Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, 2, 4, 
            PixelColor.MakePixelColor(colorPixel.Red(), colorPixel.Green(), colorPixel.Blue(), colorPixel.Alpha()));
        }

        public void DrawHorizontalEdge(BigGustave.PngBuilder builder, ColorPaleteMap colorPaleteMap, 
        int colorIndex, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorPaleteMap.GetHorizontalColor(colorIndex);
            Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, 4, 2, 
            PixelColor.MakePixelColor(colorPixel.Red(), colorPixel.Green(), colorPixel.Blue(), colorPixel.Alpha()));
        }
    }
}