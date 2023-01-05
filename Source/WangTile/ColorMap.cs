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

        public ColorMap((PixelColor[] cornerPixelColors,PixelColor[] verticalPixelColors,PixelColor[] horizontalPixelColors) colors)
        {
            this.CornerColorsJSONMap = new Dictionary < int, CornerColor > ();
            this.VerticalColorsJSONMap = new Dictionary < int, VerticalColor > ();
            this.HorizontalColorsJSONMap = new Dictionary < int, HorizontalColor > ();
            
            CornerPixelColors = colors.cornerPixelColors;
            VerticalPixelColors = colors.verticalPixelColors;
            HorizontalPixelColors = colors.horizontalPixelColors;

            
        }
    }
}