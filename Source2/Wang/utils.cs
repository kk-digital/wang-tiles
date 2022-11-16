namespace Wang
{
    public static class Utils
    {
        public static int ToRGBA8(char r, char g, char b, char a)
        {
            return (r << 24) + (g << 16) + (b << 8) + a;
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

        public static (int col,int row) GetRandomPosition(int width, int height)
        {
            Random rand = new Random();
            int randX = rand.Next(0,width);
            int randY = rand.Next(0,height);

            return (col:randX, row:randY);
        }

        public static int GetBoardSlotIndex(int Width, int col, int row)
        {
            return (Width*col)+row;
        }
    }
}