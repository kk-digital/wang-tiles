namespace WangTile
{
    public static class TetrisMismatchCalculator
    {

        public static int GetBoardTotalMismatch(Board board, int tileSetID, bool useBitmasking){
            int col = board.Height;
            int row = board.Width;
            int totalMismatches=0;

            for (int i=0;i<col;i++){
                for (int j=0;j<row;j++){
                    int index = Utils.GetBoardSlotIndex(board.Width, i, j);

                    int? tileID = board.TileSlots[index].TileID;
                    if (tileID!=null){
                        WangTile tile = board.TileSet[tileSetID].Tiles[(int)tileID];

                        (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = board.GetTileAdjacentColorValues(useBitmasking, i, j);

                        totalMismatches+=TetrisMismatchCalculator.CountMismatchOnCorners_ForRemoval(cColors, tile.TileBitMask);
                        totalMismatches+=TetrisMismatchCalculator.CountMismatchVertical_ForRemoval(vColors, tile.TileBitMask);
                        totalMismatches+=TetrisMismatchCalculator.CountMismatchHorizontal_ForRemoval(hColors, tile.TileBitMask);
                        
                    }
                }
            }

            return totalMismatches;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////// Mismatch Calculation for Placement //////////////////////////////////
        public static int CountMismatchOnCorners_ForPlacement(CornerColor[] cornerColors, int tileBitmask, ColorMatching colorMatching){
            int numberOfMismatch=0;
            
            // NW corner
            CornerColor cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_8NE)) == (1<<(int)BitMask.NW_8NE)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            CornerColor cornerColorC8_NE=cornerColors[(int)TileOffsetCorner.C8_NE];
            if (cornerColorC8_NE==CornerColor.Border){
                cornerColorC8_NE=CornerColor.WildCard;
            }

            if (cornerColorC0_NW!=CornerColor.WildCard && (cornerColorC0_NW!=cornerColorC8_NE && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColorC8_NE==CornerColor.WildCard) || 
            (cornerColorC8_NE==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            )) {
                numberOfMismatch++;
            }

            cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_1SE)) == (1<<(int)BitMask.NW_1SE)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            CornerColor cornerColorC1_SE=cornerColors[(int)TileOffsetCorner.C1_SE];
            if (cornerColorC1_SE==CornerColor.Border){
                cornerColorC1_SE=CornerColor.WildCard;
            }

            if (cornerColorC0_NW!=CornerColor.WildCard && (cornerColorC0_NW!=cornerColorC1_SE && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColorC1_SE==CornerColor.WildCard) || 
            (cornerColorC1_SE==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            )) {
                numberOfMismatch++;
            }

            cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_2SW)) == (1<<(int)BitMask.NW_2SW)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            CornerColor cornerColorC2_SW=cornerColors[(int)TileOffsetCorner.C2_SW];
            if (cornerColorC2_SW==CornerColor.Border){
                cornerColorC2_SW=CornerColor.WildCard;
            }

            if (cornerColorC0_NW!=CornerColor.WildCard && (cornerColorC0_NW!=cornerColorC2_SW && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColorC2_SW==CornerColor.WildCard) || 
            (cornerColorC2_SW==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            )) {
                numberOfMismatch++;
            }

            // NE Corner
            CornerColor cornerColorC0_NE=cornerColors[(int)TileOffsetCorner.C0_NE];
            if ((tileBitmask&(1<<(int)BitMask.NE_2SE)) == (1<<(int)BitMask.NE_2SE)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            CornerColor cornerColorC2_SE=cornerColors[(int)TileOffsetCorner.C2_SE];
            if (cornerColorC2_SE==CornerColor.Border){
                cornerColorC2_SE=CornerColor.WildCard;
            }

            if (cornerColorC0_NE!=CornerColor.WildCard && (cornerColorC0_NE!=cornerColorC2_SE && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColorC2_SE==CornerColor.WildCard) || 
            (cornerColorC2_SE==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.NE_3SW)) == (1<<(int)BitMask.NE_3SW)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            CornerColor cornerColorC3_SW=cornerColors[(int)TileOffsetCorner.C3_SW];
            if (cornerColorC3_SW==CornerColor.Border){
                cornerColorC3_SW=CornerColor.WildCard;
            }

            if (cornerColorC0_NE!=CornerColor.WildCard && (cornerColorC0_NE!=cornerColorC3_SW && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColorC3_SW==CornerColor.WildCard) || 
            (cornerColorC3_SW==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if (cornerColorC0_NE!=CornerColor.WildCard && (tileBitmask&(1<<(int)BitMask.NE_4NW)) == (1<<(int)BitMask.NE_4NW)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            CornerColor cornerColorC4_NW=cornerColors[(int)TileOffsetCorner.C4_NW];
            if (cornerColorC4_NW==CornerColor.Border){
                cornerColorC4_NW=CornerColor.WildCard;
            }

            if ((cornerColorC0_NE!=cornerColorC4_NW && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColorC4_NW==CornerColor.WildCard) || 
            (cornerColorC4_NW==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            // SE corner
            CornerColor cornerColorC0_SE=cornerColors[(int)TileOffsetCorner.C0_SE];
            if ((tileBitmask&(1<<(int)BitMask.SE_4SW)) == (1<<(int)BitMask.SE_4SW)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            CornerColor cornerColorC4_SW=cornerColors[(int)TileOffsetCorner.C4_SW];
            if (cornerColorC4_SW==CornerColor.Border){
                cornerColorC4_SW=CornerColor.WildCard;
            }

            if (cornerColorC0_SE!=CornerColor.WildCard && (cornerColorC0_SE!=cornerColorC4_SW && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColorC4_SW==CornerColor.WildCard) || 
            (cornerColorC4_SW==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SE_5NW)) == (1<<(int)BitMask.SE_5NW)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            CornerColor cornerColorC5_NW=cornerColors[(int)TileOffsetCorner.C5_NW];
            if (cornerColorC5_NW==CornerColor.Border){
                cornerColorC5_NW=CornerColor.WildCard;
            }

            if (cornerColorC0_SE!=CornerColor.WildCard && (cornerColorC0_SE!=cornerColorC5_NW && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColorC5_NW==CornerColor.WildCard) || 
            (cornerColorC5_NW==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SE_6NE)) == (1<<(int)BitMask.SE_6NE)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            CornerColor cornerColorC6_NE=cornerColors[(int)TileOffsetCorner.C6_NE];
            if (cornerColorC6_NE==CornerColor.Border){
                cornerColorC6_NE=CornerColor.WildCard;
            }

            if (cornerColorC0_SE!=CornerColor.WildCard && (cornerColorC0_SE!=cornerColorC6_NE && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColorC6_NE==CornerColor.WildCard) || 
            (cornerColorC6_NE==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            // SW Corner
            CornerColor cornerColorC0_SW=cornerColors[(int)TileOffsetCorner.C0_SW];
            if ((tileBitmask&(1<<(int)BitMask.SW_6NW)) == (1<<(int)BitMask.SW_6NW)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            CornerColor cornerColorC6_NW=cornerColors[(int)TileOffsetCorner.C6_NW];
            if (cornerColorC6_NW==CornerColor.Border){
                cornerColorC6_NW=CornerColor.WildCard;
            }

            if (cornerColorC0_SW!=CornerColor.WildCard && (cornerColorC0_SW!=cornerColorC6_NW && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColorC6_NW==CornerColor.WildCard) || 
            (cornerColorC6_NW==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SW_7NE)) == (1<<(int)BitMask.SW_7NE)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            CornerColor cornerColorC7_NE=cornerColors[(int)TileOffsetCorner.C7_NE];
            if (cornerColorC7_NE==CornerColor.Border){
                cornerColorC7_NE=CornerColor.WildCard;
            }

            if (cornerColorC0_SW!=CornerColor.WildCard && (cornerColorC0_SW!=cornerColorC7_NE && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColorC7_NE==CornerColor.WildCard) || 
            (cornerColorC7_NE==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }
            
            if ((tileBitmask&(1<<(int)BitMask.SW_8SE)) == (1<<(int)BitMask.SW_8SE)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            CornerColor cornerColorC8_SE=cornerColors[(int)TileOffsetCorner.C8_SE];
            if (cornerColorC8_SE==CornerColor.Border){
                cornerColorC8_SE=CornerColor.WildCard;
            }

            if (cornerColorC0_SW!=CornerColor.WildCard && (cornerColorC0_SW!=cornerColorC8_SE && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColorC8_SE==CornerColor.WildCard) || 
            (cornerColorC8_SE==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        public static int CountMismatchVertical_ForPlacement(VerticalColor[] vColors, int tileBitmask, ColorMatching colorMatching){
            int numberOfMismatch=0;

            // North
            VerticalColor vColorV0_N=vColors[(int)TileOffsetVertical.V0_N];
            if ((tileBitmask&(1<<(int)BitMask.N_2S)) == (1<<(int)BitMask.N_2S)){
                vColorV0_N=VerticalColor.WildCard;
            }
            VerticalColor vColorV2_S=vColors[(int)TileOffsetVertical.V2_S];
            if (vColorV2_S==VerticalColor.Border){
                vColorV2_S=VerticalColor.WildCard;
            }

            if (((vColorV0_N!=VerticalColor.WildCard && colorMatching==ColorMatching.CurrentTileBitmask) ||
            (colorMatching==ColorMatching.SymmetricalMatching)) && (vColorV0_N!=vColorV2_S && 
            !((vColorV0_N==VerticalColor.NoColor && vColorV2_S==VerticalColor.WildCard) || 
            (vColorV2_S==VerticalColor.NoColor && vColorV0_N==VerticalColor.WildCard))
            )){
                numberOfMismatch++;
            }

            // South
            VerticalColor vColorV0_S=vColors[(int)TileOffsetVertical.V0_S];
            if ((tileBitmask&(1<<(int)BitMask.S_6N)) == (1<<(int)BitMask.S_6N)){
                vColorV0_S=VerticalColor.WildCard;
            }
            VerticalColor vColorV6_N=vColors[(int)TileOffsetVertical.V6_N];
            if (vColorV6_N==VerticalColor.Border){
                vColorV6_N=VerticalColor.WildCard;
            }

            if (((vColorV0_S!=VerticalColor.WildCard && colorMatching==ColorMatching.CurrentTileBitmask) ||
            (colorMatching==ColorMatching.SymmetricalMatching)) && (vColorV0_S!=vColorV6_N && 
            !((vColorV0_S==VerticalColor.NoColor && vColorV6_N==VerticalColor.WildCard) || 
            (vColorV6_N==VerticalColor.NoColor && vColorV0_S==VerticalColor.WildCard)) 
            )){
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        public static int CountMismatchHorizontal_ForPlacement(HorizontalColor[] hColors, int tileBitmask, ColorMatching colorMatching){
            int numberOfMismatch=0;
            
            // West
            HorizontalColor hColorH0_W=hColors[(int)TileOffsetHorizontal.H0_W];
            if ((tileBitmask&(1<<(int)BitMask.W_8E)) == (1<<(int)BitMask.W_8E)){
                hColorH0_W=HorizontalColor.WildCard;
            }
            HorizontalColor hColorH8_E=hColors[(int)TileOffsetHorizontal.H8_E];
            if (hColorH8_E==HorizontalColor.Border){
                hColorH8_E=HorizontalColor.WildCard;
            }

            if (((hColorH0_W!=HorizontalColor.WildCard && colorMatching==ColorMatching.CurrentTileBitmask) ||
            (colorMatching==ColorMatching.SymmetricalMatching)) && (hColorH0_W!=hColorH8_E && 
            !((hColorH0_W==HorizontalColor.NoColor && hColorH8_E==HorizontalColor.WildCard) || 
            (hColorH8_E==HorizontalColor.NoColor && hColorH0_W==HorizontalColor.WildCard)) 
            )){
                numberOfMismatch++;
            }

            // East
            HorizontalColor hColorH0_E=hColors[(int)TileOffsetHorizontal.H0_E];
            if ((tileBitmask&(1<<(int)BitMask.E_4W)) == (1<<(int)BitMask.E_4W)){
                hColorH0_E=HorizontalColor.WildCard;
            }
            HorizontalColor hColorH4_W=hColors[(int)TileOffsetHorizontal.H4_W];
            if (hColorH4_W==HorizontalColor.Border){
                hColorH4_W=HorizontalColor.WildCard;
            }

            if (((hColorH0_E!=HorizontalColor.WildCard && colorMatching==ColorMatching.CurrentTileBitmask) ||
            (colorMatching==ColorMatching.SymmetricalMatching)) && (hColorH0_E!=hColorH4_W  && 
            !((hColorH0_E==HorizontalColor.NoColor && hColorH4_W==HorizontalColor.WildCard) || 
            (hColorH4_W==HorizontalColor.NoColor && hColorH0_E==HorizontalColor.WildCard)) 
            )){
                numberOfMismatch++;
            }
            
            return numberOfMismatch;
        }
        
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// Mismatch Calculation for Removal //////////////////////////////////
        public static int CountMismatchOnCorners_ForRemoval(CornerColor[] cornerColors, int tileBitmask){
            int numberOfMismatch=0;
            
            // NW corner
            CornerColor cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_8NE)) == (1<<(int)BitMask.NW_8NE)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            CornerColor cornerColorC8_NE=cornerColors[(int)TileOffsetCorner.C8_NE];
            if (cornerColorC8_NE==CornerColor.Border){
                cornerColorC8_NE=CornerColor.WildCard;
            }

            if (cornerColorC0_NW!=CornerColor.WildCard && (cornerColorC0_NW!=cornerColorC8_NE && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColorC8_NE==CornerColor.WildCard) || 
            (cornerColorC8_NE==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            ) && cornerColorC0_NW!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_1SE)) == (1<<(int)BitMask.NW_1SE)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            CornerColor cornerColorC1_SE=cornerColors[(int)TileOffsetCorner.C1_SE];
            if (cornerColorC1_SE==CornerColor.Border){
                cornerColorC1_SE=CornerColor.WildCard;
            }

            if (cornerColorC0_NW!=CornerColor.WildCard && (cornerColorC0_NW!=cornerColorC1_SE && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColorC1_SE==CornerColor.WildCard) || 
            (cornerColorC1_SE==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            ) && cornerColorC0_NW!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_2SW)) == (1<<(int)BitMask.NW_2SW)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            CornerColor cornerColorC2_SW=cornerColors[(int)TileOffsetCorner.C2_SW];
            if (cornerColorC2_SW==CornerColor.Border){
                cornerColorC2_SW=CornerColor.WildCard;
            }

            if (cornerColorC0_NW!=CornerColor.WildCard && (cornerColorC0_NW!=cornerColorC2_SW && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColorC2_SW==CornerColor.WildCard) || 
            (cornerColorC2_SW==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            ) && cornerColorC0_NW!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            // NE Corner
            CornerColor cornerColorC0_NE=cornerColors[(int)TileOffsetCorner.C0_NE];
            if ((tileBitmask&(1<<(int)BitMask.NE_2SE)) == (1<<(int)BitMask.NE_2SE)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            CornerColor cornerColorC2_SE=cornerColors[(int)TileOffsetCorner.C2_SE];
            if (cornerColorC2_SE==CornerColor.Border){
                cornerColorC2_SE=CornerColor.WildCard;
            }

            if (cornerColorC0_NE!=CornerColor.WildCard && (cornerColorC0_NE!=cornerColorC2_SE && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColorC2_SE==CornerColor.WildCard) || 
            (cornerColorC2_SE==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            ) && cornerColorC0_NE!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.NE_3SW)) == (1<<(int)BitMask.NE_3SW)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            CornerColor cornerColorC3_SW=cornerColors[(int)TileOffsetCorner.C3_SW];
            if (cornerColorC3_SW==CornerColor.Border){
                cornerColorC3_SW=CornerColor.WildCard;
            }

            if (cornerColorC0_NE!=CornerColor.WildCard && (cornerColorC0_NE!=cornerColorC3_SW && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColorC3_SW==CornerColor.WildCard) || 
            (cornerColorC3_SW==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            ) && cornerColorC0_NE!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            if (cornerColorC0_NE!=CornerColor.WildCard && (tileBitmask&(1<<(int)BitMask.NE_4NW)) == (1<<(int)BitMask.NE_4NW)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            CornerColor cornerColorC4_NW=cornerColors[(int)TileOffsetCorner.C4_NW];
            if (cornerColorC4_NW==CornerColor.Border){
                cornerColorC4_NW=CornerColor.WildCard;
            }

            if ((cornerColorC0_NE!=cornerColorC4_NW && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColorC4_NW==CornerColor.WildCard) || 
            (cornerColorC4_NW==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            ) && cornerColorC0_NE!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            // SE corner
            CornerColor cornerColorC0_SE=cornerColors[(int)TileOffsetCorner.C0_SE];
            if ((tileBitmask&(1<<(int)BitMask.SE_4SW)) == (1<<(int)BitMask.SE_4SW)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            CornerColor cornerColorC4_SW=cornerColors[(int)TileOffsetCorner.C4_SW];
            if (cornerColorC4_SW==CornerColor.Border){
                cornerColorC4_SW=CornerColor.WildCard;
            }

            if (cornerColorC0_SE!=CornerColor.WildCard && (cornerColorC0_SE!=cornerColorC4_SW && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColorC4_SW==CornerColor.WildCard) || 
            (cornerColorC4_SW==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            ) && cornerColorC0_SE!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SE_5NW)) == (1<<(int)BitMask.SE_5NW)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            CornerColor cornerColorC5_NW=cornerColors[(int)TileOffsetCorner.C5_NW];
            if (cornerColorC5_NW==CornerColor.Border){
                cornerColorC5_NW=CornerColor.WildCard;
            }

            if (cornerColorC0_SE!=CornerColor.WildCard && (cornerColorC0_SE!=cornerColorC5_NW && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColorC5_NW==CornerColor.WildCard) || 
            (cornerColorC5_NW==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            ) && cornerColorC0_SE!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SE_6NE)) == (1<<(int)BitMask.SE_6NE)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            CornerColor cornerColorC6_NE=cornerColors[(int)TileOffsetCorner.C6_NE];
            if (cornerColorC6_NE==CornerColor.Border){
                cornerColorC6_NE=CornerColor.WildCard;
            }

            if (cornerColorC0_SE!=CornerColor.WildCard && (cornerColorC0_SE!=cornerColorC6_NE && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColorC6_NE==CornerColor.WildCard) || 
            (cornerColorC6_NE==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            ) && cornerColorC0_SE!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            // SW Corner
            CornerColor cornerColorC0_SW=cornerColors[(int)TileOffsetCorner.C0_SW];
            if ((tileBitmask&(1<<(int)BitMask.SW_6NW)) == (1<<(int)BitMask.SW_6NW)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            CornerColor cornerColorC6_NW=cornerColors[(int)TileOffsetCorner.C6_NW];
            if (cornerColorC6_NW==CornerColor.Border){
                cornerColorC6_NW=CornerColor.WildCard;
            }

            if (cornerColorC0_SW!=CornerColor.WildCard && (cornerColorC0_SW!=cornerColorC6_NW && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColorC6_NW==CornerColor.WildCard) || 
            (cornerColorC6_NW==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            ) && cornerColorC0_SW!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SW_7NE)) == (1<<(int)BitMask.SW_7NE)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            CornerColor cornerColorC7_NE=cornerColors[(int)TileOffsetCorner.C7_NE];
            if (cornerColorC7_NE==CornerColor.Border){
                cornerColorC7_NE=CornerColor.WildCard;
            }

            if (cornerColorC0_SW!=CornerColor.WildCard && (cornerColorC0_SW!=cornerColorC7_NE && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColorC7_NE==CornerColor.WildCard) || 
            (cornerColorC7_NE==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            ) && cornerColorC0_SW!=CornerColor.WildCard) {
                numberOfMismatch++;
            }
            
            if ((tileBitmask&(1<<(int)BitMask.SW_8SE)) == (1<<(int)BitMask.SW_8SE)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            CornerColor cornerColorC8_SE=cornerColors[(int)TileOffsetCorner.C8_SE];
            if (cornerColorC8_SE==CornerColor.Border){
                cornerColorC8_SE=CornerColor.WildCard;
            }

            if (cornerColorC0_SW!=CornerColor.WildCard && (cornerColorC0_SW!=cornerColorC8_SE && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColorC8_SE==CornerColor.WildCard) || 
            (cornerColorC8_SE==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            ) && cornerColorC0_SW!=CornerColor.WildCard) {
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        public static int CountMismatchVertical_ForRemoval(VerticalColor[] vColors, int tileBitmask){
            int numberOfMismatch=0;

            VerticalColor vColorV0_N=vColors[(int)TileOffsetVertical.V0_N];
            if ((tileBitmask&(1<<(int)BitMask.N_2S)) == (1<<(int)BitMask.N_2S)){
                vColorV0_N=VerticalColor.WildCard;
            }
            VerticalColor vColorV2_S=vColors[(int)TileOffsetVertical.V2_S];
            if (vColorV2_S==VerticalColor.Border){
                vColorV2_S=VerticalColor.WildCard;
            }

            if ((vColorV0_N!=vColorV2_S && 
            !((vColorV0_N==VerticalColor.NoColor && vColorV2_S==VerticalColor.WildCard) || 
            (vColorV2_S==VerticalColor.NoColor && vColorV0_N==VerticalColor.WildCard))
            ) && vColorV0_N!=VerticalColor.WildCard){
                numberOfMismatch++;
            }


            VerticalColor vColorV0_S=vColors[(int)TileOffsetVertical.V0_S];
            if ((tileBitmask&(1<<(int)BitMask.S_6N)) == (1<<(int)BitMask.S_6N)){
                vColorV0_S=VerticalColor.WildCard;
            }
            VerticalColor vColorV6_N=vColors[(int)TileOffsetVertical.V6_N];
            if (vColorV6_N==VerticalColor.Border){
                vColorV6_N=VerticalColor.WildCard;
            }
            if ((vColorV0_S!=vColorV6_N && 
            !((vColorV0_S==VerticalColor.NoColor && vColorV6_N==VerticalColor.WildCard) || 
            (vColorV6_N==VerticalColor.NoColor && vColorV0_S==VerticalColor.WildCard)) 
            ) && vColorV0_S!=VerticalColor.WildCard){
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        public static int CountMismatchHorizontal_ForRemoval(HorizontalColor[] hColors, int tileBitmask){
            int numberOfMismatch=0;
        
            HorizontalColor hColorH0_W=hColors[(int)TileOffsetHorizontal.H0_W];
            if ((tileBitmask&(1<<(int)BitMask.W_8E)) == (1<<(int)BitMask.W_8E)){
                hColorH0_W=HorizontalColor.WildCard;
            }
            HorizontalColor hColorH8_E=hColors[(int)TileOffsetHorizontal.H8_E];
            if (hColorH8_E==HorizontalColor.Border){
                hColorH8_E=HorizontalColor.WildCard;
            }

            if ((hColorH0_W!=hColorH8_E && 
            !((hColorH0_W==HorizontalColor.NoColor && hColorH8_E==HorizontalColor.WildCard) || 
            (hColorH8_E==HorizontalColor.NoColor && hColorH0_W==HorizontalColor.WildCard)) 
            ) && hColorH0_W!=HorizontalColor.WildCard){
                numberOfMismatch++;
            }

            HorizontalColor hColorH0_E=hColors[(int)TileOffsetHorizontal.H0_E];
            if ((tileBitmask&(1<<(int)BitMask.E_4W)) == (1<<(int)BitMask.E_4W)){
                hColorH0_E=HorizontalColor.WildCard;
            }
            HorizontalColor hColorH4_W=hColors[(int)TileOffsetHorizontal.H4_W];
            if (hColorH4_W==HorizontalColor.Border){
                hColorH4_W=HorizontalColor.WildCard;
            }

            if ((hColorH0_E!=hColorH4_W  && 
            !((hColorH0_E==HorizontalColor.NoColor && hColorH4_W==HorizontalColor.WildCard) || 
            (hColorH4_W==HorizontalColor.NoColor && hColorH0_E==HorizontalColor.WildCard)) 
            ) && hColorH0_E!=HorizontalColor.WildCard){
                numberOfMismatch++;
            }
            
            return numberOfMismatch;
        }
    }
}   
