namespace WangTile
{
    static class Globals
    {
        public static int ColorCount;

        public static int GetColorCount()
        {
            return ColorCount;
        }

        public static void IncrementColorCount()
        {
            Globals.ColorCount++;
        }
    }
}