namespace Wang
{
    public class Board
    {
        public BoardTileSlot[] TileSlots;
        public int Height;
        public int Width;
        public WangCornerTileSet[]? TileSet;

        // Constructor
        public Board(int height, int width)
        {
            TileSlots = new BoardTileSlot[height*width];
            Height = height;
            Width = width;
        }

        // Methods
        public void AddTileSet(WangCornerTileSet tileSet)
        {   
            if (this.TileSet==null){
                this.TileSet = new WangCornerTileSet[1];
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

        // numberOfVariants is the number of 
        // variants per color combination.
        // numberOfColors is the number of 
        // colors available to make a combination.
        public WangCornerTileSet GenerateTileSet(int numberOfColors, int numberOfVariants)
        {
        int NW, NE, SE, SW;
        WangCornerTileSet tileSet = new WangCornerTileSet();

        for (NW = 0; NW < numberOfColors; NW++) {
            for (NE = 0; NE < numberOfColors; NE++) {
                for (SE = 0; SE < numberOfColors; SE++) {
                    for (SW = 0; SW < numberOfColors; SW++) {

                        // +2 because colors in our enum starts with 2
                        // 0 and 1 are special cases
                        WangCornerTile newTile=tileSet.CreateTile(TileGeometry.FP,(Color)NW+2,(Color)NE+2,(Color)SE+2,(Color)SW+2);
                    }
                }
            }
        } 

        return tileSet;
        }

        public WangCornerTileSet GenerateOthersTileSet(){
            WangCornerTileSet tileSet = new WangCornerTileSet();
            WangCornerTile newTile=tileSet.CreateTile(TileGeometry.Error,Color.Air,Color.Air,Color.Air,Color.Air);
            tileSet.Tiles=new WangCornerTile[1];
            tileSet.Tiles[0]=newTile;

            return tileSet;
        }

        public (int col, int row) AddTileToAdjacent(int col, int row)
        {
            var pos=chooseRandomAdjacentSide(col,row);
            var adjacentColors=getTileAdjacentColors(pos.col,pos.row);
            var matchTiles=this.TileSet[0].ReturnMatches(this.TileSet[0].Tiles,adjacentColors.colorNW,adjacentColors.colorNE,adjacentColors.colorSE,adjacentColors.colorSW);
   
            Random randomIndex = new Random();
            // Select random tile to place
            int randIndex = randomIndex.Next(0,matchTiles.Length);

            int newTileID=matchTiles[randIndex];
            this.PlaceTile(0,newTileID,pos.col,pos.row);

            return (col:pos.col,row:pos.row);
        }

        public (int col,int row) AddTileToNextSlot(int col,int row){
            var pos = GetNextTileSlot(col,row);

            var adjacentColors=getTileAdjacentColors(pos.col,pos.row);
            var matchTiles=this.TileSet[0].ReturnMatches(this.TileSet[0].Tiles,adjacentColors.colorNW,adjacentColors.colorNE,adjacentColors.colorSE,adjacentColors.colorSW);
   
            Random randomIndex = new Random();
            // Select random tile to place
            int randIndex = randomIndex.Next(0,matchTiles.Length);

            int newTile=matchTiles[randIndex];
            this.PlaceTile(0,newTile,pos.col,pos.row);

            return (col:pos.col,row:pos.row);
        }


        public Color[] GetTileCornerColorValues(int col, int row) {
            Color[] cornerColorValues = new Color[16];

            WangCornerTile currentTile = getTile(col,row);
            WangCornerTile northWestTile = getNorthWestTile(col,row);
            WangCornerTile northTile = getNorthTile(col,row);
            WangCornerTile northEastTile = getNorthEastTile(col,row);
            WangCornerTile eastTile = getEastTile(col,row);
            WangCornerTile southEastTile = getSouthEastTile(col,row);
            WangCornerTile southTile = getSouthTile(col,row);
            WangCornerTile southWestTile = getSouthWestTile(col,row);
            WangCornerTile westTile = getWestTile(col,row);

            cornerColorValues[(int)TileOffsetCorner.C0_NW]= currentTile.CornerColorNW;
            cornerColorValues[(int)TileOffsetCorner.C8_NE]= westTile.CornerColorNE;
            cornerColorValues[(int)TileOffsetCorner.C1_SE]= northWestTile.CornerColorSE;
            cornerColorValues[(int)TileOffsetCorner.C2_SW]= northTile.CornerColorSW;

            cornerColorValues[(int)TileOffsetCorner.C0_NE]= currentTile.CornerColorNE;
            cornerColorValues[(int)TileOffsetCorner.C2_SE]= northTile.CornerColorSE;
            cornerColorValues[(int)TileOffsetCorner.C3_SW]= northEastTile.CornerColorSW;
            cornerColorValues[(int)TileOffsetCorner.C4_NW]= eastTile.CornerColorNW;

            cornerColorValues[(int)TileOffsetCorner.C0_SE]= currentTile.CornerColorSE;
            cornerColorValues[(int)TileOffsetCorner.C4_SW]= eastTile.CornerColorSW;
            cornerColorValues[(int)TileOffsetCorner.C5_NW]= southEastTile.CornerColorNW;
            cornerColorValues[(int)TileOffsetCorner.C6_NE]= southTile.CornerColorNE;

            cornerColorValues[(int)TileOffsetCorner.C0_SW]= currentTile.CornerColorSW;
            cornerColorValues[(int)TileOffsetCorner.C6_NW]= southTile.CornerColorNW;
            cornerColorValues[(int)TileOffsetCorner.C7_NE]= southWestTile.CornerColorNE;
            cornerColorValues[(int)TileOffsetCorner.C8_SE]= westTile.CornerColorSE;

            return cornerColorValues;
        }
        
        public bool IsThereMismatch(int col, int row){
            Color[] cornerColors = GetTileCornerColorValues(col,row);

            // check if tile is unassigned
            // if unassigned then a mismatch
            if (cornerColors[(int)TileOffsetCorner.C0_NW]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_NE]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_SE]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_SW]==Color.MatchAll){
                    return true;
            }

