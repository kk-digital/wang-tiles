namespace Wang
{
    public static class Utils
    {
        public static int ToRGBA8(char r, char g, char b, char a)
        {
            return (r << 24) + (g << 16) + (b << 8) + a;
        }
        
        public static void FillRectangle(BigGustave.PngBuilder builder, int col, int row, int w, int h, PixelColor color)
        {
            for(int j = row; j < row + h; j++)
            {
                for(int i = col; i < col + w; i++)
                {
                    builder.SetPixel((byte)color.Red(), (byte)color.Green(), (byte)color.Blue(), i, j);
                }
            }
        }

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

        static public int Partition(TileWeight[] arr, int left, int right) {
         TileWeight pivot;
         pivot = arr[left];
         while (true) {
            while (arr[left].Weight < pivot.Weight) {
               left++;
            }
            while (arr[right].Weight > pivot.Weight) {
               right--;
            }
            if (left < right) {
               TileWeight temp = arr[right];
               arr[right] = arr[left];
               arr[left] = temp;
            } else {
               return right;
            }
         }
      }
      static public void QuickSortTileWeight(TileWeight[] arr, int left, int right) {
         int pivot;
         if (left < right) {
            pivot = Partition(arr, left, right);
            if (pivot > 1) {
               QuickSortTileWeight(arr, left, pivot - 1);
            }  
            if (pivot + 1 < right) {
              QuickSortTileWeight(arr, pivot + 1, right);
            }
         }
      }
    }
}