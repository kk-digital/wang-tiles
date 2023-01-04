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

        public Dictionary<int, CornerColor> CornerColorsJSONMap;
        public Dictionary<int, VerticalColor> VerticalColorsJSONMap;
        public Dictionary<int, HorizontalColor> HorizontalColorsJSONMap;

        public int CornerColorJSONCount=0;
        public int VerticalColorJSONCount=0;
        public int HorizontalColorJSONCount=0;

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

        public CornerColor RetrieveCornerColorForJSON(int cornerColorInt){
            // if corner color already exists
            if (this.CornerColorsJSONMap.ContainsKey(cornerColorInt)){
                // return value
                return this.CornerColorsJSONMap[cornerColorInt];
            } else {
                // then does not exist, add it
                // the index to be used is +2 since first two indexes are reserved
                this.CornerColorsJSONMap[cornerColorInt]=(CornerColor)this.CornerColorCount+2;

                // Increment count for the new color
                this.CornerColorCount++;
            }

            return this.CornerColorsJSONMap[cornerColorInt];
        }

        public VerticalColor RetrieveVerticalColorForJSON(int verticalColorInt){
            // if vertical color already exists
            if (this.VerticalColorsJSONMap.ContainsKey(verticalColorInt)){
                // return value
                return this.VerticalColorsJSONMap[verticalColorInt];
            } else {
                // then does not exist, add it
                // the index to be used is +2 since first two indexes are reserved
                this.VerticalColorsJSONMap[verticalColorInt]=(VerticalColor)this.VerticalColorCount+2;

                // Increment count for the new color
                this.VerticalColorCount++;
            }

            return this.VerticalColorsJSONMap[verticalColorInt];
        }

        public HorizontalColor RetrieveHorizontalColorForJSON(int horizontalColorInt){
            // if horizontal color already exists
            if (this.HorizontalColorsJSONMap.ContainsKey(horizontalColorInt)){
                // return value
                return this.HorizontalColorsJSONMap[horizontalColorInt];
            } else {
                // then does not exist, add it
                // the index to be used is +2 since first two indexes are reserved
                this.HorizontalColorsJSONMap[horizontalColorInt]=(HorizontalColor)this.HorizontalColorCount+2;

                // Increment count for the new color
                this.HorizontalColorCount++;
            }

            return this.HorizontalColorsJSONMap[horizontalColorInt];
        }

        public ColorMap()
        {
            this.CornerColorsJSONMap = new Dictionary < int, CornerColor > ();
            this.VerticalColorsJSONMap = new Dictionary < int, VerticalColor > ();
            this.HorizontalColorsJSONMap = new Dictionary < int, HorizontalColor > ();

            int length=119;
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
            // DarkOliveGreen- 69-78  ( 85, 107, 47, 1 )
            // DarkSalmon- 79-88 ( 233, 150, 122, 1 )
            // MediumAquamarine- 89-98 ( 102, 205, 170, 1 )
            // Magenta- 99-108 ( 255, 0, 255, 1 )
            // Maroon- 109-118 ( 128, 0, 0, 1 )
            for (int i=0;i<=9;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255,0,0, 255);
            }

            for (int i=10;i<=18;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(0,0,205, 255);
            }

            for (int i=19;i<=28;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(0,205,0, 255);
            }

            for (int i=29;i<=38;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(92,64,51, 255);
            }

            for (int i=39;i<=48;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255,250,205,255);
            }

            for (int i=49;i<=58;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255,127,80);
            }

            for (int i=59;i<=68;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(246,190,0,255);
            }

            for (int i=69;i<=78;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(85, 107, 47, 1);
            }

            for (int i=79;i<=88;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(233, 150, 122, 1);
            }

            for (int i=89;i<=98;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(102, 205, 170, 1);
            }

            for (int i=99;i<=108;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor(255, 0, 255, 1);
            }

            for (int i=109;i<=118;i++){
                CornerPixelColors[i] = PixelColor.MakePixelColor( 128, 0, 0, 1);
            }

            
            PixelColor[] colorSet = new PixelColor[length];
            colorSet[0]=PixelColor.MakePixelColor(0,0,255,255);
            colorSet[1]=PixelColor.MakePixelColor(255,255,0,255);
            colorSet[2]=PixelColor.MakePixelColor(255,255,204, 255);
            colorSet[3]=PixelColor.MakePixelColor(0,255,255, 255);
            colorSet[4]=PixelColor.MakePixelColor(128,0,0, 255);
            colorSet[5]=PixelColor.MakePixelColor(0,128,0, 255);
            colorSet[6]=PixelColor.MakePixelColor(0,0,128, 255);
            colorSet[7]=PixelColor.MakePixelColor(128,128,0, 255);
            colorSet[8]=PixelColor.MakePixelColor(0,128,128, 255);
            colorSet[9]=PixelColor.MakePixelColor(255,0,255,255);
            
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