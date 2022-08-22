
using wang;

class MainClass
{
    static void Main(string[] args)
    {
        Console.Write("Main" + "\n");

        ColorPaleteMap palette = new ColorPaleteMap();
        palette.Initialize();

        Int64 id = Utils.CreateID(10, 20);
        Int32 time = Utils.GetTimeFromID(id);
        Int32 randomNumber = Utils.GetRandomNumberFromID(id);

        Console.Write(id + "\n");
        Console.Write(time + "\n");
        Console.Write(randomNumber + "\n");


        //palette.GetHorizontalColor(4);
    }
}