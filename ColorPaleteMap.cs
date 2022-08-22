
using System.Security.Cryptography;

namespace wang
{
    public class ColorPaleteMap
    {
        public PixelColor[] HorizontalEdgeColors;
        public PixelColor[] VerticalEdgeColors;

        public PixelColor GetHorizontalColor(int index)
        {
            if(index <= 0)
                throw new ArgumentOutOfRangeException("Out Of Range");

            return HorizontalEdgeColors[index];
        }

        public PixelColor GetVerticalColor(int index)
        {
            if(index <= 0)
                throw new ArgumentOutOfRangeException("Out Of Range");

            return VerticalEdgeColors[index];
        }

        public void Initialize()
        {
            Random random = new Random();

            HorizontalEdgeColors = new PixelColor[16];
            VerticalEdgeColors = new PixelColor[16];

            for(int i = 0; i < HorizontalEdgeColors.Length; i++)
            {
                HorizontalEdgeColors[i] = PixelColor.MakePixelColor(random.Next(0,255), random.Next(0,255), random.Next(0,255), 255);
            }

            for(int i = 0; i < VerticalEdgeColors.Length; i++)
            {
                VerticalEdgeColors[i] = PixelColor.MakePixelColor(random.Next(0,255), random.Next(0,255), random.Next(0,255), 255);
            }
        }
    }
}