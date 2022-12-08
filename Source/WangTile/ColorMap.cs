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
            CornerPixelColors = new PixelColor[50];
            VerticalPixelColors = new PixelColor[50];
            HorizontalPixelColors = new PixelColor[50];

            // Red - 0-9 index
            // Blue - 10-18 index
            // Green - 19-28 index
            // Brown - 29-38 index
            // Light yellow - 39-48
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
            CornerPixelColors[10] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[11] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[12] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[14] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[15] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[16] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[17] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[18] = PixelColor.MakePixelColor(0,0,205, 255);
            CornerPixelColors[19] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[20] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[21] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[22] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[23] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[24] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[25] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[26] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[27] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[28] = PixelColor.MakePixelColor(0,205,0, 255);
            CornerPixelColors[29] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[30] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[31] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[32] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[33] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[34] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[35] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[36] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[37] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[38] = PixelColor.MakePixelColor(92,64,51, 255);
            CornerPixelColors[39] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[40] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[41] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[42] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[43] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[44] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[45] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[46] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[47] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[48] = PixelColor.MakePixelColor(255,250,205,255);
            CornerPixelColors[49] = PixelColor.MakePixelColor(255,250,205,255);


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
            VerticalPixelColors[32] = PixelColor.MakePixelColor(128,128,0, 255);
            VerticalPixelColors[33] = PixelColor.MakePixelColor(255,255,0, 255);
            VerticalPixelColors[34] = PixelColor.MakePixelColor(0,255,255, 255);
            VerticalPixelColors[35] = PixelColor.MakePixelColor(255,0,255, 255);
            VerticalPixelColors[36] = PixelColor.MakePixelColor(138,43,226, 255);
            VerticalPixelColors[37] = PixelColor.MakePixelColor(0,128,0, 255);
            VerticalPixelColors[38] = PixelColor.MakePixelColor(128,0,128, 255);
            VerticalPixelColors[39] = PixelColor.MakePixelColor(0,128,128, 255);
            VerticalPixelColors[40] = PixelColor.MakePixelColor(0,128,128, 255);
            VerticalPixelColors[41] = PixelColor.MakePixelColor(255,0,0, 255);
            VerticalPixelColors[42] = PixelColor.MakePixelColor(0,255,0, 255);
            VerticalPixelColors[43] = PixelColor.MakePixelColor(128,128,0, 255);
            VerticalPixelColors[44] = PixelColor.MakePixelColor(255,255,0, 255);
            VerticalPixelColors[45] = PixelColor.MakePixelColor(0,255,255, 255);
            VerticalPixelColors[46] = PixelColor.MakePixelColor(255,0,255, 255);
            VerticalPixelColors[47] = PixelColor.MakePixelColor(138,43,226, 255);
            VerticalPixelColors[48] = PixelColor.MakePixelColor(0,128,0, 255);
            VerticalPixelColors[49] = PixelColor.MakePixelColor(128,0,128, 255);

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
            HorizontalPixelColors[32] = PixelColor.MakePixelColor(128,128,0, 255);
            HorizontalPixelColors[33] = PixelColor.MakePixelColor(255,255,0, 255);
            HorizontalPixelColors[34] = PixelColor.MakePixelColor(0,255,255, 255);
            HorizontalPixelColors[35] = PixelColor.MakePixelColor(255,0,255, 255);
            HorizontalPixelColors[36] = PixelColor.MakePixelColor(138,43,226, 255);
            HorizontalPixelColors[37] = PixelColor.MakePixelColor(0,128,0, 255);
            HorizontalPixelColors[38] = PixelColor.MakePixelColor(128,0,128, 255);
            HorizontalPixelColors[39] = PixelColor.MakePixelColor(0,128,128, 255);
            HorizontalPixelColors[40] = PixelColor.MakePixelColor(0,128,128, 255);
            HorizontalPixelColors[41] = PixelColor.MakePixelColor(255,0,0, 255);
            HorizontalPixelColors[42] = PixelColor.MakePixelColor(0,255,0, 255);
            HorizontalPixelColors[43] = PixelColor.MakePixelColor(128,128,0, 255);
            HorizontalPixelColors[44] = PixelColor.MakePixelColor(255,255,0, 255);
            HorizontalPixelColors[45] = PixelColor.MakePixelColor(0,255,255, 255);
            HorizontalPixelColors[46] = PixelColor.MakePixelColor(255,0,255, 255);
            HorizontalPixelColors[47] = PixelColor.MakePixelColor(138,43,226, 255);
            HorizontalPixelColors[48] = PixelColor.MakePixelColor(0,128,0, 255);
            HorizontalPixelColors[49] = PixelColor.MakePixelColor(128,0,128, 255);
        }
    }
}