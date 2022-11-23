namespace WangTile
{
    class Picture
    {

        public void SavePNG(Board board,string filename)
        {
            ColorMap colorMap = new ColorMap();
            int tileSizeInPixels = WangConstants.TileSizeInPixels;
            int numberOfColumns = board.Height;
            int numberOfRows = board.Width;
            
            var builder = BigGustave.PngBuilder.Create(tileSizeInPixels * numberOfRows, tileSizeInPixels * numberOfColumns, true);

            for(int tileIndex = 0; tileIndex < board.TileSlots.Length; tileIndex++)
            {   
                if (board.TileSlots[tileIndex].TileID==null){
                    continue;
                }

                int xPosition = tileIndex % numberOfRows;
                int yPosition = tileIndex / numberOfRows;

                int xPixelPosition = xPosition * tileSizeInPixels;
                int yPixelPosition = yPosition * tileSizeInPixels;

                // get tileset
                int tileSetID=(int)board.TileSlots[tileIndex].TileSetID;
                int tileID=(int)board.TileSlots[tileIndex].TileID;
                WangTile tile = board.TileSet[tileSetID].Tiles[tileID];

                DrawTile(builder, colorMap, tile,xPixelPosition, yPixelPosition, tileSizeInPixels);
            }

            using (FileStream fs = File.OpenWrite(@"././data" + "/" + filename))
            {
                builder.Save(fs);
            }

        }

        public static void FillRectangle(BigGustave.PngBuilder builder, int col, int row, int w, int h, PixelColor color)
        {
            for(int j = row; j < row + h; j++)
            {
                for(int i = col; i < col + w; i++)
                {
                    builder.SetPixel((byte)color.Red(), (byte)color.Green(), (byte)color.Blue(), i, j);
                }
            }
        }

        public void DrawTile(BigGustave.PngBuilder builder,ColorMap colorMap, WangTile tile,int xPixelPosition,int yPixelPosition, int tileSizeInPixels)
        {
            DrawTileBorder(builder, xPixelPosition, yPixelPosition, tileSizeInPixels);

            // NorthWest
            DrawCornerColor(builder, colorMap, tile.CornerColorNW, xPixelPosition + 1 , yPixelPosition+1, tileSizeInPixels);
            // NorthEast
            DrawCornerColor(builder, colorMap, tile.CornerColorNE, xPixelPosition + tileSizeInPixels-3 , yPixelPosition+1, tileSizeInPixels);
            // SouthEast
            DrawCornerColor(builder, colorMap, tile.CornerColorSE, xPixelPosition + tileSizeInPixels-3 , yPixelPosition+tileSizeInPixels-3, tileSizeInPixels);
            // SouthWest 
            DrawCornerColor(builder, colorMap, tile.CornerColorSW, xPixelPosition + 1 , yPixelPosition+tileSizeInPixels-3, tileSizeInPixels);

            // North
            DrawHorizontalEdgeColor(builder, colorMap, tile.EdgeColorNorth, xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + 1, tileSizeInPixels);
            // East
            DrawVerticalEdgeColor(builder, colorMap, tile.EdgeColorEast, xPixelPosition + tileSizeInPixels - 3, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
            // South
            DrawHorizontalEdgeColor(builder, colorMap, tile.EdgeColorSouth, xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + tileSizeInPixels - 3, tileSizeInPixels);
            // West
            DrawVerticalEdgeColor(builder, colorMap, tile.EdgeColorWest, xPixelPosition + 1, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
        }

        public void DrawTileBorder(BigGustave.PngBuilder builder, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            // Left border
            FillRectangle(builder, xPixelPosition, yPixelPosition, 1, tileSizeInPixels, PixelColor.Gray);
            // Up border
            FillRectangle(builder, xPixelPosition, yPixelPosition, tileSizeInPixels, 1, PixelColor.Gray);
            // Right border
            FillRectangle(builder, xPixelPosition + tileSizeInPixels - 1, yPixelPosition, 1, tileSizeInPixels, PixelColor.Gray);
            // Down border
            FillRectangle(builder, xPixelPosition, yPixelPosition + tileSizeInPixels - 1, tileSizeInPixels, 1, PixelColor.Gray);
        }

        public void DrawCornerColor(BigGustave.PngBuilder builder, ColorMap colorMap, 
        int color, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorMap.GetPixelColor((int)color);
            FillRectangle(builder, xPixelPosition, yPixelPosition, 2, 2, colorPixel);
        }

        public void DrawVerticalEdgeColor(BigGustave.PngBuilder builder, ColorMap colorMap, 
        int color, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorMap.GetPixelColor((int)color);
            FillRectangle(builder, xPixelPosition, yPixelPosition, 2, 4, colorPixel);
        }

        public void DrawHorizontalEdgeColor(BigGustave.PngBuilder builder, ColorMap colorMap, 
        int color, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorMap.GetPixelColor((int)color);
            FillRectangle(builder, xPixelPosition, yPixelPosition, 4, 2, colorPixel);

        }
    }
}