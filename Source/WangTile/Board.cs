namespace WangTile
{
    public struct BoardTileSlot
    {
        public int? TileSetID;
        public int? TileID;
    }

    public struct TileMismatch{
        public int TileID;
        public int NumberOfMismatches;

        public TileMismatch(int tileID, int numberOfMismatches){
            this.TileID=tileID;
            this.NumberOfMismatches=numberOfMismatches;
        }
    }

    public class Board
    {
        public BoardTileSlot[] TileSlots;
        public int Height;
        public int Width;
        public WangTileSet[]? TileSet;
        
        // Constructor
        public Board(int height, int width)
        {
            TileSlots = new BoardTileSlot[height*width];
            Height = height;
            Width = width;
        }

        // Methods
        public void AddTileSet(WangTileSet tileSet)
        {   
            if (this.TileSet==null){
                this.TileSet = new WangTileSet[1];
                this.TileSet[0]=tileSet;
            }else{
                this.TileSet=this.TileSet.Append(tileSet).ToArray();
            }
        }

        public void PlaceTile(int tileSetID, int tileID, int col, int row)
        {
            int index = Utils.GetBoardSlotIndex(this.Width,col,row);
            this.TileSlots[index].TileSetID = tileSetID;
            this.TileSlots[index].TileID=tileID;
        }

        public void RemoveTile(int col, int row)
        {
            int index = Utils.GetBoardSlotIndex(this.Width,col,row);
            this.TileSlots[index].TileSetID = null;
            this.TileSlots[index].TileID=null;
        }

        public int[] GetTileMismatchArray(int tileSetID,int col, int row, bool useBitmasking){
            WangTile[] tiles = this.TileSet[tileSetID].Tiles;
            int[] tilesMismatches= new int[tiles.Length];


            for (int i=0;i<tiles.Length;i++){
                int numberOfMismatch = 0;
                int tileID = tiles[i].TileID;
                this.PlaceTile(tileSetID,tileID,col,row);

                (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = GetTileAdjacentColorValues(useBitmasking,col,row);

                
                numberOfMismatch=countMismatchOnCorners(cColors,tiles[i].TileBitMask);
                numberOfMismatch+=countMismatchVertical(vColors,tiles[i].TileBitMask);
                numberOfMismatch+=countMismatchHorizontal(hColors,tiles[i].TileBitMask);
                
                tilesMismatches[i]=numberOfMismatch;

                // Remove placed tile since
                // it's not the final tile yet.
                this.RemoveTile(col,row);
            }

            

            return tilesMismatches;
        }

        int countMismatchOnCorners(CornerColor[] cornerColors, int tileBitmask){
            int numberOfMismatch=0;

            if ((cornerColors[(int)TileOffsetCorner.C0_NW]!=cornerColors[(int)TileOffsetCorner.C8_NE] && (
                cornerColors[(int)TileOffsetCorner.C0_NW]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C8_NE]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.NW_8NE))==1<<(int)BitMask.NW_8NE &&
                cornerColors[(int)TileOffsetCorner.C8_NE]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NW]!=cornerColors[(int)TileOffsetCorner.C1_SE] && (
                cornerColors[(int)TileOffsetCorner.C0_NW]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C1_SE]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.NW_1SE))==1<<(int)BitMask.NW_1SE &&
                cornerColors[(int)TileOffsetCorner.C1_SE]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NW]!=cornerColors[(int)TileOffsetCorner.C2_SW] && (
                cornerColors[(int)TileOffsetCorner.C0_NW]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C2_SW]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.NW_2SW))==1<<(int)BitMask.NW_2SW &&
                cornerColors[(int)TileOffsetCorner.C2_SW]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NE]!=cornerColors[(int)TileOffsetCorner.C2_SE] && (
                cornerColors[(int)TileOffsetCorner.C0_NE]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C2_SE]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.NE_2SE))==1<<(int)BitMask.NE_2SE &&
                cornerColors[(int)TileOffsetCorner.C2_SE]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NE]!=cornerColors[(int)TileOffsetCorner.C3_SW] && (
                cornerColors[(int)TileOffsetCorner.C0_NE]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C3_SW]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.NE_3SW))==1<<(int)BitMask.NE_3SW &&
                cornerColors[(int)TileOffsetCorner.C3_SW]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NE]!=cornerColors[(int)TileOffsetCorner.C4_NW] && (
                cornerColors[(int)TileOffsetCorner.C0_NE]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C4_NW]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.NE_4NW))==1<<(int)BitMask.NE_4NW &&
                cornerColors[(int)TileOffsetCorner.C4_NW]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SE]!=cornerColors[(int)TileOffsetCorner.C4_SW] && (
                cornerColors[(int)TileOffsetCorner.C0_SE]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C4_SW]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.SE_4SW))==1<<(int)BitMask.SE_4SW &&
                cornerColors[(int)TileOffsetCorner.C4_SW]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SE]!=cornerColors[(int)TileOffsetCorner.C5_NW] && (
                cornerColors[(int)TileOffsetCorner.C0_SE]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C5_NW]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.SE_5NW))==1<<(int)BitMask.SE_5NW &&
                cornerColors[(int)TileOffsetCorner.C5_NW]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SE]!=cornerColors[(int)TileOffsetCorner.C6_NE] && (
                cornerColors[(int)TileOffsetCorner.C0_SE]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C6_NE]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.SE_6NE))==1<<(int)BitMask.SE_6NE &&
                cornerColors[(int)TileOffsetCorner.C6_NE]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SW]!=cornerColors[(int)TileOffsetCorner.C6_NW] && (
                cornerColors[(int)TileOffsetCorner.C0_SW]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C6_NW]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.SW_6NW))==1<<(int)BitMask.SW_6NW &&
                cornerColors[(int)TileOffsetCorner.C6_NW]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SW]!=cornerColors[(int)TileOffsetCorner.C7_NE] && (
                cornerColors[(int)TileOffsetCorner.C0_SW]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C7_NE]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.SW_7NE))==1<<(int)BitMask.SW_7NE &&
                cornerColors[(int)TileOffsetCorner.C7_NE]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SW]!=cornerColors[(int)TileOffsetCorner.C8_SE] && (
                cornerColors[(int)TileOffsetCorner.C0_SW]!=(int)CornerColor.WildCard &&
                cornerColors[(int)TileOffsetCorner.C8_SE]!=(int)CornerColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.SW_8SE))==1<<(int)BitMask.SW_8SE &&
                cornerColors[(int)TileOffsetCorner.C8_SE]==(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        int countMismatchVertical(VerticalColor[] vColors, int tileBitmask){
            int numberOfMismatch=0;

            if ((vColors[(int)TileOffsetVertical.V0_N]!=vColors[(int)TileOffsetVertical.V2_S] && (
                vColors[(int)TileOffsetVertical.V0_N]!=(int)VerticalColor.WildCard &&
                vColors[(int)TileOffsetVertical.V2_S]!=(int)VerticalColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.N_2S))==1<<(int)BitMask.N_2S &&
                vColors[(int)TileOffsetVertical.V2_S]==(int)VerticalColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((vColors[(int)TileOffsetVertical.V0_S]!=vColors[(int)TileOffsetVertical.V6_N] && (
                vColors[(int)TileOffsetVertical.V0_S]!=(int)VerticalColor.WildCard &&
                vColors[(int)TileOffsetVertical.V6_N]!=(int)VerticalColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.S_6N))==1<<(int)BitMask.S_6N &&
                vColors[(int)TileOffsetVertical.V6_N]==(int)VerticalColor.WildCard)) {
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

            int countMismatchHorizontal(HorizontalColor[] hColors, int tileBitmask){
            int numberOfMismatch=0;

            if ((hColors[(int)TileOffsetHorizontal.H0_W]!=hColors[(int)TileOffsetHorizontal.H8_E] && (
                hColors[(int)TileOffsetHorizontal.H0_W]!=(int)HorizontalColor.WildCard &&
                hColors[(int)TileOffsetHorizontal.H8_E]!=(int)HorizontalColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.W_8E))==1<<(int)BitMask.W_8E &&
                hColors[(int)TileOffsetHorizontal.H8_E]==(int)HorizontalColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((hColors[(int)TileOffsetHorizontal.H0_E]!=hColors[(int)TileOffsetHorizontal.H4_W] && (
                hColors[(int)TileOffsetHorizontal.H0_E]!=(int)HorizontalColor.WildCard &&
                hColors[(int)TileOffsetHorizontal.H4_W]!=(int)HorizontalColor.WildCard)) || 
                !((tileBitmask&(1<<(int)BitMask.E_4W))==1<<(int)BitMask.E_4W &&
                hColors[(int)TileOffsetHorizontal.H4_W]==(int)HorizontalColor.WildCard)) {
                numberOfMismatch++;
            }
            
            return numberOfMismatch;
        }

        public (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) GetTileAdjacentColorValues(bool useBitmasking, int col, int row) {
            CornerColor[] cornerColorValues = new CornerColor[16];
            HorizontalColor[] horizontalColorValues = new HorizontalColor[4];
            VerticalColor[] verticalColorValues = new VerticalColor[4];

            WangTile currentTile = getTile(col,row);
            WangTile northWestTile = getNorthWestTile(col,row);
            WangTile northTile = getNorthTile(col,row);
            WangTile northEastTile = getNorthEastTile(col,row);
            WangTile eastTile = getEastTile(col,row);
            WangTile southEastTile = getSouthEastTile(col,row);
            WangTile southTile = getSouthTile(col,row);
            WangTile southWestTile = getSouthWestTile(col,row);
            WangTile westTile = getWestTile(col,row);
            for (int i=0;i<=(int)TileOffsetCorner.C8_SE;i++){
                cornerColorValues[i]= CornerColor.WildCard;
            }

            verticalColorValues[(int)TileOffsetVertical.V0_N] = VerticalColor.WildCard;
            verticalColorValues[(int)TileOffsetVertical.V2_S] = VerticalColor.WildCard;
            verticalColorValues[(int)TileOffsetVertical.V0_S] = VerticalColor.WildCard;
            verticalColorValues[(int)TileOffsetVertical.V6_N] = VerticalColor.WildCard;

            horizontalColorValues[(int)TileOffsetHorizontal.H0_E] = HorizontalColor.WildCard;
            horizontalColorValues[(int)TileOffsetHorizontal.H4_W] = HorizontalColor.WildCard;
            horizontalColorValues[(int)TileOffsetHorizontal.H0_W] = HorizontalColor.WildCard;
            horizontalColorValues[(int)TileOffsetHorizontal.H8_E] = HorizontalColor.WildCard; 

            // ///////
            cornerColorValues[(int)TileOffsetCorner.C0_NW]= currentTile.CornerColorNW;

            if ((westTile.TileBitMask&(1<<(int)BitMask.NE_4NW))!=((1<<(int)BitMask.NE_4NW))){
                cornerColorValues[(int)TileOffsetCorner.C8_NE]= westTile.CornerColorNE;
            }
            if ((northWestTile.TileBitMask&(1<<(int)BitMask.SE_5NW))!=((1<<(int)BitMask.SE_5NW))){
                cornerColorValues[(int)TileOffsetCorner.C1_SE]= northWestTile.CornerColorSE;
            }
            if ((northTile.TileBitMask&(1<<(int)BitMask.SW_6NW))!=((1<<(int)BitMask.SW_6NW))){
                cornerColorValues[(int)TileOffsetCorner.C2_SW]= northTile.CornerColorSW;
            }

            verticalColorValues[(int)TileOffsetVertical.V0_N]=currentTile.EdgeColorNorth;

            if ((northTile.TileBitMask&(1<<(int)BitMask.S_6N))!=((1<<(int)BitMask.S_6N))){
                verticalColorValues[(int)TileOffsetVertical.V2_S]=northTile.EdgeColorSouth;
            }


            cornerColorValues[(int)TileOffsetCorner.C0_NE]= currentTile.CornerColorNE;

            if ((northTile.TileBitMask&(1<<(int)BitMask.SE_6NE))!=((1<<(int)BitMask.SE_6NE))){
                cornerColorValues[(int)TileOffsetCorner.C2_SE]= northTile.CornerColorSE;
            }
            if ((northEastTile.TileBitMask&(1<<(int)BitMask.SW_7NE))!=((1<<(int)BitMask.SW_7NE))){
                cornerColorValues[(int)TileOffsetCorner.C3_SW]= northEastTile.CornerColorSW;
            }
            if ((eastTile.TileBitMask&(1<<(int)BitMask.NW_8NE))!=((1<<(int)BitMask.NW_8NE))){
                cornerColorValues[(int)TileOffsetCorner.C4_NW]= eastTile.CornerColorNW;
            }

            horizontalColorValues[(int)TileOffsetHorizontal.H0_E]=currentTile.EdgeColorEast;

            if ((eastTile.TileBitMask&(1<<(int)BitMask.W_8E))!=((1<<(int)BitMask.W_8E))){
                horizontalColorValues[(int)TileOffsetHorizontal.H4_W]=eastTile.EdgeColorWest;
            }


            cornerColorValues[(int)TileOffsetCorner.C0_SE]= currentTile.CornerColorSE;

            if ((eastTile.TileBitMask&(1<<(int)BitMask.SW_8SE))!=((1<<(int)BitMask.SW_8SE))){
                cornerColorValues[(int)TileOffsetCorner.C4_SW]= eastTile.CornerColorSW;
            }
            if ((southEastTile.TileBitMask&(1<<(int)BitMask.NW_1SE))!=((1<<(int)BitMask.NW_1SE))){
                cornerColorValues[(int)TileOffsetCorner.C5_NW]= southEastTile.CornerColorNW;
            }
            if ((southTile.TileBitMask&(1<<(int)BitMask.NE_2SE))!=((1<<(int)BitMask.NE_2SE))){
                cornerColorValues[(int)TileOffsetCorner.C6_NE]= southTile.CornerColorNE;
            }

            verticalColorValues[(int)TileOffsetVertical.V0_S]=currentTile.EdgeColorSouth;

            if ((southTile.TileBitMask&(1<<(int)BitMask.N_2S))!=((1<<(int)BitMask.N_2S))){
                verticalColorValues[(int)TileOffsetVertical.V6_N]=southTile.EdgeColorNorth;
            }

            cornerColorValues[(int)TileOffsetCorner.C0_SW]= currentTile.CornerColorSW;

            if ((southTile.TileBitMask&(1<<(int)BitMask.NW_2SW))!=((1<<(int)BitMask.NW_2SW))){
                cornerColorValues[(int)TileOffsetCorner.C6_NW]= southTile.CornerColorNW;
            }
            if ((southWestTile.TileBitMask&(1<<(int)BitMask.NE_3SW))!=((1<<(int)BitMask.NE_3SW))){
                cornerColorValues[(int)TileOffsetCorner.C7_NE]= southWestTile.CornerColorNE;
            }
            if ((westTile.TileBitMask&(1<<(int)BitMask.SE_4SW))!=((1<<(int)BitMask.SE_4SW))){
                cornerColorValues[(int)TileOffsetCorner.C8_SE]= westTile.CornerColorSE;
            }

            horizontalColorValues[(int)TileOffsetHorizontal.H0_W]=currentTile.EdgeColorWest;

            if ((westTile.TileBitMask&(1<<(int)BitMask.E_4W))!=((1<<(int)BitMask.E_4W))){
                horizontalColorValues[(int)TileOffsetHorizontal.H8_E]=westTile.EdgeColorEast;
            }

            // cornerColorValues = applyBitmaskingForCornerColor(currentTile.TileBitMask, cornerColorValues);
            // verticalColorValues = applyBitmaskingForVerticalColor(currentTile.TileBitMask, verticalColorValues);
            // horizontalColorValues = applyBitmaskingForHorizontalColor(currentTile.TileBitMask, horizontalColorValues);

            return (cColors:cornerColorValues,hColors:horizontalColorValues,vColors:verticalColorValues);
        }

        CornerColor[] applyBitmaskingForCornerColor(int tileBitmask,  CornerColor[] cColors){
            if ((tileBitmask&(1<<(int)BitMask.NW_8NE))==((1<<(int)BitMask.NW_8NE))){
                cColors[(int)TileOffsetCorner.C8_NE]=CornerColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.NW_1SE))==((1<<(int)BitMask.NW_1SE))){
                cColors[(int)TileOffsetCorner.C1_SE]= CornerColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.NW_2SW))==((1<<(int)BitMask.NW_2SW))){
                cColors[(int)TileOffsetCorner.C2_SW]= CornerColor.WildCard;
            }


            if ((tileBitmask&(1<<(int)BitMask.NE_2SE))==((1<<(int)BitMask.NE_2SE))){
                cColors[(int)TileOffsetCorner.C2_SE]= CornerColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.NE_3SW))==((1<<(int)BitMask.NE_3SW))){
                 cColors[(int)TileOffsetCorner.C3_SW]= CornerColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.NE_4NW))==((1<<(int)BitMask.NE_4NW))){
                 cColors[(int)TileOffsetCorner.C4_NW]= CornerColor.WildCard;
            }


            if ((tileBitmask&(1<<(int)BitMask.SE_4SW))==((1<<(int)BitMask.SE_4SW))){
                cColors[(int)TileOffsetCorner.C4_SW]= CornerColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.SE_5NW))==((1<<(int)BitMask.SE_5NW))){
                cColors[(int)TileOffsetCorner.C5_NW]= CornerColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.SE_6NE))==((1<<(int)BitMask.SE_6NE))){
                cColors[(int)TileOffsetCorner.C6_NE]= CornerColor.WildCard;
            }


            if ((tileBitmask&(1<<(int)BitMask.SW_6NW))==((1<<(int)BitMask.SW_6NW))){
                cColors[(int)TileOffsetCorner.C6_NW]= CornerColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.SW_7NE))==((1<<(int)BitMask.SW_7NE))){
                cColors[(int)TileOffsetCorner.C7_NE]= CornerColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.SW_8SE))==((1<<(int)BitMask.SW_8SE))){
                cColors[(int)TileOffsetCorner.C8_SE]= CornerColor.WildCard;
            }

            return cColors;
        }

        VerticalColor[] applyBitmaskingForVerticalColor(int tileBitmask,  VerticalColor[] vColors){
            if ((tileBitmask&(1<<(int)BitMask.N_2S))==((1<<(int)BitMask.N_2S))){
                vColors[(int)TileOffsetVertical.V2_S]=VerticalColor.WildCard;
            }
            if ((tileBitmask&(1<<(int)BitMask.S_6N))==((1<<(int)BitMask.S_6N))){
                vColors[(int)TileOffsetVertical.V6_N]=VerticalColor.WildCard;

            }
  
            return vColors;
        }

        HorizontalColor[] applyBitmaskingForHorizontalColor(int tileBitmask,  HorizontalColor[] hColors){
            if ((tileBitmask&(1<<(int)BitMask.E_4W))==((1<<(int)BitMask.E_4W))){
                hColors[(int)TileOffsetHorizontal.H4_W]=HorizontalColor.WildCard;

            }
            if ((tileBitmask&(1<<(int)BitMask.W_8E))==((1<<(int)BitMask.W_8E))){
                hColors[(int)TileOffsetHorizontal.H8_E]=HorizontalColor.WildCard;
            }
  
            return hColors;
        }

        (int col,int row) getNorthCoordinates(int col,int row){
            return (col:col-1,row:row);
        }

        (int col,int row) getEastCoordinates(int col,int row){
            return (col:col,row:row+1);
        }

        (int col,int row) getSouthCoordinates(int col,int row){
            return (col:col+1,row:row);
        }

         (int col,int row) getWestCoordinates(int col,int row){
            return (col:col,row:row-1);
        }

         WangTile getTile(int col, int row){
            if (isValidPosition(col,row) && isTileAlreadyExist(col,row)){
                int index = Utils.GetBoardSlotIndex(this.Width,col,row);

                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        WangTile getNorthWestTile(int col, int row){
            (int col, int row) coord = getNorthCoordinates(col,row);
            coord = getWestCoordinates(coord.col,coord.row);

            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        WangTile getNorthTile(int col, int row){
            (int col, int row) coord = getNorthCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        WangTile getNorthEastTile(int col, int row){
            (int col, int row) coord = getNorthCoordinates(col,row);
            coord = getEastCoordinates(coord.col,coord.row);
            
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        WangTile getEastTile(int col, int row){
            (int col, int row) coord = getEastCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        WangTile getSouthEastTile(int col, int row){
            (int col, int row) coord = getSouthCoordinates(col,row);
            coord = getEastCoordinates(coord.col,coord.row);
            
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        WangTile getSouthTile(int col, int row){
            (int col, int row) coord = getSouthCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        WangTile getSouthWestTile(int col, int row){
            (int col, int row) coord = getSouthCoordinates(col,row);
            coord = getWestCoordinates(coord.col,coord.row);
            
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        WangTile getWestTile(int col, int row){
            (int col, int row) coord = getWestCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
        }

        bool isValidPosition(int col, int row){
            if (col >=0 && col<this.Height && row>=0 && row<this.Width){
                return true;
            }

            return false;
        }

         bool isTileAlreadyExist(int col, int row){
            int index = Utils.GetBoardSlotIndex(this.Width,col,row);
            
            if  ((this.TileSlots[index].TileID==null) && (this.TileSlots[index].TileSetID==null)){
                return false;
            }
            
            return true;
        }
     
    }
}