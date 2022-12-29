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
                    // For grid lines
                    SKPaint thinLinePaint = new SKPaint
                    {
                        Style = SKPaintStyle.StrokeAndFill,
                        Color = SKColors.Black,
                        StrokeWidth = 3
                    };

                    //get the drawing canvas of the surface
                    var canvas = tempSurface.Canvas;

                    //set background color
                    canvas.Clear(SKColors.Transparent);

                    //go through each image and draw it on the final image
                    int row = 0;
                    int col = 0;

                    int verticalGridLinesDrawn = 0;
                    foreach (SKBitmap image in images)
                    {
                        canvas.DrawBitmap(image, SKRect.Create(row, col, image.Width, image.Height));

                        // update col and row
                        if (row>=((width-1)*image.Width)){
                            col+=(int)(image.Height);
                            row=0;

                            // Draw horizontal grid line
                            SKPoint p1 = new SKPoint(0, col);
                            SKPoint p2 = new SKPoint(width*image.Width,col);
                            canvas.DrawLine(p1, p2, thinLinePaint);
                        } else {
                            row+=(int)(image.Width);

                            if (col==((height-1)*image.Height) && verticalGridLinesDrawn<width){
                                // Draw vertical grid line
                                SKPoint p1 = new SKPoint(row, 0);
                                SKPoint p2 = new SKPoint(row,height*image.Height);
                                canvas.DrawLine(p1, p2, thinLinePaint);
                                verticalGridLinesDrawn++;

                                Console.WriteLine($"p2={row},{height*image.Height}");
                            }
                        }
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
}