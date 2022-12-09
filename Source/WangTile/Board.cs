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

    public struct TileProbability{
        public int TileID;
        public int NumberOfMismatch;
        public float Weight;

        public TileProbability(int tileID, float weight, int numberOfMismatches){
            this.TileID = tileID;
            this.Weight = weight;
            this.NumberOfMismatch= numberOfMismatches;
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

                (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = GetTileAdjacentColorValues(useBitmasking,col,row);

                
                numberOfMismatch=TetrisMismatchCalculator.CountMismatchOnCorners_ForPlacement(cColors,tiles[i].TileBitMask,colorMatching);
                numberOfMismatch+=TetrisMismatchCalculator.CountMismatchVertical_ForPlacement(vColors,tiles[i].TileBitMask,colorMatching);
                numberOfMismatch+=TetrisMismatchCalculator.CountMismatchHorizontal_ForPlacement(hColors,tiles[i].TileBitMask,colorMatching);
                
                tilesMismatches[i]=numberOfMismatch;

                // Remove placed tile since
                // it's not the final tile yet.
                this.RemoveTile(col,row);
            }

            

            return tilesMismatches;
        }

        public TileMismatch[] GetTilesWithLowestMismatches(TileMismatch[] tileMismatches){
            TileMismatch[] lowestTileMismatches= new TileMismatch[0];
            int lowestMismatch=0;
            while (true){
                for (int i=0;i<tileMismatches.Length;i++){
                    if (tileMismatches[i].NumberOfMismatches==lowestMismatch) {
                        lowestTileMismatches=lowestTileMismatches.Append(tileMismatches[i]).ToArray();
                    }
                }

                if (lowestTileMismatches.Length==0){
                    lowestMismatch++;
                } else{
                    return lowestTileMismatches;
                }
            }
        }

        public void RemoveTilesWithMismatches(bool useBitmasking, ColorMatching colorMatching){
            (int col, int row) pos = (0,0);

            bool wasThereMismatch = false;
            while (true){
                int numberOfMismatch = 0;
                WangTile tile = this.getTile(pos.col,pos.row);
                (CornerColor[] cColors, HorizontalColor[] hColors, VerticalColor[] vColors) = GetTileAdjacentColorValues(useBitmasking,pos.col,pos.row);

                
                numberOfMismatch=TetrisMismatchCalculator.CountMismatchOnCorners_ForRemoval(cColors,tile.TileBitMask);
                numberOfMismatch+=TetrisMismatchCalculator.CountMismatchVertical_ForRemoval(vColors,tile.TileBitMask);
                numberOfMismatch+=TetrisMismatchCalculator.CountMismatchHorizontal_ForRemoval(hColors,tile.TileBitMask);

                if (numberOfMismatch>0){
                    wasThereMismatch = true;
                    this.RemoveTile(pos.col,pos.row);
                    // Console.WriteLine($"Col={pos.col},Row={pos.row}, mismatches={numberOfMismatch}");
                }

                pos = GetNextTileSlot(pos.col,pos.row);
                
                if (pos.col==this.Height){
                    if (!wasThereMismatch){
                        break;
                    }
                    wasThereMismatch=false;

                    pos.col=0;
                    pos.row=0;
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
            if (westTileNEBitmask){
                cornerColorValues[(int)TileOffsetCorner.C8_NE]= CornerColor.WildCard;
            }

            bool northWestTileSEBitmask =(northWestTile.TileBitMask&(1<<(int)BitMask.SE_5NW))==(1<<(int)BitMask.SE_5NW);
            if (northWestTileSEBitmask){
                cornerColorValues[(int)TileOffsetCorner.C1_SE]= CornerColor.WildCard;
            }

            bool northTileSWBitmask = (northTile.TileBitMask&(1<<(int)BitMask.SW_6NW))==(1<<(int)BitMask.SW_6NW);
            if (northTileSWBitmask){
                cornerColorValues[(int)TileOffsetCorner.C2_SW]= CornerColor.WildCard;
            }

            // NE corner
            bool northTileSEBitmask = (northTile.TileBitMask&(1<<(int)BitMask.SE_6NE))==(1<<(int)BitMask.SE_6NE);
            if (northTileSEBitmask){
                cornerColorValues[(int)TileOffsetCorner.C2_SE]= CornerColor.WildCard;
            }

            bool northEastTileSWBitmask = (northEastTile.TileBitMask&(1<<(int)BitMask.SW_7NE))==(1<<(int)BitMask.SW_7NE);
            if (northEastTileSWBitmask){
                cornerColorValues[(int)TileOffsetCorner.C3_SW]= CornerColor.WildCard;
            }

            bool eastTileNWBitmask = (eastTile.TileBitMask&(1<<(int)BitMask.NW_8NE))==(1<<(int)BitMask.NW_8NE);
            if (eastTileNWBitmask){
                cornerColorValues[(int)TileOffsetCorner.C4_NW]= CornerColor.WildCard;
            }

            // SE corner
            bool eastTileSWBitmask = (eastTile.TileBitMask&(1<<(int)BitMask.SW_8SE))==(1<<(int)BitMask.SW_8SE);
            if (eastTileSWBitmask){
                cornerColorValues[(int)TileOffsetCorner.C4_SW] = CornerColor.WildCard;
            }

            bool southEastTileNWBitmask = (southEastTile.TileBitMask&(1<<(int)BitMask.NW_1SE))==(1<<(int)BitMask.NW_1SE);
            if (southEastTileNWBitmask){
                cornerColorValues[(int)TileOffsetCorner.C5_NW] = CornerColor.WildCard;
            }

            bool southTileNEBitmask = (southTile.TileBitMask&(1<<(int)BitMask.NE_2SE))==(1<<(int)BitMask.NE_2SE);
            if (southTileNEBitmask){
                cornerColorValues[(int)TileOffsetCorner.C6_NE] = CornerColor.WildCard;
            }

            // SW corner
            bool southTileNWBitmask = (southTile.TileBitMask&(1<<(int)BitMask.NW_2SW))==(1<<(int)BitMask.NW_2SW);
            if (southTileNWBitmask){
                cornerColorValues[(int)TileOffsetCorner.C6_NW] = CornerColor.WildCard;
            }

            bool southWestNEBitmask = (southWestTile.TileBitMask&(1<<(int)BitMask.NE_3SW))==(1<<(int)BitMask.NE_3SW);
            if (southWestNEBitmask){
                cornerColorValues[(int)TileOffsetCorner.C7_NE] = CornerColor.WildCard;
            }

            bool westTileSEBitmask = (westTile.TileBitMask&(1<<(int)BitMask.SE_4SW))==(1<<(int)BitMask.SE_4SW);
            if (westTileSEBitmask){
                cornerColorValues[(int)TileOffsetCorner.C8_SE] = CornerColor.WildCard;
            }

            
            // Neighboring N, E, S, W
            bool northTileSBitmask = (northTile.TileBitMask&(1<<(int)BitMask.S_6N))==(1<<(int)BitMask.S_6N);
            if (northTileSBitmask){
                verticalColorValues[(int)TileOffsetVertical.V2_S]=VerticalColor.WildCard;
            }

            bool eastTileWBitmask = (eastTile.TileBitMask&(1<<(int)BitMask.W_8E))==(1<<(int)BitMask.W_8E);
            if (eastTileWBitmask){
                horizontalColorValues[(int)TileOffsetHorizontal.H4_W] = HorizontalColor.WildCard;
            }

            bool southTileNBitmask = (southTile.TileBitMask&(1<<(int)BitMask.N_2S))==(1<<(int)BitMask.N_2S);
            if (southTileNBitmask){
                verticalColorValues[(int)TileOffsetVertical.V6_N] = VerticalColor.WildCard;
            }

            bool westTileEBitmask = (westTile.TileBitMask&(1<<(int)BitMask.E_4W))==(1<<(int)BitMask.E_4W);
            if (westTileEBitmask){
                horizontalColorValues[(int)TileOffsetHorizontal.H8_E] = HorizontalColor.WildCard;
            }

            return (cColors:cornerColorValues,hColors:horizontalColorValues,vColors:verticalColorValues);
        }
        
        // Return empty slot position
        public (int col, int row) GetEmptySlotPosition(){
            (int col, int row) pos = (0,0);

            while (true){
                int index = Utils.GetBoardSlotIndex(this.Width, pos.col, pos.row);
                if (this.TileSlots[index].TileID==null){
                    return pos;
                }

                 pos = GetNextTileSlot(pos.col,pos.row);
                
                if (pos.col==this.Height){
                    return (col: this.Height, row: this.Width);
                }
            }
        }
        // RemoveAdjacentTiles remove all 4 tiles on its north, east, south, and west.
        public void RemoveAdjacentTiles(int col, int row){
            (int Col,int Row) northPos = Utils.GetNorthCoordinates(col, row);
            (int Col,int Row) eastPos = Utils.GetEastCoordinates(col, row);
            (int Col,int Row) southPos = Utils.GetSouthCoordinates(col, row);
            (int Col,int Row) westPos = Utils.GetWestCoordinates(col, row);

            if (this.isValidPosition(northPos.Col, northPos.Row)){
                this.RemoveTile(northPos.Col, northPos.Row);
            }

            if (this.isValidPosition(eastPos.Col,eastPos.Row)){
                this.RemoveTile(eastPos.Col,eastPos.Row);
            }

            if (this.isValidPosition(southPos.Col, southPos.Row)){
                this.RemoveTile(southPos.Col, southPos.Row); 
            }

            if (this.isValidPosition(westPos.Col, westPos.Row)){
                this.RemoveTile(westPos.Col, westPos.Row);
            }
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
            (int col, int row) coord = Utils.GetNorthCoordinates(col,row);
            coord = Utils.GetWestCoordinates(coord.col,coord.row);

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
            (int col, int row) coord = Utils.GetNorthCoordinates(col,row);
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
            (int col, int row) coord = Utils.GetNorthCoordinates(col,row);
            coord = Utils.GetEastCoordinates(coord.col,coord.row);
            
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
            (int col, int row) coord = Utils.GetEastCoordinates(col,row);
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
            (int col, int row) coord = Utils.GetSouthCoordinates(col,row);
            coord = Utils.GetEastCoordinates(coord.col,coord.row);
            
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
            (int col, int row) coord = Utils.GetSouthCoordinates(col,row);
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
            (int col, int row) coord = Utils.GetSouthCoordinates(col,row);
            coord = Utils.GetWestCoordinates(coord.col,coord.row);
            
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
            (int col, int row) coord = Utils.GetWestCoordinates(col,row);
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

        // Find random tile slot position with adjacent tile and has no tile yet
        public (int col, int row) FindEmptySlotWithAdjacentTilesOnEdges(Random rand){
            int i=0;
            (int col, int row) emptySlotPos;
            while (true){
                (int col, int row) pos = Utils.GetRandomPosition(this.Width,this.Height, rand);
                if (IsThereAnAdjacentTileOnEdges(pos.col,pos.row) && !isTileAlreadyExist(pos.col,pos.row)){
                    return (col:pos.col,row:pos.row);
                }

                if (i%100==0){
                    emptySlotPos = this.GetEmptySlotPosition();
                    if (emptySlotPos.col==this.Height && emptySlotPos.row== this.Width){
                        // No empty tile slots
                        return emptySlotPos;
                    }

                    if (i==1000){
                        return pos;
                    } 
                }
                i++;
            }
        }

        bool IsThereAnAdjacentTileOnEdges(int col, int row){
            (int col, int row) northCoor = Utils.GetNorthCoordinates(col,row);
            (int col, int row) eastCoor = Utils.GetEastCoordinates(col,row);
            (int col, int row) southCoor = Utils.GetSouthCoordinates(col,row);
            (int col, int row) westCoor = Utils.GetWestCoordinates(col,row);

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

        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////// Weighted-Probability Methods ////////////////////////////////////
        public TileProbability[] GetProbabilityVector(int col,int row, int tileSetID, TileMismatch[] tileMismatches){
            TileProbability[] tileProbabilityVector = new TileProbability[tileMismatches.Length];
            int numberOfMismatch = 0;
    
            float k = 1f;
            int maxEnergy = 16;
            float gamma = 2.0f;
            float epsilon = 0f; // 1/16
            int numberOfTiles = this.TileSet[tileSetID].Tiles.Length;
            float maxEnergyPowerGamma = (float)Math.Pow((double)maxEnergy,gamma);

            float epsilonMulInverseOfNumOfTiles = epsilon *(1.0f/numberOfTiles);

            for (int i=0;i<tileMismatches.Length;i++){
                
                numberOfMismatch = tileMismatches[i].NumberOfMismatches;
                // Weight[i] = (k * ((max energy - error(i))^gamma) / (max_energy^gamma) ) + epsilon* (1.0 / number-of-tiles)
                float term1= k * (((float)Math.Pow((double)(maxEnergy-numberOfMismatch),gamma)/maxEnergyPowerGamma));
                float term2= epsilonMulInverseOfNumOfTiles;
                float weight =  term1+term2; 

                // Console.WriteLine($"NumberOfMismatch={numberOfMismatch}, Term 1 + Term 2 = {term1} + {term2}");
                // Console.WriteLine("tileID= "+i+", weight= "+weight[i].ToString("0.000")+$", # of mismatches={numberOfMismatch}"+$", term1= "+term1.ToString("0.000")+$", term2= "+term2.ToString("0.000"));
                // Console.WriteLine("--------------------------");
                TileProbability tileProbability = new TileProbability(tileMismatches[i].TileID, weight, tileMismatches[i].NumberOfMismatches);
                tileProbabilityVector[i]=tileProbability;
            }

            return tileProbabilityVector;
        }


        public TileProbability[] GetNormalizedProbabilityVector(TileProbability[] tileProbabilityVector){
            float sum = 0f;
            for (int i = 0;i < tileProbabilityVector.Length;i++){
                sum += tileProbabilityVector[i].Weight;
            }

            for (int i = 0;i < tileProbabilityVector.Length;i++){
                float normalizedWeight = tileProbabilityVector[i].Weight/sum;
                tileProbabilityVector[i].Weight=normalizedWeight;
            }

            return tileProbabilityVector;
        }

        public TileProbability[] GetCumulativeProbabilityVector(TileProbability[] tileProbabilityVector){
            TileProbability[] CV = tileProbabilityVector;

            float sum = 0;
            for (int i = 0;i < CV.Length;i++){
                sum = sum + CV[i].Weight;
                CV[i].Weight=sum;
            }
            
            return CV;
        }

        public int ChooseTileIndexFromCumulativeProbabilityVector(TileProbability[] CumulativeProbabilityVector, Random rand){
            
            // int randomInt = rand.Next(0,100);
            // float randomFloat = (float)randomInt / 100f;
             float randomFloat = 0.3f;
                
            for (int i=0;i<CumulativeProbabilityVector.Length*2;i++){
                int chooseIndex = rand.Next(0,CumulativeProbabilityVector.Length);
                if (CumulativeProbabilityVector[chooseIndex].Weight<randomFloat){
                    return CumulativeProbabilityVector[chooseIndex].TileID;
                }
            }

            return 0;
        }
     
    }
}