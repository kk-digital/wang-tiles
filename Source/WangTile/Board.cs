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

                // North west
                numberOfMismatch=numberOfMismatch+countMismatchOnCorner(cColors, TileOffsetCorner.C0_NW, TileOffsetCorner.C8_NE, TileOffsetCorner.C1_SE, TileOffsetCorner.C2_SW);
                // North east
                numberOfMismatch=numberOfMismatch+countMismatchOnCorner(cColors, TileOffsetCorner.C4_NW, TileOffsetCorner.C0_NE, TileOffsetCorner.C2_SE, TileOffsetCorner.C3_SW);
                // South east
                numberOfMismatch=numberOfMismatch+countMismatchOnCorner(cColors, TileOffsetCorner.C5_NW, TileOffsetCorner.C6_NE, TileOffsetCorner.C0_SE, TileOffsetCorner.C4_SW);
                // South west
                numberOfMismatch=numberOfMismatch+countMismatchOnCorner(cColors, TileOffsetCorner.C6_NW, TileOffsetCorner.C7_NE, TileOffsetCorner.C8_SE, TileOffsetCorner.C0_SW );
            
                // North
                numberOfMismatch=numberOfMismatch+countMismatchVertical(vColors,TileOffsetVertical.V0_N, TileOffsetVertical.V2_S);
                // South
                numberOfMismatch=numberOfMismatch+countMismatchVertical(vColors,TileOffsetVertical.V6_N,TileOffsetVertical.V0_S);
                // West
                numberOfMismatch=numberOfMismatch+countMismatchHorizontal(hColors,TileOffsetHorizontal.H0_W,TileOffsetHorizontal.H8_E);
                // East
                numberOfMismatch=numberOfMismatch+countMismatchHorizontal(hColors,TileOffsetHorizontal.H4_W,TileOffsetHorizontal.H0_E);

                tilesMismatches[i]=numberOfMismatch;

                // Remove placed tile since
                // it's not the final tile yet.
                this.RemoveTile(col,row);
            }

            

            return tilesMismatches;
        }

        int countMismatchOnCorner(CornerColor[] cornerColors, TileOffsetCorner NW, TileOffsetCorner NE, TileOffsetCorner SE,TileOffsetCorner SW){
            int numberOfMismatch=0;

            if (cornerColors[(int)NW]!=cornerColors[(int)NE] && (
                cornerColors[(int)NW]!=(int)CornerColor.WildCard &&
                cornerColors[(int)NE]!=(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)NW]!=cornerColors[(int)SE] && (
                cornerColors[(int)NW]!=(int)CornerColor.WildCard && 
                cornerColors[(int)SE]!=(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)NW]!=cornerColors[(int)SW] && (
                cornerColors[(int)NW]!=(int)CornerColor.WildCard && 
                cornerColors[(int)SW]!=(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)NE]!=cornerColors[(int)SW] && (
                cornerColors[(int)NE]!=(int)CornerColor.WildCard && 
                cornerColors[(int)SW]!=(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)NE]!=cornerColors[(int)SE] && (
                cornerColors[(int)NE]!=(int)CornerColor.WildCard && 
                cornerColors[(int)SE]!=(int)CornerColor.WildCard)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)SE]!=cornerColors[(int)SW] && (
                cornerColors[(int)SE]!=(int)CornerColor.WildCard && 
                cornerColors[(int)SW]!=(int)CornerColor.WildCard)) {
               numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        int countMismatchVertical(VerticalColor[] vColors, TileOffsetVertical N, TileOffsetVertical S){
            int numberOfMismatch=0;

            if (vColors[(int)N]!=vColors[(int)S] && (
                vColors[(int)N]!=(int)VerticalColor.WildCard &&
                vColors[(int)S]!=(int)VerticalColor.WildCard)) {
                numberOfMismatch++;
            }

            return numberOfMismatch;
        }

            int countMismatchHorizontal(HorizontalColor[] hColors, TileOffsetHorizontal W, TileOffsetHorizontal E){
            int numberOfMismatch=0;

            if (hColors[(int)W]!=hColors[(int)E] && (
                hColors[(int)W]!=(int)HorizontalColor.WildCard &&
                hColors[(int)E]!=(int)HorizontalColor.WildCard)) {
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

            cornerColorValues[(int)TileOffsetCorner.C0_SW]= currentTile.CornerColorSW;
            cornerColorValues[(int)TileOffsetCorner.C6_NW]= southTile.CornerColorNW;
            cornerColorValues[(int)TileOffsetCorner.C7_NE]= southWestTile.CornerColorNE;
            cornerColorValues[(int)TileOffsetCorner.C8_SE]= westTile.CornerColorSE;

            horizontalColorValues[(int)TileOffsetHorizontal.H0_W]=currentTile.EdgeColorWest;
            horizontalColorValues[(int)TileOffsetHorizontal.H8_E]=westTile.EdgeColorEast;

            // cornerColorValues = applyBitmaskingForCornerColor(currentTile.BitMask, cornerColorValues);
            // verticalColorValues = applyBitmaskingForVerticalColor(currentTile.BitMask, verticalColorValues);
            // horizontalColorValues = applyBitmaskingForHorizontalColor(currentTile.BitMask, horizontalColorValues);

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