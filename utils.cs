namespace wang_tiles
{
    public static class Utils
    {
        public static int ToRGBA8(char r, char g, char b, char a)
        {
            return (((int)r) << 24) + (((int)g) << 16) + (((int)b) << 8) + (int)a;
        }

        public static long GenerateID()
        {
            DateTimeOffset dto = DateTimeOffset.Now;
            Random random = new Random();

            int timeInSeconds = (int)dto.ToUnixTimeSeconds();
            int randomNumber = (int)random.NextInt64();

            return CreateID(timeInSeconds, randomNumber);
        }

        public static long CreateID(int timeInSeconds, int randomNumber)
        {
            long result = ((long)timeInSeconds << 32) + randomNumber;

            return result;
        }

        public static int GetTimeFromID(long id)
        {
            return (int)(id >> 32);
        }

        public static int GetRandomNumberFromID(long id)
        {
            return (int)(id << 32 >> 32);
        }

        public static string IDToString(long id)
        {
            //TODO(Mahdi): empty for now
            return "";
        }
    }
}