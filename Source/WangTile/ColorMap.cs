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
            int length=118;
            CornerPixelColors = new PixelColor[length];
            VerticalPixelColors = new PixelColor[length];
            HorizontalPixelColors = new PixelColor[length];

            // Red - 0-9 index
            // Blue - 10-18 
            // Green - 19-28 
            // Brown - 29-38 
            // Light yellow - 39-48 
            // Orange - 49-58 
            // Dark yellow - 59-68
            //  - 69-78
            //  - 79-88
            //  - 89-98
            //  - 99-108
            //  - 109-118
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
            CornerPixelColors[49] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[50] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[51] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[52] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[53] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[54] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[55] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[56] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[57] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[58] = PixelColor.MakePixelColor(255,127,80);
            CornerPixelColors[59] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[60] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[61] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[62] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[63] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[64] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[65] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[66] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[67] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[68] = PixelColor.MakePixelColor(246,190,0,255);
            CornerPixelColors[69] = PixelColor.MakePixelColor(246,190,0,255);
            
            PixelColor[] colorSet = new PixelColor[length];
            colorSet[0]=PixelColor.MakePixelColor(255,0,0, 255);
            colorSet[1]=PixelColor.MakePixelColor(0,255,0, 255);
            colorSet[2]=PixelColor.MakePixelColor(128,128,0, 255);
            colorSet[3]=PixelColor.MakePixelColor(255,255,0, 255);
            colorSet[4]=PixelColor.MakePixelColor(0,255,255, 255);
            colorSet[5]=PixelColor.MakePixelColor(255,0,255, 255);
            colorSet[6]=PixelColor.MakePixelColor(138,43,226, 255);
            colorSet[7]=PixelColor.MakePixelColor(0,128,0, 255);
            colorSet[8]=PixelColor.MakePixelColor(128,0,128, 255);
            colorSet[9]=PixelColor.MakePixelColor(0,128,128, 255);
            
            // different vertical colors in all indexes
            int j = 0;
            for (int i=0;i<length;i++){
                if (j==10){
                    j=0;
                }

                VerticalPixelColors[i]=colorSet[j];
                j++;
            }
        

            // different horizontal colors in all indexes
            j = 0;
            for (int i=0;i<length;i++){
                if (j==10){
                    j=0;
                }

                HorizontalPixelColors[i]=colorSet[j];
                j++;
            }
        }
    }
}