using System.Text.Json;

namespace WangTile
{
    public class WangTileJSON
    {
        public string CornerColorNW { get; set; }
        public string CornerColorNE { get; set; }
        public string CornerColorSE { get; set; }
        public string CornerColorSW { get; set; }

        public string EdgeColorNorth { get; set; }
        public string EdgeColorSouth { get; set; }

        public string EdgeColorWest { get; set; }
        public string EdgeColorEast { get; set; }

        public bool BitmaskAllCorners { get; set; }
        public bool BitmaskNorthEdge { get; set; }
        public bool BitmaskEastEdge { get; set; }
        public bool BitmaskSouthEdge { get; set; }
        public bool BitmaskWestEdge { get; set; }

        public static void TestJSON(){

            // Serialize test
            var options = new JsonSerializerOptions { WriteIndented = true };
            WangTileJSON[] wangArray1 = new WangTileJSON[0];

            var wangTileJSON = new WangTileJSON {
                CornerColorNW="AA",
                CornerColorNE="AB"
            };

            wangArray1 = wangArray1.Append(wangTileJSON).ToArray();
            string jsonString1 = JsonSerializer.Serialize<WangTileJSON[]>(wangArray1,options);
            Console.WriteLine(jsonString1);

            // Deserialize test
            string fileName = "./data/json/test.json";
            string jsonString = File.ReadAllText(fileName);
            WangTileJSON[] wangArray = JsonSerializer.Deserialize<WangTileJSON[]>(jsonString)!;
            Console.WriteLine($"WangTile[0] CornerColorNW={wangArray[0].CornerColorNW}");
            Console.WriteLine($"WangTile[0] CornerColorNE={wangArray[0].CornerColorNE}");
            Console.WriteLine($"WangTile[0] CornerColorSE={wangArray[0].CornerColorSE}");
            Console.WriteLine($"WangTile[0] CornerColorSW={wangArray[0].CornerColorSW}");

            Console.WriteLine($"WangTile[0] EdgeColorNorth={wangArray[0].EdgeColorNorth}");
            Console.WriteLine($"WangTile[0] EdgeColorEast={wangArray[0].EdgeColorEast}");
            Console.WriteLine($"WangTile[0] EdgeColorSouth={wangArray[0].EdgeColorSouth}");
            Console.WriteLine($"WangTile[0] EdgeColorWest={wangArray[0].EdgeColorWest}");

            Console.WriteLine($"WangTile[0] BitmaskAllCorners={wangArray[0].BitmaskAllCorners}");
            Console.WriteLine($"WangTile[0] BitmaskNorthEdge={wangArray[0].BitmaskNorthEdge}");
            Console.WriteLine($"WangTile[0] BitmaskEastEdge={wangArray[0].BitmaskEastEdge}");
            Console.WriteLine($"WangTile[0] BitmaskSouthEdge={wangArray[0].BitmaskSouthEdge}");
            Console.WriteLine($"WangTile[0] BitmaskWestEdge={wangArray[0].BitmaskWestEdge}");


            Console.WriteLine($"WangTile[1] CornerColorNW={wangArray[1].CornerColorNW}");
            Console.WriteLine($"WangTile[1] CornerColorNE={wangArray[1].CornerColorNE}");
            Console.WriteLine($"WangTile[1] CornerColorSE={wangArray[1].CornerColorSE}");
            Console.WriteLine($"WangTile[1] CornerColorSW={wangArray[1].CornerColorSW}");

            Console.WriteLine($"WangTile[1] EdgeColorNorth={wangArray[1].EdgeColorNorth}");
            Console.WriteLine($"WangTile[1] EdgeColorEast={wangArray[1].EdgeColorEast}");
            Console.WriteLine($"WangTile[1] EdgeColorSouth={wangArray[1].EdgeColorSouth}");
            Console.WriteLine($"WangTile[1] EdgeColorWest={wangArray[1].EdgeColorWest}");

            Console.WriteLine($"WangTile[1] BitmaskAllCorners={wangArray[1].BitmaskAllCorners}");
            Console.WriteLine($"WangTile[1] BitmaskNorthEdge={wangArray[1].BitmaskNorthEdge}");
            Console.WriteLine($"WangTile[1] BitmaskEastEdge={wangArray[1].BitmaskEastEdge}");
            Console.WriteLine($"WangTile[1] BitmaskSouthEdge={wangArray[1].BitmaskSouthEdge}");
            Console.WriteLine($"WangTile[1] BitmaskWestEdge={wangArray[1].BitmaskWestEdge}");
        }
    }

   
}