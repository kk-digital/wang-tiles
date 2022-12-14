namespace WangTile
{
    class Picture
    {

        public void SavePNG(Board board,ColorMap colorMap,string filename)
        {
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

                DrawTile(builder, board.TileSet[tileSetID], colorMap, tile,xPixelPosition, yPixelPosition, tileSizeInPixels);
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

        public void DrawTile(BigGustave.PngBuilder builder,WangTileSet tileSet,ColorMap colorMap, WangTile tile,int xPixelPosition,int yPixelPosition, int tileSizeInPixels)
        {
            // DrawTileBorder(builder, PixelColor.Gray, xPixelPosition, yPixelPosition, tileSizeInPixels);
            PixelColor colorPixel = colorMap.GetCornerPixelColor(tileSet.GetCornerColorPalette(tile.CornerColorNW));
            DrawTileBorder(builder, colorPixel, xPixelPosition, yPixelPosition, tileSizeInPixels);


            // NorthWest
            DrawCornerColor(builder, colorMap, tileSet.GetCornerColorPalette(tile.CornerColorNW), xPixelPosition + 1 , yPixelPosition+1, tileSizeInPixels);
            // NorthEast
            DrawCornerColor(builder, colorMap, tileSet.GetCornerColorPalette(tile.CornerColorNE), xPixelPosition + tileSizeInPixels-3 , yPixelPosition+1, tileSizeInPixels);
            // SouthEast
            DrawCornerColor(builder, colorMap, tileSet.GetCornerColorPalette(tile.CornerColorSE), xPixelPosition + tileSizeInPixels-3 , yPixelPosition+tileSizeInPixels-3, tileSizeInPixels);
            // SouthWest 
            DrawCornerColor(builder, colorMap, tileSet.GetCornerColorPalette(tile.CornerColorSW), xPixelPosition + 1 , yPixelPosition+tileSizeInPixels-3, tileSizeInPixels);

            // North
            DrawVerticalEdgeColor(builder, colorMap, tileSet.GetVerticalColorPalette(tile.EdgeColorNorth), xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + 1, tileSizeInPixels);
            // East
            DrawHorizontalEdgeColor(builder, colorMap, tileSet.GetHorizontalColorPalette(tile.EdgeColorEast), xPixelPosition + tileSizeInPixels - 2, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
            // South
            DrawVerticalEdgeColor(builder, colorMap, tileSet.GetVerticalColorPalette(tile.EdgeColorSouth), xPixelPosition + tileSizeInPixels / 2 - 2, yPixelPosition + tileSizeInPixels - 2, tileSizeInPixels);
            // West
            DrawHorizontalEdgeColor(builder, colorMap, tileSet.GetHorizontalColorPalette(tile.EdgeColorWest), xPixelPosition + 1, yPixelPosition + tileSizeInPixels / 2 - 2, tileSizeInPixels);
        

            DrawTileBitMask_Gray(builder, tile, xPixelPosition,yPixelPosition,tileSizeInPixels);
        }

        public void DrawTileBorder(BigGustave.PngBuilder builder,
        PixelColor pixelColor, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            // Left border
            FillRectangle(builder, xPixelPosition, yPixelPosition, 1, tileSizeInPixels, pixelColor);
            // Up border
            FillRectangle(builder, xPixelPosition, yPixelPosition, tileSizeInPixels, 1, pixelColor);
            // Right border
            FillRectangle(builder, xPixelPosition + tileSizeInPixels - 1, yPixelPosition, 1, tileSizeInPixels, pixelColor);
            // Down border
            FillRectangle(builder, xPixelPosition, yPixelPosition + tileSizeInPixels - 1, tileSizeInPixels, 1, pixelColor);
        }

        public void DrawTileBitMask_Gray(BigGustave.PngBuilder builder, WangTile tile, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {   
            if ((tile.TileBitMask&(1<<(int)BitMask.N_2S))==(1<<(int)BitMask.N_2S)){
                // North
                FillRectangle(builder, xPixelPosition + tileSizeInPixels / 2 - 1, yPixelPosition, 2, 1, PixelColor.Gray);
            }
           
            if ((tile.TileBitMask&(1<<(int)BitMask.E_4W))==(1<<(int)BitMask.E_4W)){
                // East
                FillRectangle(builder, xPixelPosition + tileSizeInPixels - 1, yPixelPosition + tileSizeInPixels / 2 - 1, 1, 2, PixelColor.Gray);
            }

            if ((tile.TileBitMask&(1<<(int)BitMask.S_6N))==(1<<(int)BitMask.S_6N)){
                // South
                FillRectangle(builder, xPixelPosition + tileSizeInPixels / 2 - 1, yPixelPosition + tileSizeInPixels - 1, 2, 1, PixelColor.Gray);
            }

            if ((tile.TileBitMask&(1<<(int)BitMask.W_8E))==(1<<(int)BitMask.W_8E)){
                // West
                FillRectangle(builder,xPixelPosition , yPixelPosition + tileSizeInPixels / 2 - 1, 1, 2, PixelColor.Gray);
            }
        }

        public void DrawCornerColor(BigGustave.PngBuilder builder, ColorMap colorMap, 
        int color, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorMap.GetCornerPixelColor(color);
            FillRectangle(builder, xPixelPosition, yPixelPosition, 2, 2, colorPixel);
        }

        public void DrawVerticalEdgeColor(BigGustave.PngBuilder builder, ColorMap colorMap, 
        int color, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorMap.GetVerticalPixelColor((int)color);
            FillRectangle(builder, xPixelPosition, yPixelPosition, 4, 1, colorPixel);
        }

        public void DrawHorizontalEdgeColor(BigGustave.PngBuilder builder, ColorMap colorMap, 
        int color, int xPixelPosition, int yPixelPosition, int tileSizeInPixels)
        {
            PixelColor colorPixel = colorMap.GetHorizontalPixelColor((int)color);
            FillRectangle(builder, xPixelPosition, yPixelPosition, 1, 4, colorPixel);

        }
    }
}