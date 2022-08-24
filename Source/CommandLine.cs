using Wang;
using Wang.Board;
using Wang.EdgeTile;

namespace Wang.CLI
{
    public static class CommandLine
    {
        public static void ExecuteCommand(string[] args)
        {


            if (args.Length >= 1 && args[0] == "help")
            {
                ExecuteHelp();
            }
            else if (args.Length >= 2 && args[0] == "list")
            {
                if (args[1] == "tilesets")
                {
                    ListFolder("s00_Tileset\\", ".json");
                }
                else if (args[1] == "boards")
                {
                    ListFolder("s01_Board//", ".json");
                }
                else if (args[1] == "scenes")
                {
                    ListFolder("s02_Scene//", ".json");
                }
                else if (args[1] == "pixelassignments")
                {
                    ListFolder("s02_TilesetPixelAssignment//", ".json");
                }

            }
            else if (args.Length >= 6 && args[0] == "new")
            {
                if (args[1] == "tileset")
                {
                    TileSize tileSize = TileSize.TileSize_16x16;
                    if (args[2] == "8x8")
                    {
                        tileSize = TileSize.TileSize_8x8;
                    }
                    else if (args[2] == "16x16")
                    {
                        tileSize = TileSize.TileSize_16x16;
                    }
                    else if (args[3] == "32x32")
                    {
                        tileSize = TileSize.TileSize_32x32;
                    }

                    int verticalColorCount = Convert.ToInt32(args[3]);
                    int horizontalColorCount = Convert.ToInt32(args[4]);
                    int variant = Convert.ToInt32(args[5]);

                    EdgeTileSet newTileSet = EdgeTileSet.NewWangCompleteTileset(tileSize, verticalColorCount, horizontalColorCount, variant);
                    EdgeTileSetJson.SaveJson("s00_Tileset\\tileset_" + newTileSet.Description.IDString + ".json", newTileSet);
                }
            }
            else if (args.Length >= 5 && args[0] == "Board")
            {
                if (args[1] == "Generate")
                {
                    TileBoard tileBoard;
                     
                    int x = Convert.ToInt32(args[3]);
                    int y = Convert.ToInt32(args[4]);


                    switch (args[2])
                    {
                        case "flat":
                            tileBoard = TileBoard.MakeFlat(x, y);
                            break;
                        case "Radial":
                            tileBoard = TileBoard.MakeRadial(x, y);
                            break;
                        case "FloatingIsland":
                            tileBoard = TileBoard.MakeFloatingIsland3x3();
                            break;
                        default:
                            Console.WriteLine("Error: Not valid Generate function."); // (Todo(Joao) do help when not valid arguments?)
                            return;
                    }

                    TileBoardJson.SaveJson("s01_Board\\board" + tileBoard.ID.ToString(), tileBoard);
                }
            }
        }


        public static void ExecuteHelp()
        {
            Console.WriteLine("Thank you for choosing Help!");
            Console.WriteLine("here is a list of command you can use");
            Console.WriteLine("****** list ***");
            Console.WriteLine("*list tilesets");
            Console.WriteLine("*list boards");
            Console.WriteLine("*list scenes");
            Console.WriteLine("*list pixelassignments");
            Console.WriteLine("****** new ***");
            Console.WriteLine("*new tileset <tilesize> <vertical_color_count> <horizontal_color_count> <variant>");
            Console.WriteLine("****** Board Generate ***");
            Console.WriteLine("Board Generate flat <xSize> <ySize>");
            Console.WriteLine("Board Generate Radial  <xSize> <ySize>");
            Console.WriteLine("Board Generate FloatingIsland 3x3");
        }

        public static void ListFolder(string folder, string extension)
        {
            string[] files = Directory.GetFiles(Constants.OutputPath + "\\" + folder);

            int numberOfFiles = files.Length;
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(i.ToString() + "  " + files[i] + "\n");
            }

            if (numberOfFiles == 0)
            {
                Console.WriteLine("Folder Is Empty.");
            }
        }
    }
}