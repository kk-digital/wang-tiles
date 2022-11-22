// using System.Diagnostics;
// using System.Drawing;
// using Wang.EdgeTile;
// using Wang;

// namespace Wang.SceneW
// {


//     // TODO(Mahdi): https://github.com/kk-digital/kcg/issues/567
//     public partial class Scene
//     {


//         public void SavePNG(string filename)
//         {
//             // TODO(Mahdi): Implement

//             ColorPaleteMap colorPaleteMap = new ColorPaleteMap();
//             colorPaleteMap.Initialize();

//             int tileSizeInPixels = 16;

//             int tileCount = SizeX * SizeY;
//             int numberOfRows = SizeX;


//             var builder = BigGustave.PngBuilder.Create(tileSizeInPixels * SizeX, tileSizeInPixels * SizeY, true);

            
            

//             for(int tileIndex = 0; tileIndex < SceneTiles[(int)Layer.LayerFront].Length; tileIndex++)
//             {
//                 SceneTile sceneTile = SceneTiles[(int)Layer.LayerFront][tileIndex];

//                 if (sceneTile.TileSetID != -1 && sceneTile.TileID != -1)
//                 {
//                     EdgeTileInformation tile = TileSets[sceneTile.TileSetID].InformationArray[sceneTile.TileID];

//                     int xPosition = tileIndex % SizeX;
//                     int yPosition = tileIndex / SizeX;

//                     int xPixelPosition = xPosition * tileSizeInPixels;
//                     int yPixelPosition = yPosition * tileSizeInPixels;

//                     DrawTileBorder(builder, xPixelPosition, yPixelPosition, tileSizeInPixels);
//                     DrawHorizontalEdge(builder, colorPaleteMap, tile.BottomColor, xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + tileSizeInPixels - 3, tileSizeInPixels);
//                     DrawVerticalEdge(builder, colorPaleteMap, tile.RightColor, xPixelPosition + tileSizeInPixels - 3, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
//                     DrawHorizontalEdge(builder, colorPaleteMap, tile.TopColor, xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + 1, tileSizeInPixels);
//                     DrawVerticalEdge(builder, colorPaleteMap, tile.LeftColor, xPixelPosition + 1, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
//                 }
//                 else
//                 {
//                     int xPosition = tileIndex % SizeX;
//                     int yPosition = tileIndex / SizeX;

//                     int xPixelPosition = xPosition * tileSizeInPixels;
//                     int yPixelPosition = yPosition * tileSizeInPixels;

//                     Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, tileSizeInPixels, tileSizeInPixels, PixelColor.White);
//                 }
//             }

//             using (FileStream fs = File.OpenWrite(Constants.OutputPath + "/" + filename))
//             {
//                 builder.Save(fs);
//             }


//         }


        
//         public void DrawTileBorder(BigGustave.PngBuilder builder, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
//         {
//             Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, 1, tileSizeInPixels, PixelColor.Gray);
//             Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, tileSizeInPixels, 1, PixelColor.Gray);
//             Other.Utils.FillRectangle(builder, xPixelPosition + tileSizeInPixels - 1, yPixelPosition, 1, tileSizeInPixels, PixelColor.Gray);
//             Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition + tileSizeInPixels - 1, tileSizeInPixels, 1, PixelColor.Gray);
//         }

//         public void DrawVerticalEdge(BigGustave.PngBuilder builder, ColorPaleteMap colorPaleteMap, 
//         int colorIndex, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
//         {
//             PixelColor colorPixel = colorPaleteMap.GetVerticalColor(colorIndex);
//             Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, 2, 4, 
//             PixelColor.MakePixelColor(colorPixel.Red(), colorPixel.Green(), colorPixel.Blue(), colorPixel.Alpha()));
//         }

//         public void DrawHorizontalEdge(BigGustave.PngBuilder builder, ColorPaleteMap colorPaleteMap, 
//         int colorIndex, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
//         {
//             PixelColor colorPixel = colorPaleteMap.GetHorizontalColor(colorIndex);
//             Other.Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, 4, 2, 
//             PixelColor.MakePixelColor(colorPixel.Red(), colorPixel.Green(), colorPixel.Blue(), colorPixel.Alpha()));
//         }
//     }
// }