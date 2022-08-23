namespace wang_tiles
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
                    ListFolder("ds01_Board//", ".json");
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
            Console.WriteLine("*new tileset <tilesize>");
        }

        public static void ListFolder(string folder, string extension)
        {
            string[] files = Directory.GetFiles(Constants.OutputPath + "\\" + folder);

            int numberOfFiles = files.Length;
            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(i.ToString() + "  " + files[i] + "\n");
            }

            if(numberOfFiles == 0)
            {
                Console.WriteLine("Folder Is Empty.");
            }
        }
    }
}