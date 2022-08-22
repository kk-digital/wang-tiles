
namespace wang
{
    public struct PixelColor
    {
        int Color;


        public static PixelColor MakePixelColor(int r, int g, int b, int a)
        {
            PixelColor result = new PixelColor();
            result.Color = Utils.ToRGBA8((char)r, (char)g, (char)b, (char)a);
            return result;
        }

        public static PixelColor MakePixelColor(int r, int g, int b)
        {
            return MakePixelColor(r, g, b, 255);
        }
    }
}