            // North west
            if (isThereMismatchOnCorner(cornerColors, TileOffsetCorner.C0_NW, TileOffsetCorner.C8_NE, TileOffsetCorner.C1_SE, TileOffsetCorner.C2_SW)){
                return true;
            }


            // North east
            if (isThereMismatchOnCorner(cornerColors, TileOffsetCorner.C4_NW, TileOffsetCorner.C0_NE, TileOffsetCorner.C2_SE, TileOffsetCorner.C3_SW)){
                return true;
            }

            // South east
            if (isThereMismatchOnCorner(cornerColors, TileOffsetCorner.C5_NW, TileOffsetCorner.C6_NE, TileOffsetCorner.C0_SE, TileOffsetCorner.C4_SW)){
                return true;
            }

            // South west
           if (isThereMismatchOnCorner(cornerColors, TileOffsetCorner.C6_NW, TileOffsetCorner.C7_NE, TileOffsetCorner.C8_SE, TileOffsetCorner.C0_SW )){
                return true;
            }

            return false;
        }

        bool isThereMismatchOnCorner(Color[] cornerColors, TileOffsetCorner NW, TileOffsetCorner NE, TileOffsetCorner SE,TileOffsetCorner SW){
            if (cornerColors[(int)NW]!=cornerColors[(int)NE] && (
                cornerColors[(int)NW]!=(int)Color.MatchAll &&
                cornerColors[(int)NE]!=(int)Color.MatchAll)) {
                return true;
            }

            if (cornerColors[(int)NW]!=cornerColors[(int)SE] && (
                cornerColors[(int)NW]!=(int)Color.MatchAll &&
                cornerColors[(int)SE]!=(int)Color.MatchAll)) {
                return true;
            }

            if (cornerColors[(int)NW]!=cornerColors[(int)SW] && (
                cornerColors[(int)NW]!=(int)Color.MatchAll &&
                cornerColors[(int)SW]!=(int)Color.MatchAll)) {
                return true;
            }

            if (cornerColors[(int)NE]!=cornerColors[(int)SW] && (
                cornerColors[(int)NE]!=(int)Color.MatchAll &&
                cornerColors[(int)SW]!=(int)Color.MatchAll)) {
                return true;
            }

            if (cornerColors[(int)NE]!=cornerColors[(int)SE] && (
                cornerColors[(int)NE]!=(int)Color.MatchAll && 
                cornerColors[(int)SE]!=(int)Color.MatchAll)) {
                return true;
            }

            if (cornerColors[(int)SE]!=cornerColors[(int)SW] && (
                cornerColors[(int)SE]!=(int)Color.MatchAll && 
                cornerColors[(int)SW]!=(int)Color.MatchAll)) {
                return true;
            }

            return false;
        }

