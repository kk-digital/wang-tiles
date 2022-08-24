
using System.Security.Cryptography;

namespace Wang
{
    public class ColorPaleteMap
    {
        public PixelColor[] HorizontalEdgeColors;
        public PixelColor[] VerticalEdgeColors;

        public PixelColor GetHorizontalColor(int index)
        {
            if (index <= 0)
                throw new ArgumentOutOfRangeException("Out Of Range");

            return HorizontalEdgeColors[index];
        }

        public PixelColor GetVerticalColor(int index)
        {
            if (index <= 0)
                throw new ArgumentOutOfRangeException("Out Of Range");

            return VerticalEdgeColors[index];
        }

        public void Initialize()
        {
            Random random = new Random();

            HorizontalEdgeColors = new PixelColor[16];
            VerticalEdgeColors = new PixelColor[16];

            HorizontalEdgeColors[0] = PixelColor.MakePixelColor(0, 255, 100, 255);
            HorizontalEdgeColors[1] = PixelColor.MakePixelColor(255, 255, 100, 255);
            HorizontalEdgeColors[2] = PixelColor.MakePixelColor(255, 0, 0, 255);
            HorizontalEdgeColors[3] = PixelColor.MakePixelColor(255, 50, 0, 255);
            HorizontalEdgeColors[4] = PixelColor.MakePixelColor(45, 10, 250, 255);
            HorizontalEdgeColors[5] = PixelColor.MakePixelColor(0, 0, 50, 255);
            HorizontalEdgeColors[6] = PixelColor.MakePixelColor(0, 255, 100, 255);
            HorizontalEdgeColors[7] = PixelColor.MakePixelColor(0, 20, 0, 255);
            HorizontalEdgeColors[8] = PixelColor.MakePixelColor(0, 10, 10, 255);
            HorizontalEdgeColors[9] = PixelColor.MakePixelColor(50, 150, 10, 255);
            HorizontalEdgeColors[10] = PixelColor.MakePixelColor(10, 0, 50, 255);
            HorizontalEdgeColors[11] = PixelColor.MakePixelColor(0, 64, 40, 255);
            HorizontalEdgeColors[12] = PixelColor.MakePixelColor(80, 32, 10, 255);
            HorizontalEdgeColors[13] = PixelColor.MakePixelColor(20, 229, 98, 255);
            HorizontalEdgeColors[14] = PixelColor.MakePixelColor(135, 148, 192, 255);
            HorizontalEdgeColors[15] = PixelColor.MakePixelColor(0, 89, 100, 250);

            VerticalEdgeColors[0] = PixelColor.MakePixelColor(0, 255, 100, 255);
            VerticalEdgeColors[1] = PixelColor.MakePixelColor(255, 255, 100, 255);
            VerticalEdgeColors[2] = PixelColor.MakePixelColor(255, 0, 0, 255);
            VerticalEdgeColors[3] = PixelColor.MakePixelColor(255, 50, 0, 255);
            VerticalEdgeColors[4] = PixelColor.MakePixelColor(45, 10, 250, 255);
            VerticalEdgeColors[5] = PixelColor.MakePixelColor(0, 0, 50, 255);
            VerticalEdgeColors[6] = PixelColor.MakePixelColor(0, 255, 100, 255);
            VerticalEdgeColors[7] = PixelColor.MakePixelColor(0, 20, 0, 255);
            VerticalEdgeColors[8] = PixelColor.MakePixelColor(0, 10, 10, 255);
            VerticalEdgeColors[9] = PixelColor.MakePixelColor(50, 150, 10, 255);
            VerticalEdgeColors[10] = PixelColor.MakePixelColor(10, 0, 50, 255);
            VerticalEdgeColors[11] = PixelColor.MakePixelColor(0, 64, 40, 255);
            VerticalEdgeColors[12] = PixelColor.MakePixelColor(80, 32, 10, 255);
            VerticalEdgeColors[13] = PixelColor.MakePixelColor(20, 229, 98, 255);
            VerticalEdgeColors[14] = PixelColor.MakePixelColor(135, 148, 192, 255);
            VerticalEdgeColors[15] = PixelColor.MakePixelColor(0, 89, 100, 250);
        }
    }
}