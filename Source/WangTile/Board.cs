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

            bool wasThereMismatch = false;
            while (true){
                int numberOfMismatch = 0;
                WangTile tile = this.getTile(pos.col,pos.row);
                (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = GetTileAdjacentColorValues(useBitmasking,colorMatching,pos.col,pos.row);

                
                numberOfMismatch=countMismatchOnCorners(cColors,tile.TileBitMask);
                numberOfMismatch+=countMismatchVertical(vColors,tile.TileBitMask);
                numberOfMismatch+=countMismatchHorizontal(hColors,tile.TileBitMask);

                if (numberOfMismatch>0){
                    wasThereMismatch = true;
                    this.RemoveTile(pos.col,pos.row);
                    Console.WriteLine($"Col={pos.col},Row={pos.row}, mismatches={numberOfMismatch}");
                }

                pos = GetNextTileSlot(pos.col,pos.row);
                
                if (pos.col==this.Height){
                    if (!wasThereMismatch){
                        break;
                    }
                    wasThereMismatch=false;

                    pos.col=0;
                    pos.row=0;
                    Console.WriteLine("-----------------|");
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
            
            // NW corner
            CornerColor cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_8NE)) == (1<<(int)BitMask.NW_8NE)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            if ((cornerColorC0_NW!=cornerColors[(int)TileOffsetCorner.C8_NE] && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C8_NE]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C8_NE]==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            )) {
                numberOfMismatch++;
            }

            cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_1SE)) == (1<<(int)BitMask.NW_1SE)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            if ((cornerColorC0_NW!=cornerColors[(int)TileOffsetCorner.C1_SE] && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C1_SE]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C1_SE]==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            )) {
                numberOfMismatch++;
            }

            cornerColorC0_NW=cornerColors[(int)TileOffsetCorner.C0_NW];
            if ((tileBitmask&(1<<(int)BitMask.NW_2SW)) == (1<<(int)BitMask.NW_2SW)){
                cornerColorC0_NW=CornerColor.WildCard;
            }
            if ((cornerColorC0_NW!=cornerColors[(int)TileOffsetCorner.C2_SW] && 
            !((cornerColorC0_NW==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C2_SW]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C2_SW]==CornerColor.NoColor && cornerColorC0_NW==CornerColor.WildCard))
            )) {
                numberOfMismatch++;
            }

            // NE Corner
            CornerColor cornerColorC0_NE=cornerColors[(int)TileOffsetCorner.C0_NE];
            if ((tileBitmask&(1<<(int)BitMask.NE_2SE)) == (1<<(int)BitMask.NE_2SE)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            if ((cornerColorC0_NE!=cornerColors[(int)TileOffsetCorner.C2_SE] && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C2_SE]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C2_SE]==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.NE_3SW)) == (1<<(int)BitMask.NE_3SW)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            if ((cornerColorC0_NE!=cornerColors[(int)TileOffsetCorner.C3_SW] && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C3_SW]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C3_SW]==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.NE_4NW)) == (1<<(int)BitMask.NE_4NW)){
                cornerColorC0_NE=CornerColor.WildCard;
            }
            if ((cornerColorC0_NE!=cornerColors[(int)TileOffsetCorner.C4_NW] && 
            !((cornerColorC0_NE==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C4_NW]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C4_NW]==CornerColor.NoColor && cornerColorC0_NE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            // SE corner
            CornerColor cornerColorC0_SE=cornerColors[(int)TileOffsetCorner.C0_SE];

            if ((tileBitmask&(1<<(int)BitMask.SE_4SW)) == (1<<(int)BitMask.SE_4SW)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            if ((cornerColorC0_SE!=cornerColors[(int)TileOffsetCorner.C4_SW] && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C4_SW]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C4_SW]==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SE_5NW)) == (1<<(int)BitMask.SE_5NW)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            if ((cornerColorC0_SE!=cornerColors[(int)TileOffsetCorner.C5_NW] && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C5_NW]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C5_NW]==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SE_6NE)) == (1<<(int)BitMask.SE_6NE)){
                cornerColorC0_SE=CornerColor.WildCard;
            }
            if ((cornerColorC0_SE!=cornerColors[(int)TileOffsetCorner.C6_NE] && 
            !((cornerColorC0_SE==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C6_NE]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C6_NE]==CornerColor.NoColor && cornerColorC0_SE==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            // SW Corner
            CornerColor cornerColorC0_SW=cornerColors[(int)TileOffsetCorner.C0_SW];

            if ((tileBitmask&(1<<(int)BitMask.SW_6NW)) == (1<<(int)BitMask.SW_6NW)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            if ((cornerColorC0_SW!=cornerColors[(int)TileOffsetCorner.C6_NW] && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C6_NW]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C6_NW]==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            if ((tileBitmask&(1<<(int)BitMask.SW_7NE)) == (1<<(int)BitMask.SW_7NE)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            if ((cornerColorC0_SW!=cornerColors[(int)TileOffsetCorner.C7_NE] && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C7_NE]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C7_NE]==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }
            
            if ((tileBitmask&(1<<(int)BitMask.SW_8SE)) == (1<<(int)BitMask.SW_8SE)){
                cornerColorC0_SW=CornerColor.WildCard;
            }
            if ((cornerColorC0_SW!=cornerColors[(int)TileOffsetCorner.C8_SE] && 
            !((cornerColorC0_SW==CornerColor.NoColor && cornerColors[(int)TileOffsetCorner.C8_SE]==CornerColor.WildCard) || 
            (cornerColors[(int)TileOffsetCorner.C8_SE]==CornerColor.NoColor && cornerColorC0_SW==CornerColor.WildCard)) 
            )) {
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        int countMismatchVertical(VerticalColor[] vColors, int tileBitmask){
            int numberOfMismatch=0;

            if (vColors[(int)TileOffsetVertical.V0_N]!=vColors[(int)TileOffsetVertical.V2_S] && 
            !((vColors[(int)TileOffsetVertical.V0_N]==VerticalColor.NoColor && vColors[(int)TileOffsetVertical.V2_S]==VerticalColor.WildCard) || 
            (vColors[(int)TileOffsetVertical.V2_S]==VerticalColor.NoColor && vColors[(int)TileOffsetVertical.V0_N]==VerticalColor.WildCard))
            ){
                Console.WriteLine($"V0_N={vColors[(int)TileOffsetVertical.V0_N]}, V2_S={vColors[(int)TileOffsetVertical.V2_S]}");
                numberOfMismatch++;
            }
  
            if (vColors[(int)TileOffsetVertical.V0_S]!=vColors[(int)TileOffsetVertical.V6_N] && 
            !((vColors[(int)TileOffsetVertical.V0_S]==VerticalColor.NoColor && vColors[(int)TileOffsetVertical.V6_N]==VerticalColor.WildCard) || 
            (vColors[(int)TileOffsetVertical.V6_N]==VerticalColor.NoColor && vColors[(int)TileOffsetVertical.V0_S]==VerticalColor.WildCard)) 
            ){
                Console.WriteLine($"V0_S={vColors[(int)TileOffsetVertical.V0_S]}, V6_N={vColors[(int)TileOffsetVertical.V6_N]}");
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

            int countMismatchHorizontal(HorizontalColor[] hColors, int tileBitmask){
            int numberOfMismatch=0;
        
            if (hColors[(int)TileOffsetHorizontal.H0_W]!=hColors[(int)TileOffsetHorizontal.H8_E] && 
            !((hColors[(int)TileOffsetHorizontal.H0_W]==HorizontalColor.NoColor && hColors[(int)TileOffsetHorizontal.H8_E]==HorizontalColor.WildCard) || 
            (hColors[(int)TileOffsetHorizontal.H8_E]==HorizontalColor.NoColor && hColors[(int)TileOffsetHorizontal.H0_W]==HorizontalColor.WildCard)) 
            ){
                Console.WriteLine($"H0_W={hColors[(int)TileOffsetHorizontal.H0_W]}, H8_E={hColors[(int)TileOffsetHorizontal.H8_E]}");
                numberOfMismatch++;
            }

            if (hColors[(int)TileOffsetHorizontal.H0_E]!=hColors[(int)TileOffsetHorizontal.H4_W]  && 
            !((hColors[(int)TileOffsetHorizontal.H0_E]==HorizontalColor.NoColor && hColors[(int)TileOffsetHorizontal.H4_W]==HorizontalColor.WildCard) || 
            (hColors[(int)TileOffsetHorizontal.H4_W]==HorizontalColor.NoColor && hColors[(int)TileOffsetHorizontal.H0_E]==HorizontalColor.WildCard)) 
            ){
                Console.WriteLine($"H0_E={hColors[(int)TileOffsetHorizontal.H0_E]}, H4_W={hColors[(int)TileOffsetHorizontal.H4_W]}");
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
            bool westTileNEBitmask = (westTile.TileBitMask&(1<<(int)BitMask.NE_4NW))==(1<<(int)BitMask.NE_4NW);
            if (((westTileNEBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((westTileNEBitmask || westTile.CornerColorNE==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.NW_8NE))==((1<<(int)BitMask.NW_8NE)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C8_NE]= CornerColor.WildCard;
            }

            bool northWestTileSEBitmask =(northWestTile.TileBitMask&(1<<(int)BitMask.SE_5NW))==(1<<(int)BitMask.SE_5NW);
            if (((northWestTileSEBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((northWestTileSEBitmask || northWestTile.CornerColorSE==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.NW_1SE))==((1<<(int)BitMask.NW_1SE)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C1_SE]= CornerColor.WildCard;
            }

            bool northTileSWBitmask = (northTile.TileBitMask&(1<<(int)BitMask.SW_6NW))==(1<<(int)BitMask.SW_6NW);
            if (((northTileSWBitmask) && colorMatching==ColorMatching.CurrentTileBitmask)||
            ((northTileSWBitmask || northTile.CornerColorSW==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.NW_2SW))==((1<<(int)BitMask.NW_2SW)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C2_SW]= CornerColor.WildCard;
            }

            // NE corner
            bool northTileSEBitmask = (northTile.TileBitMask&(1<<(int)BitMask.SE_6NE))==(1<<(int)BitMask.SE_6NE);
            if (((northTileSEBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((northTileSEBitmask || northTile.CornerColorSE==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.NE_2SE))==((1<<(int)BitMask.NE_2SE)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C2_SE]= CornerColor.WildCard;
            }

            bool northEastTileSWBitmask = (northEastTile.TileBitMask&(1<<(int)BitMask.SW_7NE))==(1<<(int)BitMask.SW_7NE);
            if (((northEastTileSWBitmask) && colorMatching==ColorMatching.CurrentTileBitmask)||
            ((northEastTileSWBitmask || northEastTile.CornerColorSW==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.NE_3SW))==((1<<(int)BitMask.NE_3SW)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C3_SW]= CornerColor.WildCard;
            }

            bool eastTileNWBitmask = (eastTile.TileBitMask&(1<<(int)BitMask.NW_8NE))==(1<<(int)BitMask.NW_8NE);
            if (((eastTileNWBitmask) && colorMatching==ColorMatching.CurrentTileBitmask)||
            ((eastTileNWBitmask || eastTile.CornerColorNW==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.NE_4NW))==((1<<(int)BitMask.NE_4NW)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C4_NW]= CornerColor.WildCard;
            }

            // SE corner
            bool eastTileSWBitmask = (eastTile.TileBitMask&(1<<(int)BitMask.SW_8SE))==(1<<(int)BitMask.SW_8SE);
            if (((eastTileSWBitmask) && colorMatching==ColorMatching.CurrentTileBitmask)||
            ((eastTileSWBitmask || eastTile.CornerColorSW==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.SE_4SW))==((1<<(int)BitMask.SE_4SW)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C4_SW] = CornerColor.WildCard;
            }

            bool southEastTileNWBitmask = (southEastTile.TileBitMask&(1<<(int)BitMask.NW_1SE))==(1<<(int)BitMask.NW_1SE);
            if (((southEastTileNWBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((southEastTileNWBitmask || southEastTile.CornerColorNW==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.SE_5NW))==((1<<(int)BitMask.SE_5NW)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C5_NW] = CornerColor.WildCard;
            }

            bool southTileNEBitmask = (southTile.TileBitMask&(1<<(int)BitMask.NE_2SE))==(1<<(int)BitMask.NE_2SE);
            if (((southTileNEBitmask) && colorMatching==ColorMatching.CurrentTileBitmask)||
            ((southTileNEBitmask || southTile.CornerColorNE==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.SE_6NE))==((1<<(int)BitMask.SE_6NE)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C6_NE] = CornerColor.WildCard;
            }

            // SW corner
            bool southTileNWBitmask = (southTile.TileBitMask&(1<<(int)BitMask.NW_2SW))==(1<<(int)BitMask.NW_2SW);
            if (((southTileNWBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((southTileNWBitmask || southTile.CornerColorNW==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.SW_6NW))==((1<<(int)BitMask.SW_6NW)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C6_NW] = CornerColor.WildCard;
            }

            bool southWestNEBitmask = (southWestTile.TileBitMask&(1<<(int)BitMask.NE_3SW))==(1<<(int)BitMask.NE_3SW);
            if (((southWestNEBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((southWestNEBitmask || southWestTile.CornerColorNE==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.SW_7NE))==((1<<(int)BitMask.SW_7NE)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C7_NE] = CornerColor.WildCard;
            }

            bool westTileSEBitmask = (westTile.TileBitMask&(1<<(int)BitMask.SE_4SW))==(1<<(int)BitMask.SE_4SW);
            if (((westTileSEBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((westTileSEBitmask || westTile.CornerColorSE==CornerColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.SW_8SE))==((1<<(int)BitMask.SW_8SE)) && colorMatching==ColorMatching.SymmetricalMatching)){
                cornerColorValues[(int)TileOffsetCorner.C8_SE] = CornerColor.WildCard;
            }

            
            // Neighboring N, E, S, W
            bool northTileSBitmask = (northTile.TileBitMask&(1<<(int)BitMask.S_6N))==(1<<(int)BitMask.S_6N);
            if (((northTileSBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((northTileSBitmask || northTile.EdgeColorSouth==VerticalColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.N_2S))==((1<<(int)BitMask.N_2S)) && colorMatching==ColorMatching.SymmetricalMatching)){
                verticalColorValues[(int)TileOffsetVertical.V2_S]=VerticalColor.WildCard;
            }

            bool eastTileWBitmask = (eastTile.TileBitMask&(1<<(int)BitMask.W_8E))==(1<<(int)BitMask.W_8E);
            if (((eastTileWBitmask || eastTile.EdgeColorWest==HorizontalColor.NoColor) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((eastTileWBitmask) && (currentTile.TileBitMask&(1<<(int)BitMask.E_4W))==((1<<(int)BitMask.E_4W)) && colorMatching==ColorMatching.SymmetricalMatching)){
                horizontalColorValues[(int)TileOffsetHorizontal.H4_W] = HorizontalColor.WildCard;
            }

            bool southTileNBitmask = (southTile.TileBitMask&(1<<(int)BitMask.N_2S))==(1<<(int)BitMask.N_2S);
            if (((southTileNBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((southTileNBitmask || southTile.EdgeColorNorth==VerticalColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.S_6N))==((1<<(int)BitMask.S_6N)) && colorMatching==ColorMatching.SymmetricalMatching)){
                verticalColorValues[(int)TileOffsetVertical.V6_N] = VerticalColor.WildCard;
            }

            bool westTileEBitmask = (westTile.TileBitMask&(1<<(int)BitMask.E_4W))==(1<<(int)BitMask.E_4W);
            if (((westTileEBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((westTileEBitmask ||westTile.EdgeColorEast==HorizontalColor.NoColor) && (currentTile.TileBitMask&(1<<(int)BitMask.W_8E))==((1<<(int)BitMask.W_8E)) && colorMatching==ColorMatching.SymmetricalMatching)){
                horizontalColorValues[(int)TileOffsetHorizontal.H8_E] = HorizontalColor.WildCard;
            }

            // Current Tile N, E, S, W
            bool currTileNBitmask = (currentTile.TileBitMask&(1<<(int)BitMask.N_2S))==(1<<(int)BitMask.N_2S);
            if (((currTileNBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((currTileNBitmask) && (currentTile.TileBitMask&(1<<(int)BitMask.N_2S))==((1<<(int)BitMask.N_2S)) && colorMatching==ColorMatching.SymmetricalMatching)){
                verticalColorValues[(int)TileOffsetVertical.V0_N]=VerticalColor.WildCard;
            }

            bool currTileEBitmask = (currentTile.TileBitMask&(1<<(int)BitMask.E_4W))==(1<<(int)BitMask.E_4W);
            if (((currTileEBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((currTileEBitmask) && (currentTile.TileBitMask&(1<<(int)BitMask.E_4W))==((1<<(int)BitMask.E_4W)) && colorMatching==ColorMatching.SymmetricalMatching)){
                horizontalColorValues[(int)TileOffsetHorizontal.H0_E] = HorizontalColor.WildCard;
            }

            bool currTileSBitmask = (currentTile.TileBitMask&(1<<(int)BitMask.S_6N))==(1<<(int)BitMask.S_6N);
            if (((currTileSBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((currTileSBitmask) && (currentTile.TileBitMask&(1<<(int)BitMask.S_6N))==((1<<(int)BitMask.S_6N)) && colorMatching==ColorMatching.SymmetricalMatching)){
                verticalColorValues[(int)TileOffsetVertical.V0_S] = VerticalColor.WildCard;
            }

            bool currTileWBitmask = (currentTile.TileBitMask&(1<<(int)BitMask.W_8E))==(1<<(int)BitMask.W_8E);
            if (((currTileWBitmask) && colorMatching==ColorMatching.CurrentTileBitmask) ||
            ((currTileWBitmask) && (currentTile.TileBitMask&(1<<(int)BitMask.W_8E))==((1<<(int)BitMask.W_8E)) && colorMatching==ColorMatching.SymmetricalMatching)){
                horizontalColorValues[(int)TileOffsetHorizontal.H0_W] = HorizontalColor.WildCard;
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