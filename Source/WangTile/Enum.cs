namespace WangTile
{
    public enum TileCorner{
        NW,
        NE,
        SE,
        SW,
    }

    //          |         |
    //          |         |
    //    1     |    2    |    3
    // _ _ _ _S̲E̲|S̲W̲__S̲__S̲E̲|S̲W̲_ _ _ _
    //        NE|NW  N  NE|NW
    //          |         |
    //    8    E|W   0   E|W   4
    //          |         |
    // _ _ _ _S̲E̲|S̲W̲__S̲__S̲E̲|S̲W̲_ _ _ _
    //        NE|NW  N  NE|NW
    //    7     |    6    |    5
    //          |         |
    //          |         |
    // 0:NW : 0:NW, 8:NE, 1:SE, 2:SW
    // 0:NE : 0:NE, 2:SE, 3:SW, 4:NW
    // 0:SE : 0:SE, 4:SW, 5:NW, 6:NE
    // 0:SW : 0:SW, 6:NW, 7:NE, 8:SE
    public enum TileOffsetCorner {
    // 0:NW : 0:NW, 8:NE, 1:SE, 2:SW
    C0_NW,
    C8_NE,
    C1_SE,
    C2_SW,

    // 0:NE : 0:NE, 2:SE, 3:SW, 4:NW
    C0_NE,
    C2_SE,
    C3_SW,
    C4_NW,

    // 0:SE : 0:SE, 4:SW, 5:NW, 6:NE
    C0_SE,
    C4_SW,
    C5_NW,
    C6_NE,


    // 0:SW : 0:SW, 6:NW, 7:NE, 8:SE
    C0_SW, 
    C6_NW,
    C7_NE,
    C8_SE, 
    }

    //          |         |
    //          |         |
    //    1     |    2    |    3
    // _ _ _ _S̲E̲|S̲W̲__S̲__S̲E̲|S̲W̲_ _ _ _
    //        NE|NW  N  NE|NW
    //          |         |
    //    8    E|W   0   E|W   4
    //          |         |
    // _ _ _ _S̲E̲|S̲W̲__S̲__S̲E̲|S̲W̲_ _ _ _
    //        NE|NW  N  NE|NW
    //    7     |    6    |    5
    //          |         |
    //          |         |
    // V0_N: V0_N, V2_S
    // V0_S: V0_S, V6_N
    public enum TileOffsetVertical {
        // V0_N: V0_N, V2_S
        V0_N,
        V2_S,

        // V0_S: V0_S, V6_N
        V0_S,
        V6_N,
    }

    //          |         |
    //          |         |
    //    1     |    2    |    3
    // _ _ _ _S̲E̲|S̲W̲__S̲__S̲E̲|S̲W̲_ _ _ _
    //        NE|NW  N  NE|NW
    //          |         |
    //    8    E|W   0   E|W   4
    //          |         |
    // _ _ _ _S̲E̲|S̲W̲__S̲__S̲E̲|S̲W̲_ _ _ _
    //        NE|NW  N  NE|NW
    //    7     |    6    |    5
    //          |         |
    //          |         |
    // H0_E: H0_E, H4_W
    // H0_W: H0_W, H8_E
    public enum TileOffsetHorizontal {
        // H0_E: H0_E, H4_W
        H0_E,
        H4_W,

        // H0_W: H0_W, H8_E
        H0_W,
        H8_E,
    }

    //          |         |
    //          |         |
    //    1     |    2    |    3
    // _ _ _ _S̲E̲|S̲W̲__S̲__S̲E̲|S̲W̲_ _ _ _
    //        NE|NW  N  NE|NW
    //          |         |
    //    8    E|W   0   E|W   4
    //          |         |
    // _ _ _ _S̲E̲|S̲W̲__S̲__S̲E̲|S̲W̲_ _ _ _
    //        NE|NW  N  NE|NW
    //    7     |    6    |    5
    //          |         |
    //          |         |
    // if bit is 0, then don't ignore 
    // if bit is 1, then ignore
    public enum BitMask {
        NW_8NE,
        NW_1SE,
        NW_2SW,

        N_2S,

        NE_2SE,
        NE_3SW,
        NE_4NW,

        E_4W,

        SE_4SW,
        SE_5NW,
        SE_6NE,

        S_6N,

        SW_6NW,
        SW_7NE,
        SW_8SE,

        W_8E,
    }
}