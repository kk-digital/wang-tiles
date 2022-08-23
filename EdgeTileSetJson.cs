using Newtonsoft.Json;

namespace wang_tiles
{


    public class EdgeTileSetJson
    {


        public static string SaveJson(string filename, EdgeTileSet edgeTileSet)
        {
            //TODO(Mahdi): Implement

            DateTimeOffset dto = DateTimeOffset.Now;

            edgeTileSet.Description.CreationDate = dto.ToString();
            edgeTileSet.Description.CreationDateUnixTime = (Int64)dto.ToUnixTimeSeconds();
            var json = JsonConvert.SerializeObject(edgeTileSet, Formatting.Indented);

            File.WriteAllText(Constants.OutputPath + "\\" + filename, json);

            return json;
        }

        public static EdgeTileSet FromJson(string filename)
        {
            string json = File.ReadAllText(Constants.OutputPath + "\\" + filename);

            EdgeTileSet newBoard = JsonConvert.DeserializeObject < EdgeTileSet >(json);

            return newBoard;
        }
    }
}