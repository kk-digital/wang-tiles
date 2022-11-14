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

            PixelColors[0] = PixelColor.MakePixelColor(0, 255, 100, 255);
            PixelColors[1] = PixelColor.MakePixelColor(255, 255, 100, 255);
            PixelColors[2] = PixelColor.MakePixelColor(255, 0, 0, 255);
            PixelColors[3] = PixelColor.MakePixelColor(255, 50, 0, 255);
            PixelColors[4] = PixelColor.MakePixelColor(45, 10, 250, 255);
            PixelColors[5] = PixelColor.MakePixelColor(0, 0, 50, 255);
            PixelColors[6] = PixelColor.MakePixelColor(0, 255, 100, 255);
            PixelColors[7] = PixelColor.MakePixelColor(0, 20, 0, 255);
            PixelColors[8] = PixelColor.MakePixelColor(0, 10, 10, 255);
            PixelColors[9] = PixelColor.MakePixelColor(50, 150, 10, 255);
            PixelColors[10] = PixelColor.MakePixelColor(10, 0, 50, 255);
            PixelColors[11] = PixelColor.MakePixelColor(0, 64, 40, 255);
            PixelColors[12] = PixelColor.MakePixelColor(80, 32, 10, 255);
            PixelColors[13] = PixelColor.MakePixelColor(20, 229, 98, 255);
            PixelColors[14] = PixelColor.MakePixelColor(135, 148, 192, 255);
            PixelColors[15] = PixelColor.MakePixelColor(0, 89, 100, 250);
        }
    }

}