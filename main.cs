
using Wang;

using System.Text.Json;
using System.Text.Json.Serialization;

class MainClass
{
    static void Main(string[] args)
    {   
        Generator newGeneratedBoard= new Generator();
        newGeneratedBoard.PlacementAlgo_V1();
       
    }
}