using System.Collections.Generic;
using System.IO;
using SkiaSharp;

namespace WangTile{
    public static class SkiaSharpImageGenerator
    {
        public static void GenerateMapAndSave(Dictionary<int,SKImage> tileImageMap, BoardTileSlot[] tileSlots, int width, int height, string output){
            SKImage image = GenerateMapImage(tileImageMap, tileSlots, width, height);
            
            //save the new image
            using (SKData encoded = image.Encode(SKEncodedImageFormat.Png, 100))
            using (Stream outFile = File.OpenWrite(output))
            {
                encoded.SaveTo(outFile);
            }
            
        }

        public static SKImage CreateTileImage(Dictionary<int, SKImage> imageMap, int[] tileData)
        {
            SKImage finalImage = null;

            int imgWidth = SkiaSharpImage.TileWidth;
            int imgHeight = SkiaSharpImage.TileHeight;

            //get a surface so we can draw an image
            using (var tempSurface = SKSurface.Create(new SKImageInfo(imgWidth*SkiaSharpImage.TilePixelWidth, imgHeight*SkiaSharpImage.TilePixelWidth)))
            {
                //get the drawing canvas of the surface
                var canvas = tempSurface.Canvas;

                //set background color
                canvas.Clear(SKColors.Transparent);

                //go through each image and draw it on the final image
                int row = 0;
                int col = 0;

                foreach (int tileID in tileData)
                {
                    SKImage tileImage = imageMap[tileID];
                    SKBitmap image = SKBitmap.FromImage(tileImage);
              
                    canvas.DrawBitmap(image, SKRect.Create(row, col, image.Width, image.Height));

                    // update col and row
                    if (row>=((imgWidth-1)*image.Width)){
                        col+=(int)(image.Height);
                        row=0;
                    } else {
                        row+=(int)(image.Width);
                    }
                }

                // return the surface as a manageable image
                finalImage = tempSurface.Snapshot();
            }

            //return the image that was just drawn
            return finalImage;
        }

        public static SKImage GenerateMapImage(Dictionary<int,SKImage> tileImageMap, BoardTileSlot[] tileSlots, int mapWidth, int mapHeight)
        {
            SKImage finalImage = null;

            int tileImageWidth = SkiaSharpImage.TileWidth * SkiaSharpImage.TilePixelWidth;
            int tileImageHeight = SkiaSharpImage.TileHeight * SkiaSharpImage.TilePixelHeight;
            int mapImageWidth = mapWidth * tileImageWidth;
            int mapImageHeight =  mapHeight * tileImageHeight;
            

            // get a surface so we can draw an image
            using (var tempSurface = SKSurface.Create(new SKImageInfo(mapImageWidth, mapImageHeight)))
            {
                // get the drawing canvas of the surface
                var canvas = tempSurface.Canvas;

                // set background color
                canvas.Clear(SKColors.Transparent);

                // go through each image and draw it on the final image
                int row = 0;
                int col = 0;

                foreach (BoardTileSlot tileSlot in tileSlots)
                {   
                    int? tileID = tileSlot.TileID;
                    if (tileID != null){
                        SKImage tileImage = tileImageMap[(int)tileID];
                        SKBitmap tileBitmap = SKBitmap.FromImage(tileImage);
                        canvas.DrawBitmap(tileBitmap, SKRect.Create(row, col, tileImageWidth, tileImageHeight));
                    }

                    // update col and row
                    if (row>=((mapWidth-1)*tileImageWidth)){
                        col+=tileImageHeight;
                        row=0;
                    } else {
                        row+=(int)(tileImageWidth);
                    }
                }

                // For grid lines
                SKPaint thinLinePaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.Black,
                    StrokeWidth = 6,
                    PathEffect = SKPathEffect.CreateDash(new float[2]{10f,10f}, 20)
                };
                
                // Draw vertical grids
                for (int i=0;i<mapWidth;i++){
                    SKPoint p1 = new SKPoint(i*tileImageWidth, 0);
                    SKPoint p2 = new SKPoint(i*tileImageWidth,mapHeight*tileImageHeight);
                    canvas.DrawLine(p1, p2, thinLinePaint);
                }

                // Draw horizontal grids
                for (int i=0;i<mapHeight;i++){
                    SKPoint p1 = new SKPoint(0, i*tileImageHeight);
                    SKPoint p2 = new SKPoint(mapWidth*tileImageWidth,i*tileImageHeight);
                    canvas.DrawLine(p1, p2, thinLinePaint);
                }
                
                // return the surface as a manageable image
                finalImage = tempSurface.Snapshot();
            }

            //return the image that was just drawn
            return finalImage;
        }
    }

    public static class SkiaSharpImage {
        public static int TileWidth = 16;
        public static int TileHeight = 16;

        public static int TilePixelWidth = 32;
        public static int TilePixelHeight = 32;

        public static void TestGetTile(){
            string imgSrc = @"kcg-tiled/tilesets/Colors_Vertical.png";
            int tileIndex = 413;
            SKImage tileImg = GetTileImageFromTileSetImage(imgSrc, tileIndex);

            //save the new image
            using (SKData encoded = tileImg.Encode(SKEncodedImageFormat.Png, 100))
            using (Stream outFile = File.OpenWrite("data/testGetTile.png"))
            {
                encoded.SaveTo(outFile);
            }
        }

        public static SKImage GetTileImageFromTileSetImage(string imageSource, int tileIndex){
            SKImage finalImage = null;

            var image = SKImage.FromEncodedData(imageSource);
            var bm = SKBitmap.FromImage(image);

            (int col,int row) pos= GetTilePosition(bm.Width, tileIndex, TilePixelWidth, TilePixelHeight);
            (int col, int row) secondPos=(pos.col+TilePixelHeight, pos.row+TilePixelWidth);

            //get a surface so we can draw an image
            using (var tempSurface = SKSurface.Create(new SKImageInfo(TilePixelWidth, TilePixelHeight)))
            {
               //get the drawing canvas of the surface
                var canvas = tempSurface.Canvas;

                //set background color
                canvas.Clear(SKColors.Transparent);

            
                SKRect dest = new SKRect(0, 0, TilePixelWidth, TilePixelHeight);
                SKRect source = new SKRect(pos.row,pos.col,
                                       secondPos.row,secondPos.col);

                canvas.DrawBitmap(bm, source, dest);
                finalImage = tempSurface.Snapshot();
            }

           return finalImage;
        }

        public static SKImage CreateBlankImage(){
            SKImage finalImage = null;

            //get a surface so we can draw an image
            using (var tempSurface = SKSurface.Create(new SKImageInfo(TilePixelWidth, TilePixelHeight)))
            {
               //get the drawing canvas of the surface
                var canvas = tempSurface.Canvas;

                //set background color
                canvas.Clear(SKColors.Transparent);

                finalImage = tempSurface.Snapshot();
            }

           return finalImage;
        }


        static (int col, int row) GetTilePosition(int imageWidth, int index, int tileWidth, int tileHeight){
            int Col=0;
            int Row=0;

            // Make sure imageWidth%tileWidth is zero.
            int imageModTileWidth = imageWidth%tileWidth;
            if (imageModTileWidth > 0){
                imageWidth= imageWidth-imageModTileWidth;
            }

            int indexPos=index*tileWidth;
            int indexCol=(indexPos/imageWidth);
            int indexRow= indexPos-(imageWidth*indexCol);

            for (int i=0; i<indexCol;i++){
                Col+=tileHeight;
            }

            for (int i=0; i< indexRow/tileWidth;i++){
                Row+=tileWidth;
            }

            return (col:Col,row:Row);
        }
    }
}