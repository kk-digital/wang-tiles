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
        NoColor, // No color for empty tile slots.
        WildCard,
        Border, // border of board
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

        AA,
        AB,
        AC,
        AD,
        AE,
        AF,
        AG,
        AH,
        AI,
        AJ,
        AK,
        AL,
        AM,
        AN,
        AO,
        AP,
        AQ,
        AR,
        AS,
        AT,
        AU,
        AV,
        AW,
        AX,
        AY,
        AZ, 

        BA,
        BB,
        BC,
        BD,
        BE,
        BF,
        BG,
        BH,
        BI,
        BJ,
        BK,
        BL,
        BM,
        BN,
        BO,
        BP,
        BQ,
        BR,
        BS,
        BT,
        BU,
        BV,
        BW,
        BX,
        BY,
        BZ,
        CA,
        CB,
        CC,
        CD,
        CE,
        CF,
        CG,
        CH,
        CI,
        CJ,
        CK,
        CL,
        CM,
        CN,
        CO,
        CP,
        CQ,
        CR,
        CS,
        CT,
        CU,
        CV,
        CW,
        CX,
        CY,
        CZ,

        DA,
        DB,
        DC,
        DD,
        DE,
        DF,
        DG,
        DH,
        DI,
        DJ,
        DK,
        DL,
        DM,
        DN,
        DO,
        DP,
        DQ,
        DR,
        DS,
        DT,
        DU,
        DV,
        DW,
        DX,
        DY,
        DZ,
    }

        public enum HorizontalColor {
        NoColor, // No color for empty tile slots.
        WildCard,
        Border, // border of board
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

        AA,
        AB,
        AC,
        AD,
        AE,
        AF,
        AG,
        AH,
        AI,
        AJ,
        AK,
        AL,
        AM,
        AN,
        AO,
        AP,
        AQ,
        AR,
        AS,
        AT,
        AU,
        AV,
        AW,
        AX,
        AY,
        AZ,

        BA,
        BB,
        BC,
        BD,
        BE,
        BF,
        BG,
        BH,
        BI,
        BJ,
        BK,
        BL,
        BM,
        BN,
        BO,
        BP,
        BQ,
        BR,
        BS,
        BT,
        BU,
        BV,
        BW,
        BX,
        BY,
        BZ,

        CA,
        CB,
        CC,
        CD,
        CE,
        CF,
        CG,
        CH,
        CI,
        CJ,
        CK,
        CL,
        CM,
        CN,
        CO,
        CP,
        CQ,
        CR,
        CS,
        CT,
        CU,
        CV,
        CW,
        CX,
        CY,
        CZ,

        DA,
        DB,
        DC,
        DD,
        DE,
        DF,
        DG,
        DH,
        DI,
        DJ,
        DK,
        DL,
        DM,
        DN,
        DO,
        DP,
        DQ,
        DR,
        DS,
        DT,
        DU,
        DV,
        DW,
        DX,
        DY,
        DZ,
    }

        public enum VerticalColor {
        NoColor, // No color for empty tile slots.
        WildCard,
        Border, // border of board
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

        AA,
        AB,
        AC,
        AD,
        AE,
        AF,
        AG,
        AH,
        AI,
        AJ,
        AK,
        AL,
        AM,
        AN,
        AO,
        AP,
        AQ,
        AR,
        AS,
        AT,
        AU,
        AV,
        AW,
        AX,
        AY,
        AZ,

        BA,
        BB,
        BC,
        BD,
        BE,
        BF,
        BG,
        BH,
        BI,
        BJ,
        BK,
        BL,
        BM,
        BN,
        BO,
        BP,
        BQ,
        BR,
        BS,
        BT,
        BU,
        BV,
        BW,
        BX,
        BY,
        BZ,

        CA,
        CB,
        CC,
        CD,
        CE,
        CF,
        CG,
        CH,
        CI,
        CJ,
        CK,
        CL,
        CM,
        CN,
        CO,
        CP,
        CQ,
        CR,
        CS,
        CT,
        CU,
        CV,
        CW,
        CX,
        CY,
        CZ,

        DA,
        DB,
        DC,
        DD,
        DE,
        DF,
        DG,
        DH,
        DI,
        DJ,
        DK,
        DL,
        DM,
        DN,
        DO,
        DP,
        DQ,
        DR,
        DS,
        DT,
        DU,
        DV,
        DW,
        DX,
        DY,
        DZ,

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
}