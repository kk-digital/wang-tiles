namespace Wang
{
    static class Globals
    {
        // (color, # of times used) pair
        // ColorMap value is how many times
        //  the color is used.
        // 
        // if color count is 0, we remove 
        // it from the dictionary.
        public static Dictionary < string, int > ColorCountMap = new Dictionary < string, int > ();
    }
}