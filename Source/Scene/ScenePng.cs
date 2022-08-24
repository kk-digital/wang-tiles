using System.Diagnostics;
using System.Drawing;
using Wang.EdgeTile;
using Wang;

namespace Wang.SceneW
{


    // TODO(Mahdi): https://github.com/kk-digital/kcg/issues/567
    public partial class Scene
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

            int tileCount = SizeX * SizeY;
            int numberOfRows = SizeX;


            Bitmap bmp = new Bitmap(tileSizeInPixels * tilesPerRow, tileSizeInPixels * numberOfRows);
            Graphics g = Graphics.FromImage(bmp);

            
            

            for(int tileIndex = 0; tileIndex < SceneTiles[(int)Layer.LayerFront].Length; tileIndex++)
            {
                SceneTile sceneTile = SceneTiles[(int)Layer.LayerFront][tileIndex];
                EdgeTileInformation tile = TileSets[sceneTile.TileSetID].InformationArray[sceneTile.TileID];

                int xPosition = tileIndex % tilesPerRow;
                int yPosition = tileIndex / tilesPerRow;

                int xPixelPosition = xPosition * tileSizeInPixels;
                int yPixelPosition = (numberOfRows - yPosition - 1) * tileSizeInPixels;

                DrawTileBorder(g, xPixelPosition, yPixelPosition, tileSizeInPixels);
                DrawHorizontalEdge(g, colorPaleteMap, tile.BottomColor, xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + tileSizeInPixels - 3, tileSizeInPixels);
                DrawVerticalEdge(g, colorPaleteMap, tile.RightColor, xPixelPosition + tileSizeInPixels - 3, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
                DrawHorizontalEdge(g, colorPaleteMap, tile.TopColor, xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + 1, tileSizeInPixels);
                DrawVerticalEdge(g, colorPaleteMap, tile.LeftColor, xPixelPosition + 1, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
            }

            g.Dispose();
            bmp.Save(Constants.OutputPath + "\\" + filename, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();


        }


        public void DrawTileBorder(Graphics g, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            g.FillRectangle(Brushes.Gray, xPixelPosition, yPixelPosition,
             1, tileSizeInPixels);

             g.FillRectangle(Brushes.Gray, xPixelPosition, yPixelPosition,
             tileSizeInPixels, 1);

             g.FillRectangle(Brushes.Gray, xPixelPosition + tileSizeInPixels - 1, yPixelPosition,
             1, tileSizeInPixels);

             g.FillRectangle(Brushes.Gray, xPixelPosition, yPixelPosition + tileSizeInPixels - 1,
             tileSizeInPixels, 1);
        }

        public void DrawVerticalEdge(Graphics g, ColorPaleteMap colorPaleteMap, 
        int colorIndex, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorPaleteMap.GetVerticalColor(colorIndex);
            SolidBrush brush = new SolidBrush(Color.FromArgb(colorPixel.Alpha(), colorPixel.Red(), colorPixel.Green(), colorPixel.Blue()));
            g.FillRectangle(brush, xPixelPosition, yPixelPosition, 2, 4);
        }

        public void DrawHorizontalEdge(Graphics g, ColorPaleteMap colorPaleteMap, 
        int colorIndex, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorPaleteMap.GetHorizontalColor(colorIndex);
            SolidBrush brush = new SolidBrush(Color.FromArgb(colorPixel.Alpha(), colorPixel.Red(), colorPixel.Green(), colorPixel.Blue()));
            g.FillRectangle(brush, xPixelPosition, yPixelPosition, 4, 2);
        }
    }
}