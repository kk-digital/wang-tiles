namespace WangTile
{   

    public struct CornerColorData{
        public CornerColor CornerColorID;
        public int NumberOfTimesUsed;
        public int ColorPalette;
        // int[] TilesUsingThis;

        public CornerColorData(CornerColor cornerColorID){
            this.CornerColorID=cornerColorID;
            this.NumberOfTimesUsed=1;
            this.ColorPalette=0;
        }
    }

    public struct HorizontalColorData{
        public HorizontalColor HorizontalColorID;

        public CornerColor CornerColor1;
        public CornerColor CornerColor2;

        public int NumberOfTimesUsed;
        public int ColorPalette;
        // int[] TilesUsingThis;

        public HorizontalColorData(HorizontalColor hColorID,CornerColor cColorID1,CornerColor cColorID2){
            this.HorizontalColorID=hColorID;
            this.CornerColor1=cColorID1;
            this.CornerColor2=cColorID2;

            this.NumberOfTimesUsed=1;
            this.ColorPalette=0;
        }
    }

    public struct VerticalColorData{
        public VerticalColor VerticalColorID;

        public CornerColor CornerColor1;
        public CornerColor CornerColor2;
        
        public int NumberOfTimesUsed;
        public int ColorPalette;
        // int[] TilesUsingThis;

        public VerticalColorData(VerticalColor vColorID,CornerColor cColorID1,CornerColor cColorID2){
            this.VerticalColorID=vColorID;
            this.CornerColor1=cColorID1;
            this.CornerColor2=cColorID2;
            
            this.NumberOfTimesUsed=1;
            this.ColorPalette=0;
        }
    }

    public enum CornerColor {
        WildCard,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
    }

        public enum HorizontalColor {
        WildCard,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
    }

        public enum VerticalColor {
        WildCard,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
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
            result.Color = toRGBA8((char)r, (char)g, (char)b, (char)a);
            return result;
        }

        public static PixelColor MakePixelColor(int r, int g, int b)
        {
            return MakePixelColor(r, g, b, 255);
        }

        static int toRGBA8(char r, char g, char b, char a)
        {
            return (r << 24) + (g << 16) + (b << 8) + a;
        }
    }

    public class ColorMap
    {
        public PixelColor[] PixelColors;
        public int ColorCount=0;

        public int GetColorCount()
        {
            return this.ColorCount;
        }

        public void IncrementColorCount()
        {
            this.ColorCount++;
        }


        public PixelColor GetPixelColor(int index)
        {
            return PixelColors[index];
        }

        public ColorMap()
        {
            PixelColors = new PixelColor[32];

        //     PixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
        //     PixelColors[1] = PixelColor.MakePixelColor(0,255,0, 255);
        //     PixelColors[2] = PixelColor.MakePixelColor(128,128,0, 255);
        //     PixelColors[3] = PixelColor.MakePixelColor(255,255,0, 255);
        //     PixelColors[4] = PixelColor.MakePixelColor(0,255,255, 255);
        //     PixelColors[5] = PixelColor.MakePixelColor(255,0,255, 255);
        //     PixelColors[6] = PixelColor.MakePixelColor(138,43,226, 255);
        //     PixelColors[7] = PixelColor.MakePixelColor(0,128,0, 255);
        //     PixelColors[8] = PixelColor.MakePixelColor(128,0,128, 255);
        //     PixelColors[9] = PixelColor.MakePixelColor(0,128,128, 255);
        //     PixelColors[10] = PixelColor.MakePixelColor(0,0,128, 255);
        //     PixelColors[11] = PixelColor.MakePixelColor(255,140,0, 255);
        //     PixelColors[12] = PixelColor.MakePixelColor(255,228,196, 255);
        //     PixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
        //     PixelColors[14] = PixelColor.MakePixelColor(255,20,147, 255);
        //     PixelColors[15] = PixelColor.MakePixelColor(0,0,255, 250);
        //     PixelColors[16] = PixelColor.MakePixelColor(255,0,0, 255);
        //     PixelColors[17] = PixelColor.MakePixelColor(0,255,0, 255);
        //     PixelColors[18] = PixelColor.MakePixelColor(128,128,0, 255);
        //     PixelColors[19] = PixelColor.MakePixelColor(255,255,0, 255);
        //     PixelColors[20] = PixelColor.MakePixelColor(0,255,255, 255);
        //     PixelColors[21] = PixelColor.MakePixelColor(255,0,255, 255);
        //     PixelColors[22] = PixelColor.MakePixelColor(138,43,226, 255);
        //     PixelColors[23] = PixelColor.MakePixelColor(0,128,0, 255);
        //     PixelColors[24] = PixelColor.MakePixelColor(128,0,128, 255);
        //     PixelColors[25] = PixelColor.MakePixelColor(0,128,128, 255);
        //     PixelColors[26] = PixelColor.MakePixelColor(0,0,128, 255);
        //     PixelColors[27] = PixelColor.MakePixelColor(255,140,0, 255);
        //     PixelColors[28] = PixelColor.MakePixelColor(255,228,196, 255);
        //     PixelColors[29] = PixelColor.MakePixelColor(0,0,205, 255);
        //     PixelColors[30] = PixelColor.MakePixelColor(255,20,147, 255);
        //     PixelColors[31] = PixelColor.MakePixelColor(0,0,255, 250);

            PixelColors[0] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[1] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[2] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[3] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[4] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[5] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[6] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[7] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[8] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[9] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[10] = PixelColor.MakePixelColor(255,0,0, 255);
            PixelColors[11] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[12] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[13] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[14] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[15] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[16] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[17] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[18] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[19] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[20] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[21] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[22] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[23] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[24] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[25] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[26] = PixelColor.MakePixelColor(0,0,128, 255);
            PixelColors[27] = PixelColor.MakePixelColor(255,140,0, 255);
            PixelColors[28] = PixelColor.MakePixelColor(255,228,196, 255);
            PixelColors[29] = PixelColor.MakePixelColor(0,0,205, 255);
            PixelColors[30] = PixelColor.MakePixelColor(255,20,147, 255);
            PixelColors[31] = PixelColor.MakePixelColor(0,0,255, 250);
        }
    }

}