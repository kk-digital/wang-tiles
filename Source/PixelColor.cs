using Wang.Other;

namespace Wang
{
    public struct PixelColor
    {
        public int Color;

        public int Red()
        {
            return (Color >> 24) & 0xff;
        }

        public int Green()
        {
            return (Color >> 16) & 0xff;
        }

        public int Blue()
        {
            return (Color >> 8) & 0xff;
        }

        public int Alpha()
        {
            return Color & 0xff;
        }

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



        public static PixelColor Gray = MakePixelColor(128, 128, 128, 255);
        public static PixelColor White = MakePixelColor(255, 255, 255, 255);

    }
}