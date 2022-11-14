namespace Wang
{
    public static class Utils
    {
        public static int ToRGBA8(char r, char g, char b, char a)
        {
            return (r << 24) + (g << 16) + (b << 8) + a;
        }
        public static void FillRectangle(BigGustave.PngBuilder builder, int x, int y, int w, int h, PixelColor color)
        {
            for(int j = y; j < y + h; j++)
            {
                for(int i = x; i < x + w; i++)
                {
                    builder.SetPixel((byte)color.Red(), (byte)color.Green(), (byte)color.Blue(), i, j);
                }
            }
        }
    }
}