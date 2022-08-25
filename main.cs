
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
        Scene scene = Scene.Algorithm1(TileSize.TileSize_16x16);

        scene.TileSets[0].SavePNG("MatchingCornerTileset.png", 8);

        CommandLine.ExecuteCommand(args);
    }
}