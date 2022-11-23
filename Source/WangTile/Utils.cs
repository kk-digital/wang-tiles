namespace WangTile
{
    public static class Utils
    {
        public static (int col,int row) GetRandomPosition(int width, int height)
        {
            Random rand = new Random();
            int randCol = rand.Next(0,height);
            int randRow = rand.Next(0,width);

            return (col:randCol, row:randRow);
        }

        public static int GetBoardSlotIndex(int Width, int col, int row)
        {
            return (Width*col)+row;
        }

      // static public void PrintResult(Board newBoard, int numberOfColors, int numberOfFlips, int numberOfMismatch, TimeSpan time){
      //    Console.WriteLine("Statistics");
      //    Console.WriteLine($"Board Size {newBoard.Width} by {newBoard.Height}");
      //    Console.WriteLine("Number of colors is "+ numberOfColors);
      //    Console.WriteLine("Tileset length is "+ newBoard.TileSet[0].Tiles.Length);
      //    Console.WriteLine("Number of flips is "+ numberOfFlips);
      //    Console.WriteLine("Energy is "+ numberOfMismatch);
      //    Console.WriteLine("Time elapsed is "+ time + "(HH:MM:SS)");
      // }
    }
}