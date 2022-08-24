using Newtonsoft.Json;

namespace Wang.EdgeTile
{

    public class EdgeTileSetJson
    {


        public static string SaveJson(string filename, EdgeTileSet edgeTileSet)
        {
            //TODO(Mahdi): Implement

            var json = JsonConvert.SerializeObject(edgeTileSet, Formatting.Indented);

            File.WriteAllText(Constants.OutputPath + "\\" + filename, json);

            return json;
        }

        public static EdgeTileSet FromJson(string filename)
        {
            string json = File.ReadAllText(Constants.OutputPath + "\\" + filename);

            EdgeTileSet newBoard = JsonConvert.DeserializeObject<EdgeTileSet>(json);

            return newBoard;
        }
    }
}