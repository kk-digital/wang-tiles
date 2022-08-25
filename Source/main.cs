
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
        // EdgeTileSet newTileSet = EdgeTileSet.NewWangCompleteTileset(TileSize.TileSize_16x16, 2, 2, 1);

        // newTileSet.SavePNG("tileset.png", 8);
        // scene.AddTileSet(newTileSet);

        SceneTile[] tileList = scene.FindMatchingCorners(TileSize.TileSize_16x16, 255, 0, 32, 125);
        for(int i = 0; i < tileList.Length; i++) 
        {
            scene.SetTile(tileList[i].xPosition, tileList[i].yPosition, Layer.LayerFront, tileList[i]);
        }
        
        scene.SavePNG("..\\Output.png");

        ColorPaleteMap palette = new ColorPaleteMap();
        palette.Initialize();

        CommandLine.ExecuteCommand(args);
    }
}