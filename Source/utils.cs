namespace Wang.Other
{
    public static class Utils
    {
        public static int ToRGBA8(char r, char g, char b, char a)
        {
            return (r << 24) + (g << 16) + (b << 8) + a;
        }

        public static void FillRectangle(BigGustave.PngBuilder builder, int x, int y, int w, int h, PixelColor color)
        {
            for(int j = y; j < y + h; j++)
            {
                for(int i = x; i < x + w; i++)
                {
                    builder.SetPixel((byte)color.Red(), (byte)color.Green(), (byte)color.Blue(), i, j);
                }
            }
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