        int countMismatchOnCorner(Color[] cornerColors, TileOffsetCorner NW, TileOffsetCorner NE, TileOffsetCorner SE,TileOffsetCorner SW){
            int numberOfMismatch=0;

            if (cornerColors[(int)NW]!=cornerColors[(int)NE] && (
                cornerColors[(int)NW]!=(int)Color.MatchAll &&
                cornerColors[(int)NE]!=(int)Color.MatchAll)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)NW]!=cornerColors[(int)SE] && (
                cornerColors[(int)NW]!=(int)Color.MatchAll && 
                cornerColors[(int)SE]!=(int)Color.MatchAll)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)NW]!=cornerColors[(int)SW] && (
                cornerColors[(int)NW]!=(int)Color.MatchAll && 
                cornerColors[(int)SW]!=(int)Color.MatchAll)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)NE]!=cornerColors[(int)SW] && (
                cornerColors[(int)NE]!=(int)Color.MatchAll && 
                cornerColors[(int)SW]!=(int)Color.MatchAll)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)NE]!=cornerColors[(int)SE] && (
                cornerColors[(int)NE]!=(int)Color.MatchAll && 
                cornerColors[(int)SE]!=(int)Color.MatchAll)) {
                numberOfMismatch++;
            }

            if (cornerColors[(int)SE]!=cornerColors[(int)SW] && (
                cornerColors[(int)SE]!=(int)Color.MatchAll && 
                cornerColors[(int)SW]!=(int)Color.MatchAll)) {
               numberOfMismatch++;
            }

            return numberOfMismatch;
        }

        public int GetNumberOfMismatch(int col, int row){
            int numberOfMismatch=0;
            Color[] cornerColors = GetTileCornerColorValues(col,row);
  
            // check if tile is unassigned
            // if unassigned then atleast 4 mismatch
            if (cornerColors[(int)TileOffsetCorner.C0_NW]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_NE]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_SE]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_SW]==Color.MatchAll){

                // then atleast 4 mismatches
                return 4;
            }

            // North west
            numberOfMismatch=numberOfMismatch+countMismatchOnCorner(cornerColors, TileOffsetCorner.C0_NW, TileOffsetCorner.C8_NE, TileOffsetCorner.C1_SE, TileOffsetCorner.C2_SW);
            // North east
            numberOfMismatch=numberOfMismatch+countMismatchOnCorner(cornerColors, TileOffsetCorner.C4_NW, TileOffsetCorner.C0_NE, TileOffsetCorner.C2_SE, TileOffsetCorner.C3_SW);

            // South east
            numberOfMismatch=numberOfMismatch+countMismatchOnCorner(cornerColors, TileOffsetCorner.C5_NW, TileOffsetCorner.C6_NE, TileOffsetCorner.C0_SE, TileOffsetCorner.C4_SW);

            // South west
            numberOfMismatch=numberOfMismatch+countMismatchOnCorner(cornerColors, TileOffsetCorner.C6_NW, TileOffsetCorner.C7_NE, TileOffsetCorner.C8_SE, TileOffsetCorner.C0_SW );
            
            return numberOfMismatch;
        }

        public int GetNumberOfMismatch_CountCornerMismatchAsOne(int col, int row){
            int numberOfMismatch=0;
            Color[] cornerColors = GetTileCornerColorValues(col,row);
  
            // check if tile is unassigned
            // if unassigned then atleast 4 mismatch
            if (cornerColors[(int)TileOffsetCorner.C0_NW]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_NE]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_SE]==Color.MatchAll &&
                cornerColors[(int)TileOffsetCorner.C0_SW]==Color.MatchAll){

                // then atleast 4 mismatches
                return 4;
            }

            // North west
            if (countMismatchOnCorner(cornerColors, TileOffsetCorner.C0_NW, TileOffsetCorner.C8_NE, TileOffsetCorner.C1_SE, TileOffsetCorner.C2_SW)>0){
                numberOfMismatch++;
            }
      
            // North east
            if (countMismatchOnCorner(cornerColors, TileOffsetCorner.C4_NW, TileOffsetCorner.C0_NE, TileOffsetCorner.C2_SE, TileOffsetCorner.C3_SW)>0){
                numberOfMismatch++;
            }
  
            // South east
            if (countMismatchOnCorner(cornerColors, TileOffsetCorner.C5_NW, TileOffsetCorner.C6_NE, TileOffsetCorner.C0_SE, TileOffsetCorner.C4_SW)>0){
                numberOfMismatch++;
            }

            // South west
            if (countMismatchOnCorner(cornerColors, TileOffsetCorner.C6_NW, TileOffsetCorner.C7_NE, TileOffsetCorner.C8_SE, TileOffsetCorner.C0_SW)>0){
                numberOfMismatch++;
            }
    
            return numberOfMismatch;
        }


        public int GetBoardTotalMismatch(){
            int col= this.Height;
            int row=this.Width;
            int totalMismatches=0;

            for (int i=0;i<col;i++){
                for (int j=0;j<row;j++){
                    totalMismatches=totalMismatches+GetNumberOfMismatch(i,j);
                }
            }

            return totalMismatches;
        }

        (Color colorNW, Color colorNE, Color colorSE, Color colorSW) getTileAdjacentColors(int col, int row){
            WangCornerTile northTile = getNorthTile(col,row);
            WangCornerTile eastTile = getEastTile(col,row);
            WangCornerTile southTile = getSouthTile(col,row);
            WangCornerTile westTile = getWestTile(col,row);

            Color colorNW = Color.MatchAll;
            Color colorNE = Color.MatchAll;
            Color colorSE = Color.MatchAll;
            Color colorSW = Color.MatchAll;

            colorNE=northTile.CornerColorSE;
            if (colorNE==Color.MatchAll){
                colorNE=eastTile.CornerColorNW;
            }

            colorSE=eastTile.CornerColorSW;
            if (colorSE==Color.MatchAll){
               colorSE=southTile.CornerColorNE;
            }

            colorSW=southTile.CornerColorNW;
            if (colorSW==Color.MatchAll){
                colorSW=westTile.CornerColorSE;
            }

            colorNW=northTile.CornerColorSW;
            if (colorNW==Color.MatchAll){
                colorNW=westTile.CornerColorNE;
            }

            return (colorNW:colorNW,colorNE:colorNE,colorSE:colorSE,colorSW:colorSW);

        }

        (int col,int row) chooseRandomAdjacentSide(int col, int row){

            // randomize options
            // 0-north, 1-east, 2-south, 3-west
            var sideOptions = new int[]{0,1,2,3};
            Random rand = new Random();
            sideOptions = sideOptions.OrderBy(col => rand.Next()).ToArray();

            int option=0;
            (int col, int row) coord = (col:0,row:0);
            for (int i=0;i<4;i++){
                option=sideOptions[i];

                switch (option){
                case 0:
                    // north
                    coord = getNorthCoordinates(col,row);
                    
                    break;
                case 1:
                    // east
                    coord=getEastCoordinates(col,row);

                    break;
                case 2:
                    // south
                    coord=getSouthCoordinates(col,row);

                    break;
                case 3:
                    // west
                    coord=getWestCoordinates(col,row);
                  
                    break;
                }

                if (isValidPosition(coord.col,coord.row) && !isTileAlreadyExist(coord.col,coord.row)){
                        return (col:coord.col,row:coord.row);
                }
            }

            // No valid adjacent tile then,
            // find random slot that has no tile.
            coord= getRandomCoordinates();

            return (col:coord.col,row:coord.row); 
        }

        public (int col, int row) GetNextTileSlot(int col, int row){
            if (row==this.Width-1){
                return (col:col+1,row:0);
            } else {
                return (col:col, row:row+1);
            }
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

        (int col, int row) getRandomCoordinates(){
            Random rand = new Random();

            while (true){
                int randRow = rand.Next(0, this.Width);
                int randCol = rand.Next(0, this.Height);

                if (isValidPosition(randCol,randRow) && !isTileAlreadyExist(randCol,randRow)){
                    return (col:randCol,row:randRow);
                }
            }
        }

        WangCornerTile getTile(int col, int row){
            if (isValidPosition(col,row) && isTileAlreadyExist(col,row)){
                int index = Utils.GetBoardSlotIndex(this.Width,col,row);

                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
        }

        WangCornerTile getNorthWestTile(int col, int row){
            (int col, int row) coord = getNorthCoordinates(col,row);
            coord = getWestCoordinates(coord.col,coord.row);

            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
        }

        WangCornerTile getNorthTile(int col, int row){
            (int col, int row) coord = getNorthCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
        }

        WangCornerTile getNorthEastTile(int col, int row){
            (int col, int row) coord = getNorthCoordinates(col,row);
            coord = getEastCoordinates(coord.col,coord.row);
            
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
        }

        WangCornerTile getEastTile(int col, int row){
            (int col, int row) coord = getEastCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
        }

        WangCornerTile getSouthEastTile(int col, int row){
            (int col, int row) coord = getSouthCoordinates(col,row);
            coord = getEastCoordinates(coord.col,coord.row);
            
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
        }

        WangCornerTile getSouthTile(int col, int row){
            (int col, int row) coord = getSouthCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
        }

        WangCornerTile getSouthWestTile(int col, int row){
            (int col, int row) coord = getSouthCoordinates(col,row);
            coord = getWestCoordinates(coord.col,coord.row);
            
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
        }

        WangCornerTile getWestTile(int col, int row){
            (int col, int row) coord = getWestCoordinates(col,row);
            if (isValidPosition(coord.col,coord.row) && isTileAlreadyExist(coord.col,coord.row)){
                int index = Utils.GetBoardSlotIndex(this.Width,coord.col,coord.row);
                
                int tileSetID=(int)this.TileSlots[index].TileSetID;
                int tileID=(int)this.TileSlots[index].TileID;
                return this.TileSet[tileSetID].Tiles[tileID];
            }

            return new WangCornerTile(TileGeometry.Error,Color.MatchAll,Color.MatchAll,Color.MatchAll,Color.MatchAll);
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
        ////////////////////////////// Weighted-Probability Algo Methods ////////////////////////////////
        public float[] GetProbabilityVector(int col,int row, int tileSetID, EnergyCalculationMode energyCalculationMode){
            int numberOfMismatch = 0;
            int tileID = 0;
            float k = 1f;
            int maxEnergy = 24;
            float gamma = 2.0f;
            float epsilon = 0f; // 1/16
            int numberOfTiles = this.TileSet[tileSetID].Tiles.Length;
            float maxEnergyPowerGamma = (float)Math.Pow((double)maxEnergy,gamma);

            float epsilonMulInverseOfNumOfTiles = epsilon *(1.0f/numberOfTiles);

            float[] weight = new float[numberOfTiles];
            for (int i=0;i<numberOfTiles;i++){
                // Place Ith tile to the position
                tileID = i;
                this.PlaceTile(tileSetID, tileID, col,row);

                // Get error(i) or the number of mismatches using Ith tile
                switch (energyCalculationMode){
                    case EnergyCalculationMode.TotalCornerMismatches:
                        numberOfMismatch = this.GetNumberOfMismatch(col,row);
                        break;
                    case EnergyCalculationMode.CountOnePerCorner:
                        numberOfMismatch = this.GetNumberOfMismatch_CountCornerMismatchAsOne(col,row);
                        break;

                }

                if (numberOfMismatch>24){
                    Console.WriteLine("numberOfMismatch = ",numberOfMismatch);
                }
                // Weight[i] = (k * ((max energy - error(i))^gamma) / (max_energy^gamma) ) + epsilon* (1.0 / number-of-tiles)
                float term1= k * (((float)Math.Pow((double)(maxEnergy-numberOfMismatch),gamma)/maxEnergyPowerGamma));
                float term2= epsilonMulInverseOfNumOfTiles;
                weight[i] =  term1+term2; 

                // Console.WriteLine($"NumberOfMismatch={numberOfMismatch}, Term 1 + Term 2 = {term1} + {term2}");
                // Console.WriteLine("tileID= "+i+", weight= "+weight[i].ToString("0.000")+$", # of mismatches={numberOfMismatch}"+$", term1= "+term1.ToString("0.000")+$", term2= "+term2.ToString("0.000"));
                // Console.WriteLine("--------------------------");
            }

            this.RemoveTile(col,row);

            return weight;
        }


        public float[] GetNormalizedProbabilityVector(float[] probabilityVector){
            float sum = 0f;
            for (int i = 0;i < probabilityVector.Length;i++){
                sum += probabilityVector[i];
            }

            float[] normalizedVector = new float[probabilityVector.Length];
            for (int i = 0;i < probabilityVector.Length;i++){
                normalizedVector[i] = probabilityVector[i]/sum;
            }

            return normalizedVector;
        }

        public float[] GetCumulativeProbabilityVector(float[] probabilityVector){
            float[] CV = probabilityVector;

            float sum = 0;
            for (int i = 0;i < CV.Length;i++){
                sum = sum + CV[i];
                CV[i]=sum;

                // Console.WriteLine("CumulativeProbability= "+CV[i]);
            }
            
            // Console.WriteLine("-------------------------------------");
            return CV;
        }

        public int ChooseTileIndexFromCumulativeProbabilityVector(float[] CumulativeProbabilityVector){
            Random rand = new Random();
            int randomInt = rand.Next(0,100);
            float randomFloat = (float)randomInt / 100f;
                
            for (int i=0;i<CumulativeProbabilityVector.Length;i++){
                if (CumulativeProbabilityVector[i]>randomFloat){
                    return i;
                }
            }

            return 0;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// Tests Algo Methods ////////////////////////////////////////////////
        public int[] GetTileSetMismatches(int col,int row, int tileSetID, EnergyCalculationMode energyCalculationMode){
            int tileID = 0;
            int numberOfMismatch = 0;
            int numberOfTiles = this.TileSet[tileSetID].Tiles.Length;
   
            int[] tileSetMismatches = new int[numberOfTiles];
            for (int i=0;i<numberOfTiles;i++){
                // Place Ith tile to the position
                tileID = i;
                this.PlaceTile(tileSetID, tileID, col,row);

                // Get error(i) or the number of mismatches using Ith tile
                switch (energyCalculationMode){
                    case EnergyCalculationMode.TotalCornerMismatches:
                        numberOfMismatch = this.GetNumberOfMismatch(col,row);
                        break;
                    case EnergyCalculationMode.CountOnePerCorner:
                        numberOfMismatch = this.GetNumberOfMismatch_CountCornerMismatchAsOne(col,row);
                        break;

                }
                

                if (numberOfMismatch>24){
                    Console.WriteLine("numberOfMismatch = ",numberOfMismatch);
                }
             
                tileSetMismatches[i]=numberOfMismatch;
            }

            this.RemoveTile(col,row);

            return tileSetMismatches;
        }

        public TileMismatch[] SortTileMismatches(int[] tileMismatches){
            TileMismatch[] tileMismatchArray = new TileMismatch[tileMismatches.Length];

            for (int i=0;i<tileMismatches.Length;i++){
                TileMismatch newTileMismatch = new TileMismatch(tileID:i,numberOfMismatches:tileMismatches[i]);
                tileMismatchArray[i] = newTileMismatch;
            }

            Array.Sort<TileMismatch>(tileMismatchArray , (x,y) => x.NumberOfMismatches.CompareTo(y.NumberOfMismatches));

            return tileMismatchArray;
        }

         public TileWeight[] SortTileWeight(float[] tileWeights){
            TileWeight[] tileWeightArray = new TileWeight[tileWeights.Length];

            for (int i=0;i<tileWeights.Length;i++){
                TileWeight newTileWeight=new TileWeight(tileID:i,weight:tileWeights[i]);
                tileWeightArray[i] = newTileWeight;
            }

            Array.Sort<TileWeight>(tileWeightArray , (x,y) => x.Weight.CompareTo(y.Weight));
            // Utils.QuickSortTileWeight(tileWeightArray,0,tileWeightArray.Length-1);
            // Descending
            Array.Reverse(tileWeightArray);

            return tileWeightArray;
        }
    }

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

    public struct TileWeight {
        public int TileID;
        public float Weight;

        public TileWeight(int tileID, float weight){
            this.TileID=tileID;
            this.Weight=weight;
        }
    }
}