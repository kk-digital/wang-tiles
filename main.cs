
using wang_tiles;
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


        TileBoard tileBoard = new TileBoard(Utils.GenerateID(), 2, 2);
        TileBoardJson.SaveJson("board.json", tileBoard);

        ColorPaleteMap palette = new ColorPaleteMap();
        palette.Initialize();
        string input;

entry:
        Console.Write("Inputs: list tilesets, list boards, list scenes, list pixelassignments, list outputscenes, new tileset\n");

        input = Console.ReadLine();

        if(input.ToLower() == "new tileset")
        {
            string text = JsonSerializer.Serialize("TODO: Not implemented yet.");
            File.WriteAllText("data//s00_Tileset//tileset.json", text);

            Console.Clear();
            Console.WriteLine("Tileset Created.");

            goto entry;
        }
        else if(input.ToLower() == "list tilesets")
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
        else if(input.ToLower() == "list boards")
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
        else if(input.ToLower() == "list scenes")
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
        else if(input.ToLower() == "list pixelassignments")
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
        else if(input.ToLower() == "list outputscenes")
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
        }
    }
}