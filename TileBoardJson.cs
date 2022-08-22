using System.Text.Json;
using System;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace wang_tiles
{

    public class TileBoardJson
    {


        public static void SaveJson(string filename, TileBoard tileBoard)
        {
            //TODO(Mahdi): Implement

            DateTimeOffset dto = DateTimeOffset.Now;

            tileBoard.CreationDate = dto.ToString();
            tileBoard.CreationDateUnixTime = (UInt64)dto.ToUnixTimeSeconds(); ;
            var json = JsonConvert.SerializeObject(tileBoard, Formatting.Indented);

            File.WriteAllText(Constants.OutputPath + "\\" + filename, json);

            Console.WriteLine(json);

        }
    }
}