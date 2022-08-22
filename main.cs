using wang_tiles;

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


        TileBoard tileBoard = new TileBoard(2, 2);
        TileBoardJson.SaveJson("board.json", tileBoard);

        ColorPaleteMap palette = new ColorPaleteMap();
        palette.Initialize();
        string input;

entry:
        Console.Write("Inputs: list tileset, list board, list scene, list pixelassignment, list outputscene\n");

        input = Console.ReadLine();

        if(input.ToLower() == "exit")
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