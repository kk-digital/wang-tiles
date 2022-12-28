using System.Text.Json;

namespace WangTile
{
    public struct TileJSON
    {
       public TileLayerJSON[] Layers { get; set; }
    }

    public struct TileLayerJSON
    {
       public TileChunksJSON[] Chunks { get; set; }
    }

    public struct TileChunksJSON
    {
       public int[] Data { get; set; }
       public int Height { get; set; }
       public int Width { get; set; }
       public int X { get; set; }
       public int Y { get; set; }
    }

    public class WangTileJSON
    {
        public static void TestTiledJSON(){
            TileJSON tile =  DeserializeJSON("./data/json/Map_Tiles_V1.tmj");
            Console.WriteLine($"tile array len= {tile.Layers.Length}");
            Console.WriteLine($"tile array[0] chunks len= {tile.Layers[0].Chunks.Length}");

            CheckCornerMarkers(tile.Layers[0].Chunks[0]);

            // Get north color
            int northColor = GetNorthColorFromTileChunks(tile.Layers[0].Chunks[0]);
            // Get east color
            int eastColor = GetEastColorFromTileChunks(tile.Layers[0].Chunks[0]);
            // Get south color
            int southColor = GetSouthColorFromTileChunks(tile.Layers[0].Chunks[0]);
            // Get west color
            int westColor = GetWestColorFromTileChunks(tile.Layers[0].Chunks[0]);
            
        }

        public static TileJSON DeserializeJSON(string jsonDirectory){
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string jsonString = File.ReadAllText(jsonDirectory);
            TileJSON tile = JsonSerializer.Deserialize<TileJSON>(jsonString, options);

            return tile;
        }

        public static int GetNorthColorFromTileChunks(TileChunksJSON tileChunk){        
            // North edge
            for (int i=1;i<17;i++){
                int indexNorth = Utils.GetBoardSlotIndex(tileChunk.Width, 0, i);
                if (tileChunk.Data[indexNorth]!=0) {
                    return tileChunk.Data[indexNorth];
                }
            }

            return 0;
        }

        public static int GetEastColorFromTileChunks(TileChunksJSON tileChunk){        
            // East edge
            for (int i=1;i<17;i++){
                int indexEast = Utils.GetBoardSlotIndex(tileChunk.Width, i, 17);
                if (tileChunk.Data[indexEast]!=0) {
                    return tileChunk.Data[indexEast];
                }
            }

            return 0;
        }

        public static int GetSouthColorFromTileChunks(TileChunksJSON tileChunk){        
            // South edge
            for (int i=1;i<17;i++){
                int indexSouth = Utils.GetBoardSlotIndex(tileChunk.Width, 17, i);
                if (tileChunk.Data[indexSouth]!=0) {
                    return tileChunk.Data[indexSouth];
                }
            }

            return 0;
        }

        public static int GetWestColorFromTileChunks(TileChunksJSON tileChunk){        
            // West edge
            for (int i=1;i<17;i++){
                int indexWest = Utils.GetBoardSlotIndex(tileChunk.Width, i, 0);
                if (tileChunk.Data[indexWest]!=0) {
                    return tileChunk.Data[indexWest];
                }
            }

            return 0;
        }

        public static void CheckCornerMarkers(TileChunksJSON tileChunk){
            int correctHeight = 18;
            int correctWidth = 18;
            // Check tile Width and Height
            if (tileChunk.Width!=correctWidth || tileChunk.Height!=correctHeight){
                Console.WriteLine($"Got height={tileChunk.Height} and width={tileChunk.Width}, should be height={correctHeight}, width={correctWidth}");
            }
            
            // Check corner markers
            int cornerMarker=0;
            int[] tileData = tileChunk.Data;
            
            // first tileData is the NW corner marker
            cornerMarker = tileData[0];

            // the other corner markers
            int indexNE = Utils.GetBoardSlotIndex(tileChunk.Width, 0, 17);
            int indexSE = Utils.GetBoardSlotIndex(tileChunk.Width, 17, 17);
            int indexSW = Utils.GetBoardSlotIndex(tileChunk.Width, 17, 0);

            if(tileData[indexNE]!=cornerMarker || tileData[indexSE]!=cornerMarker || tileData[indexSW]!=cornerMarker){
                Console.WriteLine($"{cornerMarker}, {tileData[indexNE]}, {tileData[indexSE]}, {tileData[indexSW]}");

                Console.WriteLine("One of the corners are of different corner marker");
            }

            // Check if edge colors are the same, no two edge colors in one edge            
            // North edge
            int northEdgeColor=0;
            for (int i=1;i<17;i++){
                int indexNorth = Utils.GetBoardSlotIndex(tileChunk.Width, 0, i);
                if (tileData[indexNorth]!=0) {
                    if (northEdgeColor==0){
                        // Assign color 
                        northEdgeColor=tileData[indexNorth];
                    } else {
                        // Compare color
                        if (northEdgeColor!=tileData[indexNorth]){
                            Console.WriteLine("North Edge have more than one colors");
                        }
                    }
                }
            }

            // East edge
            int eastEdgeColor=0;
            for (int i=1;i<17;i++){
                int indexEast = Utils.GetBoardSlotIndex(tileChunk.Width, i, 17);
                if (tileData[indexEast]!=0) {
                    if (eastEdgeColor==0){
                        // Assign color 
                        eastEdgeColor=tileData[indexEast];
                    } else {
                        // Compare color
                        if (eastEdgeColor!=tileData[indexEast]){
                            Console.WriteLine("East Edge have more than one colors");
                        }
                    }
                }
            }

            // South edge
            int southEdgeColor=0;
            for (int i=1;i<17;i++){
                int indexSouth = Utils.GetBoardSlotIndex(tileChunk.Width, 17, i);
                if (tileData[indexSouth]!=0) {
                    if (southEdgeColor==0){
                        // Assign color 
                        southEdgeColor=tileData[indexSouth];
                    } else {
                        // Compare color
                        if (southEdgeColor!=tileData[indexSouth]){
                            Console.WriteLine("South Edge have more than one colors");
                        }
                    }
                }
            }

            // West edge
            int westEdgeColor=0;
            for (int i=1;i<17;i++){
                int indexWest = Utils.GetBoardSlotIndex(tileChunk.Width, i, 0);
                if (tileData[indexWest]!=0) {
                    if (westEdgeColor==0){
                        // Assign color 
                        westEdgeColor=tileData[indexWest];
                    } else {
                        // Compare color
                        if (westEdgeColor!=tileData[indexWest]){
                            Console.WriteLine("West Edge have more than one colors");
                        }
                    }
                }
            }
          
        }


    }
}