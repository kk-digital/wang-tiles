using System.Collections.Generic;
using System.IO;

using SkiaSharp;

namespace WangTile{

    public static class SkiaSharpImageMerger
    {
        public static void GenerateMapUsingGivenPictures(string[] imgDir, int width, int height, string output){
            SKImage image = Combine(imgDir, width, height);
            
            //save the new image
            using (SKData encoded = image.Encode(SKEncodedImageFormat.Png, 100))
            using (Stream outFile = File.OpenWrite(output))
            {
                encoded.SaveTo(outFile);
            }
            
        }

        public static void TestGenerateMapUsingGivenPictures(){
            // Get array of string dir in order from tileSlot ID
            string[] imgDir = new string[2];
            imgDir[0]="./data/1.png";
            imgDir[1]="./data/2.png";
            SKImage image = Combine(imgDir, 1, 2);
            
            //save the new image
            using (SKData encoded = image.Encode(SKEncodedImageFormat.Png, 100))
            using (Stream outFile = File.OpenWrite("data/testCombine.png"))
            {
                encoded.SaveTo(outFile);
            }
        }
    
        public static SKImage Combine(string[] files, int width, int height)
        {
            //read all images into memory
            List<SKBitmap> images = new List<SKBitmap>();
            SKImage finalImage = null;

            try
            {
                int imgWidth = 0;
                int imgHeight = 0;

                foreach (string image in files)
				{
                    //create a bitmap from the file and add it to the list
                    SKBitmap bitmap = SKBitmap.Decode(image);

                    if (imgWidth==0) {
                        //update the size of the final bitmap
                        imgWidth = bitmap.Width * width;
                        imgHeight = bitmap.Height * height;
                    }

                    images.Add(bitmap);
                }

                

                //get a surface so we can draw an image
                using (var tempSurface = SKSurface.Create(new SKImageInfo(imgWidth, imgHeight)))
                {
                    //get the drawing canvas of the surface
                    var canvas = tempSurface.Canvas;

                    //set background color
                    canvas.Clear(SKColors.Transparent);

                    //go through each image and draw it on the final image
                    int row = 0;
                    int col = 0;

                    foreach (SKBitmap image in images)
                    {
                        canvas.DrawBitmap(image, SKRect.Create(row, col, image.Width, image.Height));

                        // update col and row
                        if (row>=((width-1)*image.Width)){
                            col+=(int)(image.Height);
                            row=0;
                        } else {
                            row+=(int)(image.Width);
                        }
                    }

                    // For grid lines
                    SKPaint thinLinePaint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = SKColors.Black,
                        StrokeWidth = 3,
                        PathEffect = SKPathEffect.CreateDash(new float[2]{10f,10f}, 20)
                    };
                    
                    // Draw vertical grids
                    for (int i=0;i<width;i++){
                        SKPoint p1 = new SKPoint(i*images[0].Width, 0);
                        SKPoint p2 = new SKPoint(i*images[0].Width,height*images[0].Height);
                        canvas.DrawLine(p1, p2, thinLinePaint);
                    }

                    // Draw hhorizontal grids
                    for (int i=0;i<height;i++){
                        SKPoint p1 = new SKPoint(0, i*images[0].Height);
                        SKPoint p2 = new SKPoint(width*images[0].Width,i*images[0].Height);
                        canvas.DrawLine(p1, p2, thinLinePaint);
                    }
                    
                    // return the surface as a manageable image
                    finalImage = tempSurface.Snapshot();
                }

                //return the image that was just drawn
                return finalImage;
            }
            finally
            {
                //clean up memory
                // foreach (SKBitmap image in images)
                // {
                //     image.Dispose();
                // }
            }
        }
    }

    public static class SkiaSharpImage {
        static int tileWidth = 32;
        static int tileHeight = 32;

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

            (int col,int row) pos= GetTilePosition(bm.Width, tileIndex, tileWidth, tileHeight);
            (int col, int row) secondPos=(pos.col+tileHeight, pos.row+tileWidth);

            //get a surface so we can draw an image
            using (var tempSurface = SKSurface.Create(new SKImageInfo(tileWidth, tileHeight)))
            {
               //get the drawing canvas of the surface
                var canvas = tempSurface.Canvas;

                //set background color
                canvas.Clear(SKColors.Transparent);

            
                SKRect dest = new SKRect(0, 0, tileWidth, tileHeight);
                SKRect source = new SKRect(pos.row,pos.col,
                                       secondPos.row,secondPos.col);

                canvas.DrawBitmap(bm, source, dest);
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