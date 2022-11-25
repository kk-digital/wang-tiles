namespace WangTile
{
    public class WangTileSet{
        public CornerColorData[]? CornerColors;
        public HorizontalColorData[]? HorizontalColors;
        public VerticalColorData[]? VerticalColors;

        public WangTile[]? Tiles;

        public WangTileSet(){
            this.CornerColors=new CornerColorData[1];
            this.VerticalColors=new VerticalColorData[1];
            this.HorizontalColors=new HorizontalColorData[1];
        }

        /* Methods of WangTileSet */
        public int CreateTile(ColorMap colorMap, CornerColor cornerColorNW, CornerColor cornerColorNE, CornerColor cornerColorSE, CornerColor cornerColorSW, VerticalColor edgeColorN, HorizontalColor edgeColorE, VerticalColor edgeColorS, HorizontalColor edgeColorW)
        {
            WangTile newTile = new WangTile(cornerColorNW, cornerColorNE, cornerColorSE, cornerColorSW, edgeColorN, edgeColorE,edgeColorS, edgeColorW); 

            if (this.Tiles==null){
                this.Tiles=new WangTile[1];
                newTile.TileID=0;
                this.Tiles[0]=newTile;
            } else {
                newTile.TileID=this.Tiles.Length;
                this.Tiles=this.Tiles.Append(newTile).ToArray();
            }

            AddCornerColorData(cornerColorNW,colorMap);
            AddCornerColorData(cornerColorNE,colorMap);
            AddCornerColorData(cornerColorSE,colorMap);
            AddCornerColorData(cornerColorSW,colorMap);

            AddVerticalColorData(edgeColorN,cornerColorNW,cornerColorNE,colorMap);
            AddVerticalColorData(edgeColorS,cornerColorSW,cornerColorSE,colorMap);

            AddHorizontalColorData(edgeColorW,cornerColorNW,cornerColorSW,colorMap);
            AddHorizontalColorData(edgeColorE,cornerColorNE,cornerColorSE,colorMap);

            // return TileID
            return this.Tiles.Length-1;
        }

        void AddCornerColorData(CornerColor cornerColor, ColorMap colorMap){
            // if corner color already exists
            if ((this.CornerColors!=null) &&(this.CornerColors.Length>=(int)cornerColor+1)){
                // then increment number of times used
                this.CornerColors[(int)cornerColor].NumberOfTimesUsed++;
            }else{
                // then does not exist, add it
                CornerColorData cornerColorData = new CornerColorData(cornerColor);

                // Assign new color palette
                // Use color count as color pallete
                // cornerColorData.ColorPalette=colorMap.GetColorCount();
                cornerColorData.ColorPalette=(int)cornerColor;
                colorMap.IncrementColorCount();

                if (this.CornerColors==null){
                    this.CornerColors=new CornerColorData[1];
                    this.CornerColors[0]=cornerColorData;
                }else{
                    this.CornerColors=this.CornerColors.Append(cornerColorData).ToArray();
                }

                
            }
        }

        void AddHorizontalColorData(HorizontalColor hColor,CornerColor cColor1,CornerColor cColor2,ColorMap colorMap){
            // if corner color already exists
            if ((this.HorizontalColors!=null) &&(this.HorizontalColors.Length>=(int)hColor+1)){
                // then increment number of times used
                this.HorizontalColors[(int)hColor].NumberOfTimesUsed++;
            }else{
                // then does not exist, add it
                HorizontalColorData horizontalColorData = new HorizontalColorData(hColor, cColor1,cColor2);

                // Assign new color palette
                // Use color count as color pallete
                // horizontalColorData.ColorPalette=colorMap.GetColorCount();
                horizontalColorData.ColorPalette=(int)hColor;
                colorMap.IncrementColorCount();

                if (this.HorizontalColors==null){
                    this.HorizontalColors=new HorizontalColorData[1];
                    this.HorizontalColors[0]=horizontalColorData;
                }else{
                    this.HorizontalColors=this.HorizontalColors.Append(horizontalColorData).ToArray();
                } 
            }
        }

        void AddVerticalColorData(VerticalColor vColor,CornerColor cColor1,CornerColor cColor2,ColorMap colorMap){
            // if corner color already exists
            if ((this.VerticalColors!=null) &&(this.VerticalColors.Length>=(int)vColor+1)){
                // then increment number of times used
                this.VerticalColors[(int)vColor].NumberOfTimesUsed++;
            }else{
                // then does not exist, add it
                VerticalColorData verticalColorData = new VerticalColorData(vColor, cColor1,cColor2);

                // Assign new color palette
                // Use color count as color pallete
                // verticalColorData.ColorPalette=colorMap.GetColorCount();
                verticalColorData.ColorPalette=(int)vColor;
                colorMap.IncrementColorCount();

                if (this.VerticalColors==null){
                    this.VerticalColors=new VerticalColorData[1];
                    this.VerticalColors[0]=verticalColorData;
                }else{
                    this.VerticalColors=this.VerticalColors.Append(verticalColorData).ToArray();
                } 
            }
        }

        public int GetCornerColorPalette(CornerColor cColor){
            return this.CornerColors[(int)cColor].ColorPalette;
        }

        public int GetHorizontalColorPalette(HorizontalColor hColor){
            return this.HorizontalColors[(int)hColor].ColorPalette;
        }

        public int GetVerticalColorPalette(VerticalColor vColor){
            return this.VerticalColors[(int)vColor].ColorPalette;
        }
    }
}