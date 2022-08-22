
using wang;

class MainClass
{
    static void Main(string[] args)
    {
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