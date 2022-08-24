using System.Text.Json;
using System;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Wang;

namespace Wang.EdgeTile
{

    public class EdgeTileSetJson
    {


        public static string SaveJson(string filename, EdgeTileSet edgeTileSet)
        {
            //TODO(Mahdi): Implement

            DateTimeOffset dto = DateTimeOffset.Now;

            edgeTileSet.Description.CreationDate = dto.ToString();
            edgeTileSet.Description.CreationDateUnixTime = (UInt64)dto.ToUnixTimeSeconds(); ;
            var json = JsonConvert.SerializeObject(edgeTileSet, Formatting.Indented);

            File.WriteAllText(Constants.OutputPath + "\\" + filename, json);

            return json;
        }

        public static EdgeTileSet FromJson(string filename)
        {
            string json = File.ReadAllText(Constants.OutputPath + "\\" + filename);

            EdgeTileSet newObject = JsonConvert.DeserializeObject<EdgeTileSet>(json);

            return newObject;
        }
    }
}