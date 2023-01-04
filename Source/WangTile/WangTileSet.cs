namespace WangTile
{
    public class WangTileSet{
        public Dictionary < CornerColor, CornerColorData > CornerColors;
        public Dictionary < HorizontalColor, HorizontalColorData > HorizontalColors;
        public Dictionary < VerticalColor, VerticalColorData > VerticalColors;

        public WangTile[]? Tiles;

        public WangTileSet(){
            this.CornerColors = new Dictionary < CornerColor, CornerColorData  > ();
            this.VerticalColors = new Dictionary < VerticalColor, VerticalColorData  > ();
            this.HorizontalColors = new Dictionary < HorizontalColor, HorizontalColorData  > ();
        }

        /* Methods of WangTileSet */
        public int CreateTile(ColorMap colorMap, CornerColor cornerColorNW, CornerColor cornerColorNE, CornerColor cornerColorSE, CornerColor cornerColorSW, VerticalColor edgeColorN, HorizontalColor edgeColorE, VerticalColor edgeColorS, HorizontalColor edgeColorW, int[] tileData = null)
        {
            WangTile newTile = new WangTile(cornerColorNW, cornerColorNE, cornerColorSE, cornerColorSW, edgeColorN, edgeColorE,edgeColorS, edgeColorW, tileData); 

            if (this.Tiles==null){
                this.Tiles=new WangTile[1];
                newTile.TileID=0;
                this.Tiles[0]=newTile;
            } else {
                newTile.TileID=this.Tiles.Length;
                this.Tiles=this.Tiles.Append(newTile).ToArray();
            }

            this.AddCornerColorData(cornerColorNW,colorMap);
            this.AddCornerColorData(cornerColorNE,colorMap);
            this.AddCornerColorData(cornerColorSE,colorMap);
            this.AddCornerColorData(cornerColorSW,colorMap);

            this.AddVerticalColorData(edgeColorN,cornerColorNW,cornerColorNE,colorMap);
            this.AddVerticalColorData(edgeColorS,cornerColorSW,cornerColorSE,colorMap);

            this.AddHorizontalColorData(edgeColorW,cornerColorNW,cornerColorSW,colorMap);
            this.AddHorizontalColorData(edgeColorE,cornerColorNE,cornerColorSE,colorMap);

            // return TileID
            return this.Tiles.Length-1;
        }

        void AddCornerColorData(CornerColor cornerColor, ColorMap colorMap){
            // if corner color already exists
            if (this.CornerColors.ContainsKey(cornerColor)){
                // then increment number of times used
               CornerColorData cColor = this.CornerColors[cornerColor];
               cColor.NumberOfTimesUsed++;
               this.CornerColors[cornerColor]=cColor;
            } else {
                // then does not exist, add it
                CornerColorData cornerColorData = new CornerColorData(cornerColor);

                // Assign new color palette
                // Use color count as color pallete
                cornerColorData.ColorPalette=colorMap.GetCornerColorCount();
                // cornerColorData.ColorPalette=(int)cornerColor;
                colorMap.IncrementCornerColorCount();

                this.CornerColors[cornerColor]=cornerColorData;
            }
        }

        void AddHorizontalColorData(HorizontalColor hColor,CornerColor cColor1,CornerColor cColor2,ColorMap colorMap){
            // if horizontal color already exists
            if (this.HorizontalColors.ContainsKey(hColor)){
                // then increment number of times used
                HorizontalColorData horizontalColor = this.HorizontalColors[hColor];
                horizontalColor.NumberOfTimesUsed++;
                this.HorizontalColors[hColor]= horizontalColor;
            } else {
                // then does not exist, add it
                HorizontalColorData horizontalColorData = new HorizontalColorData(hColor, cColor1,cColor2);

                // Assign new color palette
                // Use color count as color pallete
                horizontalColorData.ColorPalette=colorMap.GetHorizontalColorCount();
                // horizontalColorData.ColorPalette=(int)hColor;
                colorMap.IncrementHorizontalColorCount();

                this.HorizontalColors[hColor] = horizontalColorData;
            }
        }

        void AddVerticalColorData(VerticalColor vColor,CornerColor cColor1,CornerColor cColor2,ColorMap colorMap){
            // if corner color already exists
            if (this.VerticalColors.ContainsKey(vColor)){
                // then increment number of times used
                VerticalColorData verticalColor = this.VerticalColors[vColor];
                verticalColor.NumberOfTimesUsed++;
                this.VerticalColors[vColor] = verticalColor;
            }else{
                // then does not exist, add it
                VerticalColorData verticalColorData = new VerticalColorData(vColor, cColor1,cColor2);

                // Assign new color palette
                // Use color count as color pallete
                verticalColorData.ColorPalette=colorMap.GetVerticalColorCount();
                // verticalColorData.ColorPalette=(int)vColor;
                colorMap.IncrementVerticalColorCount();

                this.VerticalColors[vColor] = verticalColorData;
                 
            }
        }

        public int GetCornerColorPalette(CornerColor cColor){
            return this.CornerColors[cColor].ColorPalette;
        }

        public int GetHorizontalColorPalette(HorizontalColor hColor){
            return this.HorizontalColors[hColor].ColorPalette;
        }

        public int GetVerticalColorPalette(VerticalColor vColor){
            return this.VerticalColors[vColor].ColorPalette;
        }
    }
}