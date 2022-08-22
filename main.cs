using wang;
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

        ColorPaleteMap palette = new ColorPaleteMap();
        palette.Initialize();
        string input;

entry:
        Console.Write("Inputs: list tilesets, list boards, list scenes, list pixelassignments, list outputscenes, new tileset\n");

        input = Console.ReadLine();

        if(input.ToLower() == "new tileset")
        {
            string text = JsonSerializer.Serialize("TODO: Not implemented yet.");
            File.WriteAllText("..//..//..//data//s00_Tileset//tileset.json", text);

            Console.Clear();
            Console.WriteLine("Tileset Created.");

            goto entry;
        }
        else if(input.ToLower() == "list tilesets")
        {
            string[] files = Directory.GetFiles("..//..//..//data//s00_Tileset//");

            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(i.ToString() + "  " + files[i] + "\n");
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

        //palette.GetHorizontalColor(4);
    }
}