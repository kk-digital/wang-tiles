namespace wang
{
    public class ColorPaleteMap
    {
        public PixelColor[] HorizontalEdgeColors;
        public PixelColor[] VerticalEdgeColors;

        public PixelColor GetHorizontalColor(int index)
        {
            return HorizontalEdgeColors[index];
        }

        public PixelColor GetVerticalColor(int index)
        {
            return VerticalEdgeColors[index];
        }
    }
}