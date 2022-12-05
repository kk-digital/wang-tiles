namespace WangTile
{   
    public class ColorMap
    {
        public PixelColor[] CornerPixelColors;
        public PixelColor[] VerticalPixelColors;
        public PixelColor[] HorizontalPixelColors;
        public int CornerColorCount=0;
        public int VerticalColorCount=0;
        public int HorizontalColorCount=0;


        public int GetCornerColorCount()
        {
            return this.CornerColorCount;
        }

        public int GetVerticalColorCount()
        {
            return this.VerticalColorCount;
        }

        public int GetHorizontalColorCount()
        {
            return this.HorizontalColorCount;
        }

        public void IncrementCornerColorCount()
        {
            this.CornerColorCount++;
        }

        public void IncrementVerticalColorCount()
        {
            this.VerticalColorCount++;
        }

        public void IncrementHorizontalColorCount()
        {
            this.HorizontalColorCount++;
        }


        public PixelColor GetCornerPixelColor(int index)
        {
            return CornerPixelColors[index];
        }

        public PixelColor GetVerticalPixelColor(int index)
        {
            return VerticalPixelColors[index];
        }

        public PixelColor GetHorizontalPixelColor(int index)
        {
            return HorizontalPixelColors[index];
        }

        public ColorMap()
        {
            CornerPixelColors = new PixelColor[32];
            VerticalPixelColors = new PixelColor[32];
            HorizontalPixelColors = new PixelColor[32];

            // Red - 0-10 index
            // Blue - 11-19 index
            // Green - 20-31
            CornerPixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[1] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[2] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[3] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[4] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[5] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[6] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[7] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[8] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[9] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[10] = PixelColor.MakePixelColor(255,0,0, 255);
            CornerPixelColors[11] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[12] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[14] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[15] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[16] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[17] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[18] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[19] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[20] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[21] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[22] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[23] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[24] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[25] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[26] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[27] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[28] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[29] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[30] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[31] = PixelColor.MakePixelColor(0,205,0, 255);

            // same vertical colors until 8th index
            // VerticalPixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[1] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[2] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[3] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[4] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[5] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[6] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[7] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[8] = PixelColor.MakePixelColor(255,0,0, 255);
            // VerticalPixelColors[9] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[10] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[11] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[12] =PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[14] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[15] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[16] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[17] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[18] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[19] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[20] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[21] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[22] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[23] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[24] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[25] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[26] = PixelColor.MakePixelColor(0,0,128, 255);
            // VerticalPixelColors[27] = PixelColor.MakePixelColor(255,140,0, 255);
            // VerticalPixelColors[28] = PixelColor.MakePixelColor(255,228,196, 255);
            // VerticalPixelColors[29] = PixelColor.MakePixelColor(0,0,205, 255);
            // VerticalPixelColors[30] = PixelColor.MakePixelColor(255,20,147, 255);
            // VerticalPixelColors[31] = PixelColor.MakePixelColor(0,0,255, 250);

            // same horizontal colors until 5th index
            // HorizontalPixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
            // HorizontalPixelColors[1] = PixelColor.MakePixelColor(255,0,0, 255);
            // HorizontalPixelColors[2] = PixelColor.MakePixelColor(255,0,0, 255);
            // HorizontalPixelColors[3] = PixelColor.MakePixelColor(255,0,0, 255);
            // HorizontalPixelColors[4] = PixelColor.MakePixelColor(255,0,0, 255);
            // HorizontalPixelColors[5] =  PixelColor.MakePixelColor(255,0,0, 255);
            // HorizontalPixelColors[6] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[7] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[8] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[9] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[10] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[11] =  PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[12] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[14] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[15] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[16] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[17] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[18] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[19] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[20] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[21] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[22] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[23] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[24] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[25] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[26] = PixelColor.MakePixelColor(0,0,128, 255);
            // HorizontalPixelColors[27] = PixelColor.MakePixelColor(255,140,0, 255);
            // HorizontalPixelColors[28] = PixelColor.MakePixelColor(255,228,196, 255);
            // HorizontalPixelColors[29] = PixelColor.MakePixelColor(0,0,205, 255);
            // HorizontalPixelColors[30] = PixelColor.MakePixelColor(255,20,147, 255);
            // HorizontalPixelColors[31] = PixelColor.MakePixelColor(0,0,255, 250);


            // different corner colors in all indexes
            // CornerPixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
            // CornerPixelColors[1] = PixelColor.MakePixelColor(0,255,0, 255);
            // CornerPixelColors[2] = PixelColor.MakePixelColor(128,128,0, 255);
            // CornerPixelColors[3] = PixelColor.MakePixelColor(255,255,0, 255);
            // CornerPixelColors[4] = PixelColor.MakePixelColor(0,255,255, 255);
            // CornerPixelColors[5] = PixelColor.MakePixelColor(255,0,255, 255);
            // CornerPixelColors[6] = PixelColor.MakePixelColor(138,43,226, 255);
            // CornerPixelColors[7] = PixelColor.MakePixelColor(0,128,0, 255);
            // CornerPixelColors[8] = PixelColor.MakePixelColor(128,0,128, 255);
            // CornerPixelColors[9] = PixelColor.MakePixelColor(0,128,128, 255);
            // CornerPixelColors[10] = PixelColor.MakePixelColor(0,0,128, 255);
            // CornerPixelColors[11] = PixelColor.MakePixelColor(255,140,0, 255);
            // CornerPixelColors[12] = PixelColor.MakePixelColor(255,228,196, 255);
            // CornerPixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            // CornerPixelColors[14] = PixelColor.MakePixelColor(255,20,147, 255);
            // CornerPixelColors[15] = PixelColor.MakePixelColor(0,0,255, 250);
            // CornerPixelColors[16] = PixelColor.MakePixelColor(255,0,0, 255);
            // CornerPixelColors[17] = PixelColor.MakePixelColor(0,255,0, 255);
            // CornerPixelColors[18] = PixelColor.MakePixelColor(128,128,0, 255);
            // CornerPixelColors[19] = PixelColor.MakePixelColor(255,255,0, 255);
            // CornerPixelColors[20] = PixelColor.MakePixelColor(0,255,255, 255);
            // CornerPixelColors[21] = PixelColor.MakePixelColor(255,0,255, 255);
            // CornerPixelColors[22] = PixelColor.MakePixelColor(138,43,226, 255);
            // CornerPixelColors[23] = PixelColor.MakePixelColor(0,128,0, 255);
            // CornerPixelColors[24] = PixelColor.MakePixelColor(128,0,128, 255);
            // CornerPixelColors[25] = PixelColor.MakePixelColor(0,128,128, 255);
            // CornerPixelColors[26] = PixelColor.MakePixelColor(0,0,128, 255);
            // CornerPixelColors[27] = PixelColor.MakePixelColor(255,140,0, 255);
            // CornerPixelColors[28] = PixelColor.MakePixelColor(255,228,196, 255);
            // CornerPixelColors[29] = PixelColor.MakePixelColor(0,0,205, 255);
            // CornerPixelColors[30] = PixelColor.MakePixelColor(255,20,147, 255);
            // CornerPixelColors[31] = PixelColor.MakePixelColor(0,0,255, 250);

            // different vertical colors in all indexes
            VerticalPixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
            VerticalPixelColors[1] = PixelColor.MakePixelColor(0,255,0, 255);
            VerticalPixelColors[2] = PixelColor.MakePixelColor(128,128,0, 255);
            VerticalPixelColors[3] = PixelColor.MakePixelColor(255,255,0, 255);
            VerticalPixelColors[4] = PixelColor.MakePixelColor(0,255,255, 255);
            VerticalPixelColors[5] = PixelColor.MakePixelColor(255,0,255, 255);
            VerticalPixelColors[6] = PixelColor.MakePixelColor(138,43,226, 255);
            VerticalPixelColors[7] = PixelColor.MakePixelColor(0,128,0, 255);
            VerticalPixelColors[8] = PixelColor.MakePixelColor(128,0,128, 255);
            VerticalPixelColors[9] = PixelColor.MakePixelColor(0,128,128, 255);
            VerticalPixelColors[10] = PixelColor.MakePixelColor(0,0,128, 255);
            VerticalPixelColors[11] = PixelColor.MakePixelColor(255,140,0, 255);
            VerticalPixelColors[12] = PixelColor.MakePixelColor(255,228,196, 255);
            VerticalPixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            VerticalPixelColors[14] = PixelColor.MakePixelColor(255,20,147, 255);
            VerticalPixelColors[15] = PixelColor.MakePixelColor(0,0,255, 250);
            VerticalPixelColors[16] = PixelColor.MakePixelColor(255,0,0, 255);
            VerticalPixelColors[17] = PixelColor.MakePixelColor(0,255,0, 255);
            VerticalPixelColors[18] = PixelColor.MakePixelColor(128,128,0, 255);
            VerticalPixelColors[19] = PixelColor.MakePixelColor(255,255,0, 255);
            VerticalPixelColors[20] = PixelColor.MakePixelColor(0,255,255, 255);
            VerticalPixelColors[21] = PixelColor.MakePixelColor(255,0,255, 255);
            VerticalPixelColors[22] = PixelColor.MakePixelColor(138,43,226, 255);
            VerticalPixelColors[23] = PixelColor.MakePixelColor(0,128,0, 255);
            VerticalPixelColors[24] = PixelColor.MakePixelColor(128,0,128, 255);
            VerticalPixelColors[25] = PixelColor.MakePixelColor(0,128,128, 255);
            VerticalPixelColors[26] = PixelColor.MakePixelColor(0,0,128, 255);
            VerticalPixelColors[27] = PixelColor.MakePixelColor(255,140,0, 255);
            VerticalPixelColors[28] = PixelColor.MakePixelColor(255,228,196, 255);
            VerticalPixelColors[29] = PixelColor.MakePixelColor(0,0,205, 255);
            VerticalPixelColors[30] = PixelColor.MakePixelColor(255,20,147, 255);
            VerticalPixelColors[31] = PixelColor.MakePixelColor(0,0,255, 250);

            // different horizontal colors in all indexes
            HorizontalPixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
            HorizontalPixelColors[1] = PixelColor.MakePixelColor(0,255,0, 255);
            HorizontalPixelColors[2] = PixelColor.MakePixelColor(128,128,0, 255);
            HorizontalPixelColors[3] = PixelColor.MakePixelColor(255,255,0, 255);
            HorizontalPixelColors[4] = PixelColor.MakePixelColor(0,255,255, 255);
            HorizontalPixelColors[5] = PixelColor.MakePixelColor(255,0,255, 255);
            HorizontalPixelColors[6] = PixelColor.MakePixelColor(138,43,226, 255);
            HorizontalPixelColors[7] = PixelColor.MakePixelColor(0,128,0, 255);
            HorizontalPixelColors[8] = PixelColor.MakePixelColor(128,0,128, 255);
            HorizontalPixelColors[9] = PixelColor.MakePixelColor(0,128,128, 255);
            HorizontalPixelColors[10] = PixelColor.MakePixelColor(0,0,128, 255);
            HorizontalPixelColors[11] = PixelColor.MakePixelColor(255,140,0, 255);
            HorizontalPixelColors[12] = PixelColor.MakePixelColor(255,228,196, 255);
            HorizontalPixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            HorizontalPixelColors[14] = PixelColor.MakePixelColor(255,20,147, 255);
            HorizontalPixelColors[15] = PixelColor.MakePixelColor(0,0,255, 250);
            HorizontalPixelColors[16] = PixelColor.MakePixelColor(255,0,0, 255);
            HorizontalPixelColors[17] = PixelColor.MakePixelColor(0,255,0, 255);
            HorizontalPixelColors[18] = PixelColor.MakePixelColor(128,128,0, 255);
            HorizontalPixelColors[19] = PixelColor.MakePixelColor(255,255,0, 255);
            HorizontalPixelColors[20] = PixelColor.MakePixelColor(0,255,255, 255);
            HorizontalPixelColors[21] = PixelColor.MakePixelColor(255,0,255, 255);
            HorizontalPixelColors[22] = PixelColor.MakePixelColor(138,43,226, 255);
            HorizontalPixelColors[23] = PixelColor.MakePixelColor(0,128,0, 255);
            HorizontalPixelColors[24] = PixelColor.MakePixelColor(128,0,128, 255);
            HorizontalPixelColors[25] = PixelColor.MakePixelColor(0,128,128, 255);
            HorizontalPixelColors[26] = PixelColor.MakePixelColor(0,0,128, 255);
            HorizontalPixelColors[27] = PixelColor.MakePixelColor(255,140,0, 255);
            HorizontalPixelColors[28] = PixelColor.MakePixelColor(255,228,196, 255);
            HorizontalPixelColors[29] = PixelColor.MakePixelColor(0,0,205, 255);
            HorizontalPixelColors[30] = PixelColor.MakePixelColor(255,20,147, 255);
            HorizontalPixelColors[31] = PixelColor.MakePixelColor(0,0,255, 250);
        }
    }
}