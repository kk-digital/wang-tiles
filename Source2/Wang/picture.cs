namespace Wang
{
    class Picture
    {

        public void SavePNG(Board board,string filename)
        {
            ColorMap colorMap = new ColorMap();
            int tileSizeInPixels = 16;
            int numberOfRows = board.Height;
            int tilesPerRow =board.Width;
            
            var builder = BigGustave.PngBuilder.Create(tileSizeInPixels * tilesPerRow, tileSizeInPixels * numberOfRows, true);


            for(int tileIndex = 0; tileIndex < 2; tileIndex++)
            {
                int xPosition = tileIndex % tilesPerRow;
                int yPosition = tileIndex / tilesPerRow;

                int xPixelPosition = xPosition * tileSizeInPixels;
                int yPixelPosition = yPosition * tileSizeInPixels;

                DrawTile(builder, colorMap, board.TileSlots[tileIndex].Tile,xPixelPosition, yPixelPosition, tileSizeInPixels);
            }

            using (FileStream fs = File.OpenWrite(@"././data" + "/" + filename))
            {
                builder.Save(fs);
            }

        }

        public void DrawTile(BigGustave.PngBuilder builder,ColorMap colorMap, WangCornerTile tile,int xPixelPosition,int yPixelPosition, int tileSizeInPixels)
        {
            DrawTileBorder(builder, xPixelPosition, yPixelPosition, tileSizeInPixels);

            // NorthWest
            DrawCornerEdge(builder, colorMap, tile.CornerColorNW, xPixelPosition + 1 , yPixelPosition+1, tileSizeInPixels);
            // NorthEast
            DrawCornerEdge(builder, colorMap, tile.CornerColorNE, xPixelPosition + tileSizeInPixels-3 , yPixelPosition+1, tileSizeInPixels);
            // SouthEast
            DrawCornerEdge(builder, colorMap, tile.CornerColorSE, xPixelPosition + tileSizeInPixels-3 , yPixelPosition+tileSizeInPixels-3, tileSizeInPixels);
            // SouthWest 
            DrawCornerEdge(builder, colorMap, tile.CornerColorSW, xPixelPosition + 1 , yPixelPosition+tileSizeInPixels-3, tileSizeInPixels);
        }
        public void DrawTileBorder(BigGustave.PngBuilder builder, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            // Left border
            Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, 1, tileSizeInPixels, PixelColor.Gray);
            // Up border
            Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, tileSizeInPixels, 1, PixelColor.Gray);
            // Right border
            Utils.FillRectangle(builder, xPixelPosition + tileSizeInPixels - 1, yPixelPosition, 1, tileSizeInPixels, PixelColor.Gray);
            // Down border
            Utils.FillRectangle(builder, xPixelPosition, yPixelPosition + tileSizeInPixels - 1, tileSizeInPixels, 1, PixelColor.Gray);
        }

        public void DrawCornerEdge(BigGustave.PngBuilder builder, ColorMap colorMap, 
        Color color, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorMap.GetPixelColor((int)color);
            Utils.FillRectangle(builder, xPixelPosition, yPixelPosition, 2, 2, colorPixel);
        }
    }
}