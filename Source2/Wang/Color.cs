namespace Wang
{
    public enum Color
    {
        // MatchAll means match anything
        MatchAll = 0,
        Air, // Color 1 is air, space, or other
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,

    }

     public struct PixelColor
    {
        public int Color;
        public static PixelColor Gray = MakePixelColor(128, 128, 128, 255);

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

    }

    public class ColorMap
    {
        public PixelColor[] PixelColors;

        public PixelColor GetPixelColor(int index)
        {
            return PixelColors[index];
        }

        public ColorMap()
        {
            PixelColors = new PixelColor[16];

            PixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[1] = PixelColor.MakePixelColor(0,255,0, 255);
            PixelColors[2] = PixelColor.MakePixelColor(128,128,0, 255);
            PixelColors[3] = PixelColor.MakePixelColor(255,255,0, 255);
            PixelColors[4] = PixelColor.MakePixelColor(0,255,255, 255);
            PixelColors[5] = PixelColor.MakePixelColor(255,0,255, 255);
            PixelColors[6] = PixelColor.MakePixelColor(138,43,226, 255);
            PixelColors[7] = PixelColor.MakePixelColor(0,128,0, 255);
            PixelColors[8] = PixelColor.MakePixelColor(128,0,128, 255);
            PixelColors[9] = PixelColor.MakePixelColor(0,128,128, 255);
            PixelColors[10] = PixelColor.MakePixelColor(0,0,128, 255);
            PixelColors[11] = PixelColor.MakePixelColor(255,140,0, 255);
            PixelColors[12] = PixelColor.MakePixelColor(255,228,196, 255);
            PixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[14] = PixelColor.MakePixelColor(255,20,147, 255);
            PixelColors[15] = PixelColor.MakePixelColor(0,0,255, 250);
        }
    }

}