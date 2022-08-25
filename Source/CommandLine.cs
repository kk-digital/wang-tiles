using Wang;
using Wang.Board;
using Wang.EdgeTile;
using Wang.Other;

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
            else if (args.Length >= 7 && args[0] == "new")
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
                    else if (args[2] == "32x32")
                    {
                        tileSize = TileSize.TileSize_32x32;
                    }

                    int verticalColorCount = Convert.ToInt32(args[3]);
                    int horizontalColorCount = Convert.ToInt32(args[4]);
                    int variant = Convert.ToInt32(args[5]);
                    int tilesPerRow = Convert.ToInt32(args[6]);

                    EdgeTileSet newTileSet = EdgeTileSet.NewWangCompleteTileset(tileSize, verticalColorCount, horizontalColorCount, variant);
                    EdgeTileSetJson.SaveJson("s00_Tileset\\tileset_" + newTileSet.Description.IDString + ".json", newTileSet);
                    newTileSet.SavePNG("s00_Tileset_Image\\tileset_" + newTileSet.Description.IDString + ".png", tilesPerRow);
                    Console.WriteLine("EdgeTileSet generated");
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

                    TileBoardJson.SaveJson("s01_Board\\board_" + tileBoard.ID.ToString() + ".json", tileBoard);
                    Console.WriteLine("board  " + "s01_Board\\board_" + tileBoard.ID.ToString() + ".json" + " generated");
                }
            }
            else if (args.Length >= 0 && args[0] == "create-scene")
            {
                List<string> listOfTilesets = new List<string>();
                string boardname = "";
                string arg = "";
                Int64 id = Other.Utils.GenerateID();
                for(int i = 1; i < args.Length; i++)
                {
                    if (args[i] == "-b" ||
                    args[i] == "-ts")
                    {
                        arg = args[i];
                    }
                    else
                    {
                        if (arg == "-b")
                        {
                            boardname = args[i];
                        }
                        else if (arg == "-ts")
                        {
                            listOfTilesets.Add(args[i]);
                        }
                    }
                }

                if (boardname != "" && listOfTilesets.Count > 0)
                {
                    Board.TileBoard board = Board.TileBoardJson.FromJson("s01_Board//" + boardname);
                    SceneW.Scene scene = new SceneW.Scene(id, board.SizeX, board.SizeY);

                    foreach(var tilesetPath in listOfTilesets)
                    {
                        EdgeTileSet tileset = EdgeTileSetJson.FromJson("s00_Tileset\\" + tilesetPath);
                        scene.AddTileSet(tileset);
                    }

                    DateTimeOffset dto = DateTimeOffset.Now;
                    Mt19937.init_genrand((ulong)dto.ToUnixTimeSeconds());

                    for(int y = 0; y < board.SizeY; y++)
                    {
                        for(int x = 0; x < board.SizeX; x++)
                        {
                            int randomTileSetID = Math.Abs((int)Mt19937.genrand_int32() % scene.TileSetsCount);
                            int randomID = Math.Abs((int)Mt19937.genrand_int32() % scene.TileSets[randomTileSetID].InformationArray.Length);

                            EdgeTileInformation tileInfo = scene.TileSets[randomTileSetID].InformationArray[randomID];

                            scene.SetTile(x, y, Layer.LayerFront, new SceneW.SceneTile(x, y, randomID, randomTileSetID, TileIsoType.FullBlock, TileType.TileTypeWang));
                        }
                    }


                    scene.SaveJson("s03_OutputScene\\" + "scene_" + id + ".json");
                    scene.SavePNG("s03_OutputScene\\" + "scene_" + id + ".png");
                    Console.WriteLine("test scene generated");

                    Console.WriteLine("scene  " + "s03_OutputScene\\" + "scene_" + id + ".json" + "  created !");
                }
                else
                {
                    Console.WriteLine("invalid parameters!");
                }
            }
            else if (args.Length >= 1 && args[0] == "test-scene-output-random")
            {
                List<string> listOfTilesets = new List<string>();
                int width = 4;
                int height = 4;
                Int64 newId = Utils.GenerateID();
                string outputPath = "scene_" + newId;
                string arg = "";
                for(int i = 1; i < args.Length; i++)
                {
                    if (args[i] == "-ts")
                    {
                        arg = "-ts";
                    }
                    else if (args[i] == "-width")
                    {
                        arg = "-width";
                    }
                    else if (args[i] == "-height")
                    {
                        arg = "-height";
                    }
                    else if (args[i] == "-out")
                    {
                        arg = "-out";
                    }
                    else
                    {
                        if (arg == "-ts")
                        {
                            listOfTilesets.Add(args[i]);
                            arg = "";
                        }
                        else if (arg == "-width")
                        {
                            width = Convert.ToInt32(args[i]);
                            arg = "";
                        }
                        else if (arg == "-height")
                        {
                            height = Convert.ToInt32(args[i]);
                            arg = "";
                        }
                        else if (arg == "-out")
                        {
                            outputPath = args[i];
                            arg = "";
                        }
                    }
                }

                if (listOfTilesets.Count == 0)
                {
                    Console.WriteLine("no tilesets chosen !!");
                    return ;
                }

                SceneW.Scene scene = new SceneW.Scene(newId, width, height);
                foreach(var tilesetPath in listOfTilesets)
                {
                    EdgeTileSet tileset = EdgeTileSetJson.FromJson("s00_Tileset\\" + tilesetPath);
                    scene.AddTileSet(tileset);
                }

                DateTimeOffset dto = DateTimeOffset.Now;
                Mt19937.init_genrand((ulong)dto.ToUnixTimeSeconds());

                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        int randomTileSetID = Math.Abs((int)Mt19937.genrand_int32() % scene.TileSetsCount);
                        int randomID = Math.Abs((int)Mt19937.genrand_int32() % scene.TileSets[randomTileSetID].InformationArray.Length);

                        EdgeTileInformation tileInfo = scene.TileSets[randomTileSetID].InformationArray[randomID];

                        scene.SetTile(x, y, Layer.LayerFront, new SceneW.SceneTile(x, y, randomID, randomTileSetID, TileIsoType.FullBlock, TileType.TileTypeWang));
                    }
                }


                scene.SaveJson("s03_OutputScene\\" + outputPath + ".json");
                scene.SavePNG("s03_OutputScene\\" + outputPath + ".png");
                Console.WriteLine("test scene " + "s03_OutputScene\\" + outputPath + ".json" + " generated");
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
            Console.WriteLine("*new tileset <tilesize> <vertical_color_count> <horizontal_color_count> <variant> <tilesPerRow>");
            Console.WriteLine("****** Board Generate ***");
            Console.WriteLine("Board Generate flat <xSize> <ySize>");
            Console.WriteLine("Board Generate Radial  <xSize> <ySize>");
            Console.WriteLine("Board Generate FloatingIsland 3x3");
            Console.WriteLine("****** Scene ******");
            Console.WriteLine("test-scene -b <board> -ts <tileset1> -ts <tileset2>");
            Console.WriteLine("test-scene-output-random -ts <tileset_name1> -ts <tileset_name2> -width <sizeX> -height <sizeY> -out <outpath>");
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