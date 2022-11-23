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
    // _ _ _ _S̲E̲|S̲W̲_____S̲E̲|S̲W̲_ _ _ _
    //        NE|NW     NE|NW
    //    8     |    0    |    4
    //          |         |
    // _ _ _ _S̲E̲|S̲W̲_____S̲E̲|S̲W̲_ _ _ _
    //        NE|NW     NE|NW
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