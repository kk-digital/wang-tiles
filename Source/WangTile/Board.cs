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

        public int[] GetTileMismatchArray(int tileSetID,int col, int row, bool useBitmasking, ColorMatching colorMatching){
            WangTile[] tiles = this.TileSet[tileSetID].Tiles;
            int[] tilesMismatches= new int[tiles.Length];


            for (int i=0;i<tiles.Length;i++){
                int numberOfMismatch = 0;
                int tileID = tiles[i].TileID;
                this.PlaceTile(tileSetID,tileID,col,row);

                (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = GetTileAdjacentColorValues(useBitmasking,colorMatching,col,row);

                
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

        public void RemoveTilesWithMismatches(bool useBitmasking, ColorMatching colorMatching){
            (int col, int row) pos = (0,0);

            while (true){
                int numberOfMismatch = 0;
                WangTile tile = this.getTile(pos.col,pos.row);
                (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = GetTileAdjacentColorValues(useBitmasking,colorMatching,pos.col,pos.row);

                
                numberOfMismatch=countMismatchOnCorners(cColors,tile.TileBitMask);
                numberOfMismatch+=countMismatchVertical(vColors,tile.TileBitMask);
                numberOfMismatch+=countMismatchHorizontal(hColors,tile.TileBitMask);

                if (numberOfMismatch>0){
                    this.RemoveTile(pos.col,pos.row);
                }

                pos = GetNextTileSlot(pos.col,pos.row);
                
                if (pos.col==this.Height){
                    break;
                }
            }
        }

        public (int col, int row) GetNextTileSlot(int col, int row){
            if (row==this.Width-1){
                return (col:col+1,row:0);
            } else {
                return (col:col, row:row+1);
            }
        }

        int countMismatchOnCorners(CornerColor[] cornerColors, int tileBitmask){
            int numberOfMismatch=0;

            if ((cornerColors[(int)TileOffsetCorner.C0_NW]!=cornerColors[(int)TileOffsetCorner.C8_NE] &&
                (cornerColors[(int)TileOffsetCorner.C0_NW]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C8_NE]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.NW_8NE))==1<<(int)BitMask.NW_8NE &&
                cornerColors[(int)TileOffsetCorner.C8_NE]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NW]!=cornerColors[(int)TileOffsetCorner.C1_SE] &&
                (cornerColors[(int)TileOffsetCorner.C0_NW]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C1_SE]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.NW_1SE))==1<<(int)BitMask.NW_1SE &&
                cornerColors[(int)TileOffsetCorner.C1_SE]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NW]!=cornerColors[(int)TileOffsetCorner.C2_SW] &&
                (cornerColors[(int)TileOffsetCorner.C0_NW]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C2_SW]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.NW_2SW))==1<<(int)BitMask.NW_2SW &&
                cornerColors[(int)TileOffsetCorner.C2_SW]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NE]!=cornerColors[(int)TileOffsetCorner.C2_SE] &&
                (cornerColors[(int)TileOffsetCorner.C0_NE]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C2_SE]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.NE_2SE))==1<<(int)BitMask.NE_2SE &&
                cornerColors[(int)TileOffsetCorner.C2_SE]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NE]!=cornerColors[(int)TileOffsetCorner.C3_SW] &&
                (cornerColors[(int)TileOffsetCorner.C0_NE]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C3_SW]!=CornerColor.NoColor)) &&
                !((tileBitmask&(1<<(int)BitMask.NE_3SW))==1<<(int)BitMask.NE_3SW &&
                cornerColors[(int)TileOffsetCorner.C3_SW]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_NE]!=cornerColors[(int)TileOffsetCorner.C4_NW] &&
                (cornerColors[(int)TileOffsetCorner.C0_NE]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C4_NW]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.NE_4NW))==1<<(int)BitMask.NE_4NW &&
                cornerColors[(int)TileOffsetCorner.C4_NW]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SE]!=cornerColors[(int)TileOffsetCorner.C4_SW] &&
                (cornerColors[(int)TileOffsetCorner.C0_SE]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C4_SW]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.SE_4SW))==1<<(int)BitMask.SE_4SW &&
                cornerColors[(int)TileOffsetCorner.C4_SW]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SE]!=cornerColors[(int)TileOffsetCorner.C5_NW] &&
                (cornerColors[(int)TileOffsetCorner.C0_SE]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C5_NW]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.SE_5NW))==1<<(int)BitMask.SE_5NW &&
                cornerColors[(int)TileOffsetCorner.C5_NW]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SE]!=cornerColors[(int)TileOffsetCorner.C6_NE] &&
                (cornerColors[(int)TileOffsetCorner.C0_SE]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C6_NE]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.SE_6NE))==1<<(int)BitMask.SE_6NE &&
                cornerColors[(int)TileOffsetCorner.C6_NE]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SW]!=cornerColors[(int)TileOffsetCorner.C6_NW] &&
                (cornerColors[(int)TileOffsetCorner.C0_SW]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C6_NW]!=CornerColor.NoColor)) &&
                !((tileBitmask&(1<<(int)BitMask.SW_6NW))==1<<(int)BitMask.SW_6NW &&
                cornerColors[(int)TileOffsetCorner.C6_NW]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SW]!=cornerColors[(int)TileOffsetCorner.C7_NE] &&
                (cornerColors[(int)TileOffsetCorner.C0_SW]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C7_NE]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.SW_7NE))==1<<(int)BitMask.SW_7NE &&
                cornerColors[(int)TileOffsetCorner.C7_NE]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if ((cornerColors[(int)TileOffsetCorner.C0_SW]!=cornerColors[(int)TileOffsetCorner.C8_SE] &&
                (cornerColors[(int)TileOffsetCorner.C0_SW]!=CornerColor.NoColor &&
                cornerColors[(int)TileOffsetCorner.C8_SE]!=CornerColor.NoColor)) && 
                !((tileBitmask&(1<<(int)BitMask.SW_8SE))==1<<(int)BitMask.SW_8SE &&
                cornerColors[(int)TileOffsetCorner.C8_SE]==CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        int countMismatchVertical(VerticalColor[] vColors, int tileBitmask){
            int numberOfMismatch=0;

            if (vColors[(int)TileOffsetVertical.V0_N]!=vColors[(int)TileOffsetVertical.V2_S] &&
                (vColors[(int)TileOffsetVertical.V0_N]!=VerticalColor.NoColor &&
                vColors[(int)TileOffsetVertical.V2_S]!=VerticalColor.NoColor) && 
                !((tileBitmask&(1<<(int)BitMask.N_2S))==1<<(int)BitMask.N_2S &&
                vColors[(int)TileOffsetVertical.V2_S]==VerticalColor.WildCard)) {
                numberOfMismatch++;
            }
  
            if (vColors[(int)TileOffsetVertical.V0_S]!=vColors[(int)TileOffsetVertical.V6_N] &&
                (vColors[(int)TileOffsetVertical.V0_S]!=VerticalColor.NoColor &&
                vColors[(int)TileOffsetVertical.V6_N]!=VerticalColor.NoColor) && 
                !((tileBitmask&(1<<(int)BitMask.S_6N))==1<<(int)BitMask.S_6N &&
                vColors[(int)TileOffsetVertical.V6_N]==VerticalColor.WildCard)) {
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

            int countMismatchHorizontal(HorizontalColor[] hColors, int tileBitmask){
            int numberOfMismatch=0;
        
            if (hColors[(int)TileOffsetHorizontal.H0_W]!=hColors[(int)TileOffsetHorizontal.H8_E] &&
                (hColors[(int)TileOffsetHorizontal.H0_W]!=HorizontalColor.NoColor &&
                hColors[(int)TileOffsetHorizontal.H8_E]!=HorizontalColor.NoColor) && 
                !((tileBitmask&(1<<(int)BitMask.W_8E))==1<<(int)BitMask.W_8E &&
                hColors[(int)TileOffsetHorizontal.H8_E]==HorizontalColor.WildCard)) {
                numberOfMismatch++;
            }

            if (hColors[(int)TileOffsetHorizontal.H0_E]!=hColors[(int)TileOffsetHorizontal.H4_W] &&
                (hColors[(int)TileOffsetHorizontal.H0_E]!=HorizontalColor.NoColor &&
                hColors[(int)TileOffsetHorizontal.H4_W]!=HorizontalColor.NoColor) && 
                !((tileBitmask&(1<<(int)BitMask.E_4W))==1<<(int)BitMask.E_4W &&
                hColors[(int)TileOffsetHorizontal.H4_W]==HorizontalColor.WildCard)) {
                numberOfMismatch++;
            }
            
            return numberOfMismatch;
        }

        public (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) GetTileAdjacentColorValues(bool useBitmasking, ColorMatching colorMatching, int col, int row) {
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

            cornerColorValues[(int)TileOffsetCorner.C0_NW]= currentTile.CornerColorNW;
            cornerColorValues[(int)TileOffsetCorner.C8_NE]= westTile.CornerColorNE;
            cornerColorValues[(int)TileOffsetCorner.C1_SE]= northWestTile.CornerColorSE;
            cornerColorValues[(int)TileOffsetCorner.C2_SW]= northTile.CornerColorSW;

            verticalColorValues[(int)TileOffsetVertical.V0_N]=currentTile.EdgeColorNorth;
            verticalColorValues[(int)TileOffsetVertical.V2_S]=northTile.EdgeColorSouth;

            cornerColorValues[(int)TileOffsetCorner.C0_NE]= currentTile.CornerColorNE;
            cornerColorValues[(int)TileOffsetCorner.C2_SE]= northTile.CornerColorSE;
            cornerColorValues[(int)TileOffsetCorner.C3_SW]= northEastTile.CornerColorSW;
            cornerColorValues[(int)TileOffsetCorner.C4_NW]= eastTile.CornerColorNW;

            horizontalColorValues[(int)TileOffsetHorizontal.H0_E]=currentTile.EdgeColorEast;
            horizontalColorValues[(int)TileOffsetHorizontal.H4_W]=eastTile.EdgeColorWest;

            cornerColorValues[(int)TileOffsetCorner.C0_SE]= currentTile.CornerColorSE;
            cornerColorValues[(int)TileOffsetCorner.C4_SW]= eastTile.CornerColorSW;
            cornerColorValues[(int)TileOffsetCorner.C5_NW]= southEastTile.CornerColorNW;
            cornerColorValues[(int)TileOffsetCorner.C6_NE]= southTile.CornerColorNE;

            verticalColorValues[(int)TileOffsetVertical.V0_S]=currentTile.EdgeColorSouth;
            verticalColorValues[(int)TileOffsetVertical.V6_N]=southTile.EdgeColorNorth;

            cornerColorValues[(int)TileOffsetCorner.C0_SW] = currentTile.CornerColorSW;
            cornerColorValues[(int)TileOffsetCorner.C6_NW] = southTile.CornerColorNW;
            cornerColorValues[(int)TileOffsetCorner.C7_NE] = southWestTile.CornerColorNE;
            cornerColorValues[(int)TileOffsetCorner.C8_SE] = westTile.CornerColorSE;

            horizontalColorValues[(int)TileOffsetHorizontal.H0_W] = currentTile.EdgeColorWest;
            horizontalColorValues[(int)TileOffsetHorizontal.H8_E] = westTile.EdgeColorEast;

            // NW corner
            if (((westTile.TileBitMask&(1<<(int)BitMask.NE_4NW))==((1<<(int)BitMask.NE_4NW))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C8_NE]= CornerColor.WildCard;
            }
            if (((northWestTile.TileBitMask&(1<<(int)BitMask.SE_5NW))==((1<<(int)BitMask.SE_5NW))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C1_SE]= CornerColor.WildCard;
            }
            if (((northTile.TileBitMask&(1<<(int)BitMask.SW_6NW))==((1<<(int)BitMask.SW_6NW))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C2_SW]= CornerColor.WildCard;
            }

            // NE corner
            if (((northTile.TileBitMask&(1<<(int)BitMask.SE_6NE))==((1<<(int)BitMask.SE_6NE))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C2_SE]= CornerColor.WildCard;
            }
            if (((northEastTile.TileBitMask&(1<<(int)BitMask.SW_7NE))==((1<<(int)BitMask.SW_7NE))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C3_SW]= CornerColor.WildCard;
            }
            if (((eastTile.TileBitMask&(1<<(int)BitMask.NW_8NE))==((1<<(int)BitMask.NW_8NE))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C4_NW]= CornerColor.WildCard;
            }

            // SE corner
            if (((eastTile.TileBitMask&(1<<(int)BitMask.SW_8SE))==((1<<(int)BitMask.SW_8SE))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C4_SW] = CornerColor.WildCard;
            }
            if (((southEastTile.TileBitMask&(1<<(int)BitMask.NW_1SE))==((1<<(int)BitMask.NW_1SE))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C5_NW] = CornerColor.WildCard;
            }
            if (((southTile.TileBitMask&(1<<(int)BitMask.NE_2SE))==((1<<(int)BitMask.NE_2SE))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C6_NE] = CornerColor.WildCard;
            }

            // SW corner
            if (((southTile.TileBitMask&(1<<(int)BitMask.NW_2SW))==((1<<(int)BitMask.NW_2SW))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C6_NW] = CornerColor.WildCard;
            }
            if (((southWestTile.TileBitMask&(1<<(int)BitMask.NE_3SW))==((1<<(int)BitMask.NE_3SW))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C7_NE] = CornerColor.WildCard;
            }
            if (((westTile.TileBitMask&(1<<(int)BitMask.SE_4SW))==((1<<(int)BitMask.SE_4SW))) && colorMatching==ColorMatching.CurrentTileBitmask){
                cornerColorValues[(int)TileOffsetCorner.C8_SE] = CornerColor.WildCard;
            }

            
            // N, E, S, W
            if (((((northTile.TileBitMask&(1<<(int)BitMask.S_6N))==((1<<(int)BitMask.S_6N))) && 
            colorMatching==ColorMatching.CurrentTileBitmask)) ||
            ((northTile.TileBitMask&(1<<(int)BitMask.S_6N))==((1<<(int)BitMask.S_6N)) && 
            (currentTile.TileBitMask&(1<<(int)BitMask.N_2S))==((1<<(int)BitMask.N_2S))  && 
            colorMatching==ColorMatching.SymmetricalMatching)){
                verticalColorValues[(int)TileOffsetVertical.V2_S]=VerticalColor.WildCard;
            }

            if (((((eastTile.TileBitMask&(1<<(int)BitMask.W_8E))==((1<<(int)BitMask.W_8E))) && 
            colorMatching==ColorMatching.CurrentTileBitmask)) ||
            ((eastTile.TileBitMask&(1<<(int)BitMask.W_8E))==((1<<(int)BitMask.W_8E)) && 
            (currentTile.TileBitMask&(1<<(int)BitMask.E_4W))==((1<<(int)BitMask.E_4W))  && 
            colorMatching==ColorMatching.SymmetricalMatching)){
                horizontalColorValues[(int)TileOffsetHorizontal.H4_W] = HorizontalColor.WildCard;
            }

            if (((((southTile.TileBitMask&(1<<(int)BitMask.N_2S))==((1<<(int)BitMask.N_2S))) && 
            colorMatching==ColorMatching.CurrentTileBitmask)) ||
            ((southTile.TileBitMask&(1<<(int)BitMask.N_2S))==((1<<(int)BitMask.N_2S)) && 
            (currentTile.TileBitMask&(1<<(int)BitMask.S_6N))==((1<<(int)BitMask.S_6N))  && 
            colorMatching==ColorMatching.SymmetricalMatching)){
                verticalColorValues[(int)TileOffsetVertical.V6_N] = VerticalColor.WildCard;
            }

            if (((((westTile.TileBitMask&(1<<(int)BitMask.E_4W))==((1<<(int)BitMask.E_4W))) && 
            colorMatching==ColorMatching.CurrentTileBitmask)) ||
            ((westTile.TileBitMask&(1<<(int)BitMask.E_4W))==((1<<(int)BitMask.E_4W)) && 
            (currentTile.TileBitMask&(1<<(int)BitMask.W_8E))==((1<<(int)BitMask.W_8E))  && 
            colorMatching==ColorMatching.SymmetricalMatching)){
                horizontalColorValues[(int)TileOffsetHorizontal.H8_E] = HorizontalColor.WildCard;
            }

            return (cColors:cornerColorValues,hColors:horizontalColorValues,vColors:verticalColorValues);
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

            // out of bounds tile
            if (!isValidPosition(col,row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }

            // empty tile
            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
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

            // out of bounds tile
            if (!isValidPosition(coord.col,coord.row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }

            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
        }

        WangTile getNorthTile(int col, int row){
            (int col, int row) coord = getNorthCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            // out of bounds tile
            if (!isValidPosition(coord.col,coord.row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }
        
            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
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

            // out of bounds tile
            if (!isValidPosition(coord.col,coord.row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }

            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
        }

        WangTile getEastTile(int col, int row){
            (int col, int row) coord = getEastCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            // out of bounds tile
            if (!isValidPosition(coord.col,coord.row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }

            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
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

            // out of bounds tile
            if (!isValidPosition(coord.col,coord.row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }

            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
        }

        WangTile getSouthTile(int col, int row){
            (int col, int row) coord = getSouthCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            // out of bounds tile
            if (!isValidPosition(coord.col,coord.row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }

            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
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

            // out of bounds tile
            if (!isValidPosition(coord.col,coord.row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }

            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
        }

        WangTile getWestTile(int col, int row){
            (int col, int row) coord = getWestCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            // out of bounds tile
            if (!isValidPosition(coord.col,coord.row)){
                return new WangTile(CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,CornerColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard,VerticalColor.WildCard,HorizontalColor.WildCard);
            }

            return new WangTile(CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,CornerColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor,VerticalColor.NoColor,HorizontalColor.NoColor);
        }

        public (int col, int row) FindRandomPositionWithAdjacentTilesOnEdges(){
            while (true){
                (int col, int row) pos = Utils.GetRandomPosition(this.Width,this.Height);
                if (IsThereAnAdjacentTileOnEdges(pos.col,pos.row)){
                    return (col:pos.col,row:pos.row);
                }
            }
        }

        bool IsThereAnAdjacentTileOnEdges(int col, int row){
            (int col, int row) northCoor = getNorthCoordinates(col,row);
            (int col, int row) eastCoor = getEastCoordinates(col,row);
            (int col, int row) southCoor = getSouthCoordinates(col,row);
            (int col, int row) westCoor = getWestCoordinates(col,row);

            if (isValidPosition(northCoor.col,northCoor.row) && isTileAlreadyExist(northCoor.col,northCoor.row)){
                return true;
            }

            if (isValidPosition(eastCoor.col,eastCoor.row) && isTileAlreadyExist(eastCoor.col,eastCoor.row)){
                return true;
            }

            if (isValidPosition(southCoor.col,southCoor.row) && isTileAlreadyExist(southCoor.col,southCoor.row)){
                return true;
            }

            if (isValidPosition(westCoor.col,westCoor.row) && isTileAlreadyExist(westCoor.col,westCoor.row)){
                return true;
            }

            return false;
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