
using Wang;
using Wang.Other;
using Wang.SceneW;
using Wang.Board;
using Wang.CLI;
using Wang.EdgeTile;
using System.Text.Json;
using System.Text.Json.Serialization;

class MainClass
{
    static void Main(string[] args)
    {
        Console.Write("Main" + "\n");
        Int64 id = Utils.CreateID(20, 10);
        Int32 time = Utils.GetTimeFromID(id);
        Int32 randomNumber = Utils.GetRandomNumberFromID(id);

        Console.Write(id + "\n");
        Console.Write(time + "\n");
        Console.Write(randomNumber + "\n");

        Scene scene = new Scene(Utils.GenerateID(), 5, 5);
        EdgeTileSet newTileSet = EdgeTileSet.NewWangCompleteTileset(TileSize.TileSize_16x16, 4, 4, 1);
        newTileSet.SavePNG("png.png");

        

        ColorPaleteMap palette = new ColorPaleteMap();
        palette.Initialize();

        CommandLine.ExecuteCommand(args);

/*entry:
        Console.Write("Inputs: list tilesets, list boards, list scenes, list pixelassignments, list outputscenes, new tileset\n");
=======
entry:
        Console.Write("Inputs: wang_tiles list tilesets, list boards, list scenes, list pixelassignments, list outputscenes, new tileset\n");
>>>>>>> c70c789918176d8531dddfdfdb37463e61c720ff

        input = Console.ReadLine();

        if(input.ToLower() == "wang_tiles new tileset")
        {
            string text = JsonSerializer.Serialize("TODO: Not implemented yet.");
            File.WriteAllText("data//s00_Tileset//tileset.json", text);

            Console.Clear();
            Console.WriteLine("Tileset Created.");

            goto entry;
        }
        else if(input.ToLower() == "wang_tiles list tilesets")
        {
            string[] files = Directory.GetFiles("data//s00_Tileset//");

            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(i.ToString() + "  " + files[i] + "\n");
            }

            if(files.Length <= 0)
            {
                Console.WriteLine("Folder Is Empty.");
            }

            goto entry;
        }
        else if(input.ToLower() == "wang_tiles list boards")
        {
            string[] files = Directory.GetFiles("data//s01_Board//");

            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(i.ToString() + "  " + files[i] + "\n");
            }
            
            if(files.Length <= 0)
            {
                Console.WriteLine("Folder Is Empty.");
            }

            goto entry;
        }
        else if(input.ToLower() == "wang_tiles list scenes")
        {
            string[] files = Directory.GetFiles("data//s02_Scene//");

            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(i.ToString() + "  " + files[i] + "\n");
            }
            
            if(files.Length <= 0)
            {
                Console.WriteLine("Folder Is Empty.");
            }

            goto entry;
        }
        else if(input.ToLower() == "wang_tiles list pixelassignments")
        {
            string[] files = Directory.GetFiles("data//s02_TilesetPixelAssignment//");

            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(i.ToString() + "  " + files[i] + "\n");
            }

            if(files.Length <= 0)
            {
                Console.WriteLine("Folder Is Empty.");
            }

            goto entry;
        }
        else if(input.ToLower() == "wang_tiles list outputscenes")
        {
            string[] files = Directory.GetFiles("data//s03_OutputScene//");

            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(i.ToString() + "  " + files[i] + "\n");
            }

            if(files.Length <= 0)
            {
                Console.WriteLine("Folder Is Empty.");
            }

            goto entry;
        }
        else if(input.ToLower() == "clear")
        {
            Console.Clear();
            goto entry;
        }
        else if(input.ToLower() == "exit")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.Clear();
            goto entry;
        }*/
    }
}