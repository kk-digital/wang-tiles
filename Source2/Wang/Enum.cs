namespace Wang
{
    public enum TileGeometry
    {
        Error = 0,
        SB,
        HB,
        TB,
        L1,
        L2,
        QP,
        HP,
        FP
    }

    
    // public enum TileIsoType
     public enum TileGeometryAndRotation
    {
        Error,
        
        SB_R0,
        SA_R0,
        
        HB_R0,
        HB_R1,
        HB_R2,
        HB_R3,
        
        TB_R0,
        TB_R1,
        TB_R2,
        TB_R3,
        
        L1_R0,
        L1_R1,
        L1_R2,
        L1_R3,
        L1_R4,
        L1_R5,
        L1_R6,
        L1_R7,
        
        L2_R0,
        L2_R1,
        L2_R2,
        L2_R3,
        L2_R4,
        L2_R5,
        L2_R6,
        L2_R7,
        
        QP_R0,
        QP_R1,
        QP_R2,
        QP_R3,
        
        HP_R0,
        HP_R1,
        HP_R2,
        HP_R3,
        
        FP_R0,
        FP_R1,
        FP_R2,
        FP_R3,
        
    }

    public enum TileCorner{
        NW,
        NE,
        SE,
        SW,
    }

    public enum TileOffset{
        C0,
        C1,
        C2,
        C3,
        C4,
        C5,
        C6,
        C7,
        C8,
    }

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
}