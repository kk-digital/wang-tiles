
using wang;

class MainClass
{
    static void Main(string[] args)
    {
<<<<<<< HEAD
        Console.Write("Main" + "\n");

=======
>>>>>>> 7328cc359a415149928c312a32ce36ce43725f44
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

        Int64 id = Utils.CreateID(10, 20);
        Int32 time = Utils.GetTimeFromID(id);
        Int32 randomNumber = Utils.GetRandomNumberFromID(id);

        Console.Write(id + "\n");
        Console.Write(time + "\n");
        Console.Write(randomNumber + "\n");


        //palette.GetHorizontalColor(4);
    }
}